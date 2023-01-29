using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        
        public override void InstallBindings()
        {
            Container
                .Bind(typeof(PlayerMovement), typeof(PlayerAttack))
                .FromComponentInNewPrefab(_player)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<PointStorage>()
                .AsSingle();
        }
    }
}