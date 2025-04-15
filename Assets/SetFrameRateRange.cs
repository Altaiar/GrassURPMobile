using UnityEngine;

public class SetFrameRateRange : MonoBehaviour
{
    void Start()
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                try
                {
                    AndroidJavaObject window = activity.Call<AndroidJavaObject>("getWindow");
                    AndroidJavaObject layoutParams = window.Call<AndroidJavaObject>("getAttributes");

                    // Set frame rate range (min and max)
                    layoutParams.Set("minFrameRate", 120);
                    layoutParams.Set("maxFrameRate", 120);
                    window.Call("setAttributes", layoutParams);

                    Debug.Log("Frame rate range set to 120 Hz.");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error setting frame rate range: {ex.Message}");
                }
            }));
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Outer error setting frame rate range: {ex.Message}");
        }
    }
}
