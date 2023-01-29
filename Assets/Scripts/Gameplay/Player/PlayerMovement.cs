using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerMovement : MonoBehaviour
    {
        private DiContainer _container;
        private Settings _settings;
    
        private CharacterController _characterController;
        private PlayerAttack _playerAttack;

        [Inject]
        public void Construct(DiContainer container, Settings settings)
        {
            _container = container;
            _settings = settings;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerAttack = GetComponent<PlayerAttack>();
        }

        private void Start()
        {
            _container
                .InstantiatePrefab(_settings.PlayerCamera, transform);
        }

        void Update()
        {
            Move();
            Turn();
        }

        private void Move()
        {
            if (_playerAttack.IsActive)
                return;
                
            Vector3 step = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            step = transform.TransformDirection(step);

            _characterController.Move(step * _settings.SpeedMove * Time.deltaTime);
        }

        private void Turn()
        {
            if (_playerAttack.IsActive)
                return;
            
            Vector3 turnStep = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
        
            transform.Rotate(turnStep * _settings.MouseSensitivityMove * Time.deltaTime, Space.World);
        }
    }
}
