using UnityEngine;

public class FixedResolutionCamera : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(256, 224, FullScreenMode.ExclusiveFullScreen);
    }
}
