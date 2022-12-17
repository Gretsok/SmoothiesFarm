using UnityEngine;

namespace SmoothiesFarm.Farmer.UI
{
    public class ResourcesWidget : MonoBehaviour
    {
        [SerializeField]
        private ResourceLine m_bonbonResourceLine = null;
        [SerializeField]
        private ResourceLine m_unicornResourceLine = null;
        [SerializeField]
        private ResourceLine m_smoothiesResourceLine = null;

        public void SetBonbonAmount(int a_bonbonAmount)
        {
            m_bonbonResourceLine.SetResourceAmount(a_bonbonAmount);
        }

        public void SetUnicornAmount(int a_unicornAmount)
        {
            m_unicornResourceLine.SetResourceAmount(a_unicornAmount);
        }

        public void SetSmoothiesAmount(int a_smoothiesAmount)
        {
            m_smoothiesResourceLine.SetResourceAmount(a_smoothiesAmount);
        }
    }
}