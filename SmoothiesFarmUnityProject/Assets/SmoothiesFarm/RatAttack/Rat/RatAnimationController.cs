using UnityEngine;

namespace SmoothiesFarm.RatAttack
{
    public class RatAnimationController : MonoBehaviour
    {
        private int m_attackKey = Animator.StringToHash("Attack");
        private int m_speedKey = Animator.StringToHash("Speed");

        [SerializeField]
        private Animator m_animator = null;

        public void Hit()
        {
            m_animator.SetTrigger(m_attackKey);
        }

        public void SetSpeed(float a_speed)
        {
            m_animator.SetFloat(m_speedKey, a_speed);
        }
    }
}