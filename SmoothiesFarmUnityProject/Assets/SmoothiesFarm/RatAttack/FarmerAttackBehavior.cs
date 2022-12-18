using UnityEngine;

namespace SmoothiesFarm.RatAttack
{
    public class FarmerAttackBehavior : MonoBehaviour
    {
        [SerializeField]
        private Farmer.FarmerModelAnimationController m_animationController = null;
        [SerializeField]
        private Farmer.FarmerCharacterMotor m_characterMotor = null;
        [SerializeField]
        private Transform m_sightOrigin = null;
        [SerializeField]
        private float m_sightDistance = 3f;

        [SerializeField]
        private int m_damageToDeal = 5;
        

        private void OnEnable()
        {
            m_characterMotor.OnAttackPerformed += HandleAttackPerformed;
        }

        private void OnDisable()
        {
            m_characterMotor.OnAttackPerformed -= HandleAttackPerformed;
        }

        private void HandleAttackPerformed()
        {
            m_animationController.Hit();
            if (Physics.Raycast(m_sightOrigin.position, m_sightOrigin.forward, out RaycastHit hitInfo, m_sightDistance))
            {
                if(hitInfo.collider.TryGetComponent(out Farm.Unicorn.UnicornCollisionHandler collisionHandler))
                {
                    collisionHandler.CharacterMotor.GetComponent<HealthHandlingController>().TakeDamage(m_damageToDeal, true);  
                }
                else if (hitInfo.collider.TryGetComponent(out HealthHandlingController healthController))
                {
                    healthController.TakeDamage(m_damageToDeal, true);
                }
            }
        }
    }
}