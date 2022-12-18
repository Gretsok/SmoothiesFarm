using TMPro;
using UnityEngine;

namespace SmoothiesFarm.Farm.Breeding
{
    public class TimeBeforeDeliveringWidget : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_timerText = null;

        public void SetTimer(float a_timeLeft)
        {
            m_timerText.text = a_timeLeft.ToString();
        }
    }
}