using System.Runtime.InteropServices;
using UnityEngine;

public class GyroReceiver : MonoBehaviour
{

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void StartGyroscope();
#endif

    [System.Serializable]
    public class GyroData
    {
        public float alpha; // kompas
        public float beta;  // atas-bawah
        public float gamma; // kiri-kanan
    }

    public static Vector2 CurrentDirection;

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartGyroscope();
#endif
    }


    public void OnGyroData(string json)
    {
        GyroData data = JsonUtility.FromJson<GyroData>(json);

        float upDown = data.beta;     // -180 to 180
        float leftRight = data.gamma; // -90 to 90

        float x = Mathf.Clamp(leftRight / 30, -1f, 1f);
        float y = Mathf.Clamp(upDown / 30, -1f, 1f);

        CurrentDirection = new Vector2(x, y);
    }
}
