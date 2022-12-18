using UnityEngine;


namespace SmoothiesFarm.RatAttack.UI
{
    public class DeliveringUIManager : MonoBehaviour
    {
        [SerializeField]
        private DeliveringGameManager m_gameManager = null;

        [SerializeField]
        private SmoothiesCounter m_smoothiesCounter = null;

        private void Update()
        {
            m_smoothiesCounter.SetSmoothiesCounter(m_gameManager.TotalSmoothiesEarned);
        }
    }
}