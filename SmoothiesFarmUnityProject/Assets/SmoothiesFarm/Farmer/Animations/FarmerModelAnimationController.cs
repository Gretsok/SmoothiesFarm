using UnityEngine;

namespace SmoothiesFarm.Farmer
{
    public class FarmerModelAnimationController : MonoBehaviour
    {
        private int m_hitParam = Animator.StringToHash("Hit");

        [SerializeField]
        private Animator m_animator = null;


        public void Hit()
        {
            m_animator.SetTrigger(m_hitParam);
        }
    }
}