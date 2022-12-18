using TMPro;
using UnityEngine;

namespace SmoothiesFarm.SweetHunt
{
    public class TimeLeftWidget : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_timerText = null;

        public void SetTimeLeft(float a_timeLeft)
        {
            m_timerText.text = a_timeLeft.ToString("0.0");
        }
    }
}