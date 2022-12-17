using UnityEngine;
using UnityEngine.Events;

namespace SmoothiesFarm.Farm.Breeding.Interaction
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        private string m_infoText = "Interact";
        [SerializeField]
        private UnityEvent m_interactionEvent = null;

        public string GetInfoText()
        {
            return m_infoText;
        }

        public void Interact()
        {
            m_interactionEvent?.Invoke();
        }
    }
}