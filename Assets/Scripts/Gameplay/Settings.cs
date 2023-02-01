using Gameplay.Player;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "ScriptableObjects/GameplaySettings", order = 1)]
    public class Settings : ScriptableObject
    {
        [Header("Player")]
        public float SpeedMove;
        public float MouseSensitivityMove;
        
        [Space]
        public float SpeedAttack;
        public float RangeAttack;

        [Space]
        public float TimeInvulnerable;

        [Header("Prefabs")]
        public PlayerCamera PlayerCamera;
        public GameplayManager GameplayManager;
    }
}
