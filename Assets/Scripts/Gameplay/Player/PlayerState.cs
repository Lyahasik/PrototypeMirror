using System.Collections;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Collider))]
    public class PlayerState : MonoBehaviour
    {
        private Settings _settings;
        
        private MeshRenderer[] _meshRenderers;

        private Color[] _colorMeshes;
        private bool _isInvulnerable;

        public bool IsInvulnerable => _isInvulnerable;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        private void Start()
        {
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
            gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");

            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.material.color = Color.red;
            }

            StartCoroutine(ResetState());
        }

        private IEnumerator ResetState()
        {
            yield return new WaitForSeconds(_settings.TimeInvulnerable);
        
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material.color = _colorMeshes[i];
            }

            _isInvulnerable = false;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}
