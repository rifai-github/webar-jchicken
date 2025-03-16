using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class GyroController : MonoBehaviour
{
    [SerializeField] private RectTransform catcher;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private float speed = 30f;

    private float maxX;
    private bool gyroEnabled;
    private Gyroscope gyro;

    void Start()
    {
        float canvasWidth = canvas.rect.width;
        float catcherWidth = catcher.rect.width;

        maxX = (canvasWidth / 2) - (catcherWidth / 2);

        gyroEnabled = SystemInfo.supportsGyroscope;
        if (gyroEnabled)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            Debug.Log("Gyroscope supported");
        }
        else
        {
            Debug.Log("Gyroscope not supported");
        }
    }

    void Update()
    {
        if (gyroEnabled)
        {
            float moveX = gyro.rotationRateUnbiased.y * speed;
            float newX = Mathf.Clamp(catcher.anchoredPosition.x + moveX, -maxX, maxX);

            catcher.anchoredPosition = new Vector2(newX, catcher.anchoredPosition.y);
        }
    }
}
