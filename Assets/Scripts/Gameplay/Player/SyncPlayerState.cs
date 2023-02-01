using Mirror;

namespace Gameplay.Player
{
    public partial class PlayerState : NetworkBehaviour
    {
        private partial void InvulnerableActivate();
        
        [ClientRpc]
        public partial void RpcInvulnerableActivate()
        {
            InvulnerableActivate();
        }
        
        [Command(requiresAuthority = false)]
        public partial void CmdInvulnerableActivate()
        {
            RpcInvulnerableActivate();
        }
    }
}
