using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kha2Dev.Example.CatchChiken
{
    public class FallingItem : MonoBehaviour
    {
        [Serializable]
        public class ItemData
        {
            public float Chance;
            public Sprite Sprite;
            public Vector2 Size;
            public int Point;
        }

        [SerializeField] private float speed;
        [SerializeField] private Image image;
        [SerializeField] private List<ItemData> items;

        private RectTransform _rectTransform;
        private RectTransform _catcher;
        private Action<FallingItem, int> _onFinished;
        private Vector2 _endPosition;
        private ItemData _itemData;


        public void Initialize(RectTransform catcher, Vector2 startPosition, Vector2 endPosition, Action<FallingItem, int> onFinished)
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            _catcher = catcher;
            _endPosition = endPosition;
            _onFinished = onFinished;

            _itemData = GetRandomItem();
            image.sprite = _itemData.Sprite;

            _rectTransform.sizeDelta = _itemData.Size;
            _rectTransform.localPosition = startPosition;
        }

        private void Update()
        {
            if (GameManagers.Instance.GameStarted)
            {
                _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y - Time.deltaTime * speed);

                if (Vector3.Distance(_rectTransform.position, _catcher.position) < 50)
                {
                    Debug.Log("Tetangkep");
                    _onFinished?.Invoke(this, _itemData.Point);
                }

                if (_rectTransform.anchoredPosition.y < _endPosition.y)
                {
                    Debug.Log("Despawn");
                    _onFinished?.Invoke(this, 0);
                }
            }
        }

        private ItemData GetRandomItem()
        {
            float totalChance = 0f;
            foreach (var item in items)
            {
                totalChance += item.Chance;
            }

            float randomPoint = UnityEngine.Random.Range(0, totalChance);
            float cumulative = 0f;

            foreach (var item in items)
            {
                cumulative += item.Chance;
                if (randomPoint <= cumulative)
                {
                    return item;
                }
            }

            return null;
        }
    }
}