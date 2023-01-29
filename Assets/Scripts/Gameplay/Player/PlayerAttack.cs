using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour
    {
        private CharacterController _characterController;
        
        private Settings _settings;

        private float _distantion;
        private bool _isActive;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Attack();
            Move();
        }

        private void Attack()
        {
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
    }
}
