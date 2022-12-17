using UnityEngine;

namespace SmoothiesFarm.Farm.Unicorn
{
    public class UnicornAnimationController : MonoBehaviour
    {
        private readonly int m_speedParam = Animator.StringToHash("SPEED");


        [SerializeField]
        private Animator m_animator = null;
        [SerializeField]
        private UnicornCharacterMotor m_motor = null;


        private void FixedUpdate()
        {
            HandleSpeed();
        }

        private void HandleSpeed()
        {
            m_animator.SetFloat(m_speedParam, m_motor.CurrentSpeed);
        }
    }
}