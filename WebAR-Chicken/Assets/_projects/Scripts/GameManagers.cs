using UnityEngine;

namespace Kha2Dev.Example.CatchChiken
{
    using Helpers;
    public class GameManagers : MonoBehaviour
    {
        public static GameManagers Instance { get; private set; }
        
        [SerializeField] private GamePlayManager gamePlayManager;

        public bool GameStarted { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            gamePlayManager.StartGame();
        } 

        public void SetGameStart(bool gameStarted)
        {
            GameStarted = gameStarted;
        }
    }
}