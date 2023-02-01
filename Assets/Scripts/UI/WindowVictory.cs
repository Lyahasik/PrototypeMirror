using TMPro;
using UnityEngine;

namespace UI
{
    public class WindowVictory : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nickname;

        public void Activate(string nickname)
        {
            gameObject.SetActive(true);
            _nickname.text = nickname;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
