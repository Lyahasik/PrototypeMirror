using Gameplay.Spawn;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network.Room
{
    public class RoomManager : NetworkRoomManager
    {
        private bool _isShownStartButton;

        public override void OnRoomServerPlayersReady()
        {
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
            _isShownStartButton = true;
#endif
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && _isShownStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START"))
            {
                _isShownStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }

        public override Transform GetStartPosition()
        {
            Transform transformPosition = new GameObject().transform;
            transformPosition.position = PointStorage.GetPoint().transform.position;

            return transformPosition;
        }
    
        public override void OnServerReady(NetworkConnectionToClient conn)
        {
            base.OnServerReady(conn);

            if (conn != null && conn.identity != null)
            {
                GameObject roomPlayer = conn.identity.gameObject;

                if (roomPlayer != null && roomPlayer.GetComponent<NetworkRoomPlayer>() != null)
                    SceneLoadedForPlayer(conn, roomPlayer);
            }
        
            NetworkServer.SetClientReady(conn);
        }
    
        public override GameObject OnRoomServerCreateGamePlayer(NetworkConnectionToClient conn, GameObject roomPlayer)
        {
            GameObject player = Instantiate(playerPrefab);
            player.transform.position = PointStorage.GetPoint().transform.position;
        
            return player;
        }
    
        void SceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer)
        {
            Debug.Log($"NetworkRoom SceneLoadedForPlayer scene: {SceneManager.GetActiveScene().path} {conn}");

            if (Utils.IsSceneActive(RoomScene))
            {
                PendingPlayer pending;
                pending.conn = conn;
                pending.roomPlayer = roomPlayer;
                pendingPlayers.Add(pending);
                return;
            }

            GameObject gamePlayer = OnRoomServerCreateGamePlayer(conn, roomPlayer);
            if (gamePlayer == null)
            {
                gamePlayer = Instantiate(playerPrefab);
                gamePlayer.transform.position = PointStorage.GetPoint().transform.position;
            }

            if (!OnRoomServerSceneLoadedForPlayer(conn, roomPlayer, gamePlayer))
                return;

            NetworkServer.ReplacePlayerForConnection(conn, gamePlayer, true);
        }
    }
}
