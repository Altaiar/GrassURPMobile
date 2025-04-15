using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DynamicResolutionURP : MonoBehaviour
{
    public TextMeshProUGUI screenText;

    // Adjustable values for scaling
    public float maxRenderScale = 1.0f;      // Maximum resolution scale (1.0 = full resolution)
    public float minRenderScale = 0.5f;      // Minimum resolution scale (0.5 = half resolution)
    public float scaleIncrement = 0.05f;     // Increment value for scaling (how much to increase/decrease each time)
    public float frameTimeThreshold = 16.0f; // Frame time threshold (in milliseconds) for adjusting scale

    private float currentRenderScale = 1.0f; // Current resolution scale
    private UniversalRenderPipelineAsset urpAsset;

    private FrameTiming[] frameTimings = new FrameTiming[3];
    private double gpuFrameTime;
    private double cpuFrameTime;

    private uint frameCount = 0;
    private const uint numFrameTimings = 2;

    private void Start()
    {
        // Get the active URP asset
        urpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.defaultRenderPipeline;

        // Update the screen text
        UpdateResolutionText();
    }

    private void FixedUpdate()
    {
        // Track frame timing
        CaptureFrameTimings();

        // Automatically adjust the render scale based on frame times
        AdjustRenderScale();

        // Update the on-screen text with current resolution settings
        UpdateResolutionText();
    }

    // Capture GPU and CPU frame times
    private void CaptureFrameTimings()
    {
        frameCount++;

        if (frameCount <= numFrameTimings)
        {
            return;
        }

        FrameTimingManager.CaptureFrameTimings();
        FrameTimingManager.GetLatestTimings(numFrameTimings, frameTimings);

        if (frameTimings.Length < numFrameTimings)
        {
            Debug.LogFormat("Skipping frame {0}, didn't get enough frame timings.", frameCount);
            return;
        }

        gpuFrameTime = frameTimings[0].gpuFrameTime;
        cpuFrameTime = frameTimings[0].cpuFrameTime;
    }

    // Function to automatically adjust the render scale based on performance
    private void AdjustRenderScale()
    {
        // Calculate the frame time in milliseconds
        float gpuFrameTimeMs = (float)gpuFrameTime;
        float cpuFrameTimeMs = (float)cpuFrameTime;

        // Determine the average frame time between GPU and CPU
        float avgFrameTime = (gpuFrameTimeMs + cpuFrameTimeMs) / 2f;

        // Check if the average frame time exceeds the threshold (indicating performance drop)
        if (avgFrameTime > frameTimeThreshold)
        {
            // Decrease render scale to improve performance
            currentRenderScale = Mathf.Max(minRenderScale, currentRenderScale - scaleIncrement);
        }
        else
        {
            // Increase render scale to restore visual quality
            currentRenderScale = Mathf.Min(maxRenderScale, currentRenderScale + scaleIncrement);
        }

        // Apply the new render scale if it has changed
        SetRenderScale(currentRenderScale);
    }

    // Function to set the render scale on the URP asset
    private void SetRenderScale(float scale)
    {
        if (urpAsset != null && Mathf.Abs(urpAsset.renderScale - scale) > Mathf.Epsilon)
        {
            urpAsset.renderScale = scale;
            Debug.Log($"Render Scale set to: {scale}");
        }
        else if (urpAsset == null)
        {
            Debug.LogError("URP Asset not found!");
        }
    }

    // Function to update the screen text with current resolution settings
    private void UpdateResolutionText()
    {
        int renderWidth = (int)(Screen.width * currentRenderScale);
        int renderHeight = (int)(Screen.height * currentRenderScale);

        screenText.text = string.Format("Render Scale: {0:F2}\nResolution: {1}x{2}\nGPU Time: {3:F2}ms\nCPU Time: {4:F2}ms",
            currentRenderScale,
            renderWidth,
            renderHeight,
            gpuFrameTime, // Convert to milliseconds
            cpuFrameTime); // Convert to milliseconds
    }

    public void EnableOrDisable()
    {
        this.enabled = !this.enabled;
    }
}
