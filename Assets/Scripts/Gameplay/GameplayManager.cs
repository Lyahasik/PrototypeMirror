using System.Collections;
using Gameplay.Player;
using Gameplay.Spawn;
using Mirror;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private Settings _settings;
    
        [SerializeField] private WindowVictory _windowVictory;

        private static bool _isPause;

        public static bool IsPause => _isPause;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _settings.GameplayManager = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        public void DeclareVictory(GameObject player, string nickname)
        {
            Cursor.lockState = CursorLockMode.None;
            _isPause = true;
        
            _windowVictory.Activate(nickname);

            StartCoroutine(RestartGame(player));
        }
        
        private IEnumerator RestartGame(GameObject player)
        {
            yield return new WaitForSeconds(5f);
        
            PointStorage.UnlockedPoints();

            player = NetworkClient.localPlayer.gameObject;

            player.GetComponent<PlayerState>()?.Reset();
            player.GetComponent<PlayerAttack>()?.Reset();
            player.GetComponent<PlayerMovement>()?.Reset();

            _windowVictory.Deactivate();
            
            Cursor.lockState = CursorLockMode.Locked;
            _isPause = false;
        }
    }
}
