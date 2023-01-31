using Mirror;

namespace Gameplay.Player
{
    public partial class PlayerState : NetworkBehaviour
    {
        [SyncVar(hook = nameof(SyncInvulnerable))]
        private bool _syncInvulnerable;

        private partial void InvulnerableActivate();

        void SyncInvulnerable(bool oldValue, bool newValue)
        {
            _isInvulnerable = newValue;
            
            if (_isInvulnerable)
                InvulnerableActivate();
        }

        [Server]
        public void ChangeInvulnerableValue(bool newValue)
        {
            _syncInvulnerable = newValue;
        }

        [Command(requiresAuthority = false)]
        public partial void CmdChangeInvulnerable(bool newValue)
        {
            ChangeInvulnerableValue(newValue);
        }
    }
}
