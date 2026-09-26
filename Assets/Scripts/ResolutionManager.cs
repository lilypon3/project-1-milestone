using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(1024, 960, FullScreenMode.Windowed);
    }
}
