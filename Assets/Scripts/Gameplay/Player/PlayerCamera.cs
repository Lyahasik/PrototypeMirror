using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private float _mouseSensitivity = 300;
    
        private void Update()
        {
            Turn();
        }

        private void Turn()
        {
            float turnAngle = -Input.GetAxis("Mouse Y");

            transform.RotateAround(transform.parent.position, transform.right, turnAngle);

            if (transform.rotation.eulerAngles.x <= 20f
                || transform.rotation.eulerAngles.x >= 50f)
            {
                transform.RotateAround(transform.parent.position, transform.right, -turnAngle);
            }
        }
    }
}
