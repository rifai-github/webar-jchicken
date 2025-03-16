using System.Runtime.InteropServices;
using UnityEngine;

public class GyroPermission : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void requestGyroPermission();
    
    public void AskPermission()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            requestGyroPermission();
        #endif
    }
}
