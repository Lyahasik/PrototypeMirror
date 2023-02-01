using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private PlayerData _playerData;

        public PlayerData PlayerData
        {
            set => _playerData = value;
        }
    
        private void Update()
        {
            if (GameplayManager.IsPause)
                return;
            
            Turn();
        }

        private void Turn()
        {
            float turnAngle = -Input.GetAxis("Mouse Y") * _playerData.MouseSensitivityMove * Time.deltaTime;

            transform.RotateAround(transform.parent.position, transform.right, turnAngle);

            if (transform.rotation.eulerAngles.x <= 20f
                || transform.rotation.eulerAngles.x >= 50f)
            {
                transform.RotateAround(transform.parent.position, transform.right, -turnAngle);
            }
        }
    }
}
