using UnityEngine;
using System.Runtime.InteropServices;

namespace Kha2Dev.Example.CatchChiken.Helpers
{
    public class JslibHelpers
    {
        // [DllImport("__Internal")]
        // private static extern void requestGyroPermission();

        [DllImport("__Internal")]
        private static extern void enableWakeLock();

        [DllImport("__Internal")]
        private static extern void disableWakeLock();

//         public static void RequestGyroPermission()
//         {
// #if !UNITY_WEBGL || UNITY_EDITOR
//             Debug.Log("JslibHelpers.RequestGyroPermission() called");
// #else
//         requestGyroPermission();
// #endif
//         }

        public static void EnableWakeLock()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("JslibHelpers.EnableWakeLock() called");
#else
        enableWakeLock();
#endif
        }

        public static void DisableWakeLock()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("JslibHelpers.DisableWakeLock() called");
#else
        disableWakeLock();
#endif
        }
    }
}