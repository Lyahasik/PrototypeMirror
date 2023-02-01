using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerData))]
    [RequireComponent(typeof(PlayerState))]
    public partial class PlayerAttack : NetworkBehaviour
    {
        private const int targetHit = 3;

        private CharacterController _characterController;
        private PlayerData _playerData;
        private PlayerState _playerState;

        private float _distantion;
        private bool _isActive;

        private int _countHit;

        public bool IsActive => _isActive;

        public partial void RpcVictory(string nickname);
        public partial void CmdVictory(string nickname);
        public partial void CmdChangePosition(Vector3 newValue);

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerData = GetComponent<PlayerData>();
            _playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
            if (GameplayManager.IsPause
                || !isOwned)
                return;
            
            Attack();
            Move();
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_isActive)
                return;
            
            PlayerState player = hit.gameObject.GetComponent<PlayerState>();
            
            if (player)
            {
                _countHit++;
                player.TakeHit();

                if (_countHit >= targetHit)
                {
                    if (isServer)
                        RpcVictory($"Player { player.netId }");
                    else
                        CmdVictory($"Player { player.netId }");
                }
            }
        }

        private void Attack()
        {
            if (_playerState.IsInvulnerable)
                return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isActive = true;
            }
        }

        private void Move()
        {
            if (!_isActive)
                return;
            
            float step = _playerData.SpeedAttack * Time.deltaTime;

            _distantion = Mathf.Clamp(_distantion + step, 0f, _playerData.RangeAttack);

            _characterController.Move(transform.forward * step);
            CmdChangePosition(_characterController.transform.position);

            if (_distantion == _playerData.RangeAttack)
                StopAttack();
        }

        private void StopAttack()
        {
            _isActive = false;
            _distantion = 0f;
        }

        public void Reset()
        {
            StopAttack();
            _countHit = 0;
        }
    }
}
