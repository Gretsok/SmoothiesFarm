using SmoothiesFarm.Farm.Unicorn;
using System;
using UnityEngine;

namespace SmoothiesFarm.RatAttack.Rat
{
    public class RatController : MonoBehaviour
    {
        [SerializeField]
        private RatCharacterMotor m_characterRotor = null;

        private UnicornCharacterMotor m_target = null;

        public Action<RatController> OnTargetKilled = null;

        public void SetTarget(UnicornCharacterMotor a_unicornMotor)
        {
            m_target = a_unicornMotor;
        }

        private void Update()
        {
            if(m_target == null)
            {
                OnTargetKilled?.Invoke(this);
            }
        }

        private void FixedUpdate()
        {

            if (!m_target) return;

            var plannedDirection = (m_target.transform.position - transform.position).normalized;
            var magnitude = plannedDirection.magnitude;
            plannedDirection.y = 0;
            plannedDirection = plannedDirection.normalized * magnitude;
            m_characterRotor.ChangeDirection(plannedDirection);
            if (Vector3.Distance(transform.position, m_target.transform.position) > 1.5f)
            {
                m_characterRotor.SetSpeed(1f);
            }
            else
            {
                m_characterRotor.Attack((m_target.transform.position - transform.position).normalized);
            }
        }
    }

}
