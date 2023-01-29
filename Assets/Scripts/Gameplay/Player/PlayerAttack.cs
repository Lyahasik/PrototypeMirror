using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerState))]
    public class PlayerAttack : MonoBehaviour
    {
        private Settings _settings;
        
        private CharacterController _characterController;
        private PlayerState _playerState;

        private float _distantion;
        private bool _isActive;

        private int _countHit;

        public bool IsActive => _isActive;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
            Attack();
            Move();
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
        
            float step = _settings.SpeedAttack * Time.deltaTime;

            _distantion = Mathf.Clamp(_distantion + step, 0f, _settings.RangeAttack);

            _characterController.Move(transform.forward * step);

            if (_distantion == _settings.RangeAttack)
            {
                _isActive = false;
                _distantion = 0f;
            }
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
            }
        }
    }
}
