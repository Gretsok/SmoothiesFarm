using TMPro;
using UnityEngine;

namespace SmoothiesFarm.Farmer.UI
{
    public class ResourceLine : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_text = null;

        public void SetResourceAmount(int a_resourceAmount)
        {
            m_text.text = a_resourceAmount.ToString();
        }
    }
}
