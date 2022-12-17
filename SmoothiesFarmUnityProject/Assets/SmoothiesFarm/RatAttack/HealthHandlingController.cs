using System;
using UnityEngine;

namespace SmoothiesFarm.RatAttack
{
    public class HealthHandlingController : MonoBehaviour
    {
        [SerializeField]
        private int m_maxLife = 20;
        private int m_currentLife = 0;

        public Action<HealthHandlingController, bool> OnDeath = null;

        private void Awake()
        {
            m_currentLife = m_maxLife;
        }

        public void TakeDamage(int a_damageToTake, bool a_fromPlayer = false)
        {
            m_currentLife -= a_damageToTake;
            if(m_currentLife <= 0)
            {
                HandleDeath(a_fromPlayer);
            }
        }

        private void HandleDeath(bool a_fromPlayer)
        {
            OnDeath?.Invoke(this, a_fromPlayer);
        }
    }
}