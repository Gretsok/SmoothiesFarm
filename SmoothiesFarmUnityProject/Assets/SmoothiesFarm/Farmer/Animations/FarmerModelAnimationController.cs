using UnityEngine;

namespace SmoothiesFarm.Farmer
{
    public class FarmerModelAnimationController : MonoBehaviour
    {
        private int m_hitParam = Animator.StringToHash("Hit");
        private int m_baseballParam = Animator.StringToHash("BaseBall");

        [SerializeField]
        private Animator m_animator = null;


        public void Hit()
        {
            m_animator.SetTrigger(m_hitParam);
        }

        public void SetBaseball(bool a_baseball)
        {
            m_animator.SetBool(m_baseballParam, a_baseball);
        }
    }
}