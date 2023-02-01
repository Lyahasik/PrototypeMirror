using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerData : NetworkBehaviour
    {
        [SerializeField] private Settings _settings;

        public float SpeedMove => _settings.SpeedMove;
        public float MouseSensitivityMove => _settings.MouseSensitivityMove;

        public float SpeedAttack => _settings.SpeedAttack;
        public float RangeAttack => _settings.RangeAttack;

        public float TimeInvulnerable => _settings.TimeInvulnerable;

        public PlayerCamera PlayerCamera => _settings.PlayerCamera;
        public GameplayManager GameplayManager => _settings.GameplayManager;
    }
}
