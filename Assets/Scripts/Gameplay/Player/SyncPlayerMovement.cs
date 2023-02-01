using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    public partial class PlayerMovement : NetworkBehaviour
    {
        [SyncVar(hook = nameof(SyncPosition))]
        private Vector3 _syncPosition;
        
        [SyncVar(hook = nameof(SyncRotation))]
        private Quaternion _syncRotation;
    
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
    
        private void SyncRotation(Quaternion oldValue, Quaternion newValue)
        {
            transform.rotation = newValue;
        }
        
        [Server]
        public void ChangeRotationValue(Quaternion newValue)
        {
            _syncRotation = newValue;
        }
        
        [Command]
        public partial void CmdChangeRotation(Quaternion newValue)
        {
            ChangeRotationValue(newValue);
        }
    }
}
