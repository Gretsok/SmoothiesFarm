using TMPro;
using UnityEngine;

namespace SmoothiesFarm.Farm.Breeding.UI
{
    public class InteractionInfoWidget : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_infoText = null;

        public void SetInfo(string a_text)
        {
            m_infoText.text = a_text;
        }
    }
}