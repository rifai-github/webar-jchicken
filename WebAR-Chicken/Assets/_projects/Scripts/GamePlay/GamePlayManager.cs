using UnityEngine;
// using Lean.Pool;

namespace Kha2Dev.Example.JChiken
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private Transform targetTransform;
        // [SerializeField] private LeanGameObjectPool itemsPoller;
        [SerializeField] private Transform plate;

        private void StartGame()
        {
            InvokeRepeating(nameof(Spawn), 0f, 3f);
        }

        private void Spawn()
        {

        }

        private void GameOver()
        {
            CancelInvoke(nameof(Spawn));
        }
    }
}