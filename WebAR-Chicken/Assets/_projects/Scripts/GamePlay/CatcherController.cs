using TMPro;
using UnityEngine;

namespace Kha2Dev.Example.CatchChiken
{
    public class CatcherController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private TMP_Text debugText;
        private RectTransform _catcher;
        private float _maxX;

        public void Initialize()
        {
            _catcher = GetComponent<RectTransform>();
            float canvasWidth = canvas.rect.width;
            float catcherWidth = _catcher.rect.width;

            _maxX = (canvasWidth / 2) - (catcherWidth / 2);
        }

        void Update()
        {
            if (GameManagers.Instance.GameStarted)
            {
                var newX = Mathf.Lerp(_catcher.anchoredPosition.x, GyroReceiver.CurrentDirection.x * _maxX, Time.deltaTime * 15);
                _catcher.anchoredPosition = new Vector2(newX, _catcher.anchoredPosition.y);
                debugText.text = GyroReceiver.CurrentDirection.ToString();
            }
        }
    }
}