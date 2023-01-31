using UnityEngine;

namespace Gameplay.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        private bool _isLock;

        public bool IsLock
        {
            get => _isLock;
            set => _isLock = value;
        }

        private void OnEnable()
        {
            PointStorage.AddPoint(this);
        }

        private void OnDisable()
        {
            PointStorage.RemovePoint(this);
        }
    }
}
