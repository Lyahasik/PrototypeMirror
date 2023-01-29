using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private Settings _settings;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }
    
        private void Update()
        {
            Turn();
        }

        private void Turn()
        {
            float turnAngle = -Input.GetAxis("Mouse Y") * _settings.MouseSensitivityMove * Time.deltaTime;

            transform.RotateAround(transform.parent.position, transform.right, turnAngle);

            if (transform.rotation.eulerAngles.x <= 20f
                || transform.rotation.eulerAngles.x >= 50f)
            {
                transform.RotateAround(transform.parent.position, transform.right, -turnAngle);
            }
        }
    }
}
