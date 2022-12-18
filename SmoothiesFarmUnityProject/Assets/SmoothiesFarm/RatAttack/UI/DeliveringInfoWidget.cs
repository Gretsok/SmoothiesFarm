using UnityEngine;

namespace SmoothiesFarm.RatAttack
{
    public class DeliveringInfoWidget : MonoBehaviour
    {
        [SerializeField]
        private float m_durationWidget = 3f;

        private float m_timeOfStart = 0;

        private void OnEnable()
        {
            m_timeOfStart = Time.time;
        }


        private void Update()
        {
            if(Time.time - m_timeOfStart > m_durationWidget)
            {
                gameObject.SetActive(false);
            }
        }
    }
}