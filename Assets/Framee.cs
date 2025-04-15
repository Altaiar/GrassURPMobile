using UnityEngine;

public class Framee : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int width = Screen.currentResolution.width;
        int height = Screen.currentResolution.height;
        uint desiredRefreshRate = 120; // Example: set to 120 Hz
        RefreshRate refreshRate = new RefreshRate { numerator = desiredRefreshRate, denominator = 1 };

        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow, refreshRate);
    }

    public void FrameRateChange(int index)
    {
        switch (index)
        {
            case 0: Application.targetFrameRate = 30; break;
            case 1: Application.targetFrameRate = 61; break;
            case 2: Application.targetFrameRate = 121; break;
            case 3: Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value; break;
            default: break;
        }
    }
}
