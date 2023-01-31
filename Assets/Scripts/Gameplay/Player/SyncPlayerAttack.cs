using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    public partial class PlayerAttack : NetworkBehaviour
    {
        [SyncVar(hook = nameof(SyncPosition))]
        private Vector3 _syncPosition;

        void SyncPosition(Vector3 oldValue, Vector3 newValue)
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
    }
}
