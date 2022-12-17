using UnityEngine;

namespace SmoothiesFarm.Farmer.UI
{
    public class FarmerUIManager : MonoBehaviour
    {
        [SerializeField]
        private ResourcesWidget m_resourcesWidget = null;
        public ResourcesWidget ResourcesWidget => m_resourcesWidget;
    }
}