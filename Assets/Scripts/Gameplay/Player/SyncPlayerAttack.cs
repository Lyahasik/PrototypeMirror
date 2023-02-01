using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    public partial class PlayerAttack : NetworkBehaviour
    {
        [SyncVar(hook = nameof(SyncPosition))]
        private Vector3 _syncPosition;

        private void SyncPosition(Vector3 oldValue, Vector3 newValue)
        {
            transform.position = newValue;
        }

        [Server]
        public void ChangePositionValue(Vector3 newValue)
        {
            _syncPosition = newValue;
        }

        [Command]
        public partial void CmdChangePosition(Vector3 newValue)
        {
            ChangePositionValue(newValue);
        }
        
        [ClientRpc]
        public partial void RpcVictory(string nickname)
        {
            _playerData.GameplayManager.DeclareVictory(gameObject, nickname);
        }
        
        [Command]
        public partial void CmdVictory(string nickname)
        {
            RpcVictory(nickname);
        }
        
    }
}
