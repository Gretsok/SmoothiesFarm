using TMPro;
using UnityEngine;

namespace SmoothiesFarm.RatAttack.UI
{
    public class SmoothiesCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_text = null;

        public void SetSmoothiesCounter(int a_smoothiesCount)
        {
            m_text.text = a_smoothiesCount.ToString();
        }
    }
}