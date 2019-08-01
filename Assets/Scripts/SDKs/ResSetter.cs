using UnityEngine;

public class ResSetter : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(960, 1600, false);
        //Screen.fullScreen = false;
    }
}
