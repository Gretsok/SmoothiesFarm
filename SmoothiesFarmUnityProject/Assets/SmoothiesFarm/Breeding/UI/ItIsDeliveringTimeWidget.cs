using UnityEngine;

namespace SmoothiesFarm.Farm.Breeding.UI
{
    public class ItIsDeliveringTimeWidget : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_widgetContainer = null;

        private void Awake()
        {
            m_widgetContainer.SetActive(false);
        }

        public void DisplayText()
        {
            m_widgetContainer.SetActive(true);
        }
    }
}