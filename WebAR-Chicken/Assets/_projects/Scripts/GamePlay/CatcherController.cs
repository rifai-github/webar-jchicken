using UnityEngine;

namespace Kha2Dev.Example.CatchChiken
{
    public class CatcherController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private float speed = 30f;
        [SerializeField] private float mouseSpeed = 1f;
        private RectTransform _catcher;
        private Gyroscope _gyro;
        private float _maxX;
        private bool _gyroEnabled;

        public void Initialize()
        {
            _catcher = GetComponent<RectTransform>();
            float canvasWidth = canvas.rect.width;
            float catcherWidth = _catcher.rect.width;

            _maxX = (canvasWidth / 2) - (catcherWidth / 2);

#if UNITY_EDITOR
            _gyroEnabled = false;
#else
        _gyroEnabled = SystemInfo.supportsGyroscope;
#endif

            if (_gyroEnabled)
            {
                _gyro = Input.gyro;
                _gyro.enabled = true;
                Debug.Log("Gyroscope supported");
            }
            else
            {
                Debug.Log("Gyroscope not supported");
            }
        }

        void Update()
        {
            if (GameManagers.Instance.GameStarted)
            {
                float moveX = 0;

                if (_gyroEnabled)
                {
                    moveX = _gyro.rotationRateUnbiased.y * speed;
                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        moveX = Input.GetAxis("Mouse X") * mouseSpeed * 10f;
                    }
                }

                float newX = Mathf.Clamp(_catcher.anchoredPosition.x + moveX, -_maxX, _maxX);
                _catcher.anchoredPosition = new Vector2(newX, _catcher.anchoredPosition.y);
            }
        }
    }
}