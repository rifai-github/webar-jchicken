using UnityEngine;
using System.Collections;
using Lean.Pool;
using TMPro;

namespace Kha2Dev.Example.CatchChiken
{
    using Helpers;
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private RectTransform spawnTransform;
        [SerializeField] private RectTransform targetTransform;
        [SerializeField] private RectTransform catcher;
        [SerializeField] private LeanGameObjectPool itemPooler;
        [SerializeField] private TextMeshProUGUI pointText;


        private int _point;
        private float _maxX;

        public void StartGame()
        {
            JslibHelpers.EnableWakeLock();
            catcher.GetComponent<CatcherController>().Initialize();

            float canvasWidth = canvas.rect.width;
            _maxX = (canvasWidth / 2) - 100;

            StartCoroutine(WaitStartGame());
        }

        private IEnumerator WaitStartGame()
        {
            for (int i = 3; i > 0; i--)
            {
                Debug.Log(i);
                yield return new WaitForSeconds(1f);
            }

            GameManagers.Instance.SetGameStart(true);
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            yield return new WaitForEndOfFrame();
            while (GameManagers.Instance.GameStarted)
            {
                float randomDelay = Random.Range(.4f, 1f);
                yield return new WaitForSeconds(randomDelay);
                var newItem = itemPooler.Spawn(itemPooler.transform);

                Vector2 spawnPosition = new(Random.Range(-_maxX, _maxX), spawnTransform.anchoredPosition.y);
                newItem.GetComponent<FallingItem>().Initialize(catcher, spawnPosition, targetTransform.anchoredPosition, OnItemFinished);
            }
        }

        private void OnItemFinished(FallingItem item, int point)
        {
            if (point < 0)
            {
                GameOver();
                return;
            }

            _point += point;
            pointText.text = _point.ToString();

            itemPooler.Despawn(item.gameObject);
        }

        private void GameOver()
        {
            GameManagers.Instance.SetGameStart(false);
            JslibHelpers.DisableWakeLock();
            // itemPooler.DespawnAll();
        }

        private void OnApplicationQuit()
        {
            GameOver();
        }
    }
}