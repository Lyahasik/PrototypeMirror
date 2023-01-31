using System.Collections;
using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(PlayerData))]
    public partial class PlayerState : NetworkBehaviour
    {
        private PlayerData _playerData;
        private MeshRenderer[] _meshRenderers;

        private Color[] _colorMeshes;
        private bool _isInvulnerable;

        public bool IsInvulnerable => _isInvulnerable;

        public partial void CmdChangeInvulnerable(bool newValue);

        private void Start()
        {
            _playerData = GetComponent<PlayerData>();
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();

            SaveBaseColors();
        }

        private void SaveBaseColors()
        {
            _colorMeshes = new Color[_meshRenderers.Length];
        
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _colorMeshes[i] = _meshRenderers[i].material.color;
            }
        }

        public void TakeHit()
        {
            _isInvulnerable = true;
            CmdChangeInvulnerable(_isInvulnerable);
            InvulnerableActivate();
        }

        private partial void InvulnerableActivate()
        {
            gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");

            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.material.color = Color.red;
            }

            StartCoroutine(ResetState());
        }

        private IEnumerator ResetState()
        {
            yield return new WaitForSeconds(_playerData.TimeInvulnerable);
        
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material.color = _colorMeshes[i];
            }

            _isInvulnerable = false;
            CmdChangeInvulnerable(_isInvulnerable);
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}
