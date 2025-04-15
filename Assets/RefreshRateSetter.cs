using UnityEngine;

public class LockRefreshRate : MonoBehaviour
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

                    layoutParams.Set("preferredRefreshRate", 120f);
                    window.Call("setAttributes", layoutParams);

                    Debug.Log("Refresh rate locked to 120 Hz.");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error setting refresh rate: {ex.Message}");
                }
            }));
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error initializing refresh rate: {ex.Message}");
        }

        // Optionally, prevent the screen from dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
