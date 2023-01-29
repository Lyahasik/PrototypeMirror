using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameplaySettingsInstaller", menuName = "Installers/GameplaySettingsInstaller")]
    public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
    {
        public Gameplay.Settings _gameplaySettings;
    
        public override void InstallBindings()
        {
            Container
                .BindInstance(_gameplaySettings);
        }
    }
}