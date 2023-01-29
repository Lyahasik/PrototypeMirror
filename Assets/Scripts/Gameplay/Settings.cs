using System;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Settings
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
    }
}
