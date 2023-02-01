using Gameplay.Spawn;
using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerData))]
    [RequireComponent(typeof(PlayerAttack))]
    public partial class PlayerMovement : NetworkBehaviour
    {
        private CharacterController _characterController;
        private PlayerData _playerData;
        private PlayerAttack _playerAttack;

        public partial void CmdChangePosition(Vector3 newValue);
        public partial void CmdChangeRotation(Quaternion newValue);

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerData = GetComponent<PlayerData>();
            _playerAttack = GetComponent<PlayerAttack>();
        }

        private void Start()
        {
            if (!isOwned)
                return;
            
            Instantiate(_playerData.PlayerCamera, transform).PlayerData = _playerData;
        }

        private void Update()
        {
            if (GameplayManager.IsPause
                || !isOwned)
                return;
            
            Move();
            Turn();
        }

        private void Move()
        {
            if (_playerAttack.IsActive)
                return;
                
            Vector3 step = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            step = transform.TransformDirection(step);

            _characterController.Move(step * _playerData.SpeedMove * Time.deltaTime);
            CmdChangePosition(_characterController.transform.position);
        }

        private void Turn()
        {
            if (_playerAttack.IsActive)
                return;
            
            Vector3 turnStep = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
        
            transform.Rotate(turnStep * _playerData.MouseSensitivityMove * Time.deltaTime, Space.World);
            CmdChangeRotation(transform.rotation);
        }

        public void Reset()
        {
            if (!isOwned)
                return;
            
            _characterController.enabled = false;

            transform.position = PointStorage.GetPoint().transform.position;
            
            _characterController.enabled = true;
            
            CmdChangePosition(transform.position);
        }
    }
}
