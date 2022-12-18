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
        private int m_damageToDealWithBaseBall = 5;
        [SerializeField]
        private int m_damageToDealWithKnife = 10;
        [SerializeField]
        private int m_damageToDealWithFork = 40;


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
                    if (PlayerDataManager.PlayerDataManager.Instance.HasUnlockedFork)
                    {
                        collisionHandler.CharacterMotor.GetComponent<HealthHandlingController>().TakeDamage(m_damageToDealWithFork, true);
                    }
                    else if (PlayerDataManager.PlayerDataManager.Instance.HasUnlockedKnife)
                    {
                        collisionHandler.CharacterMotor.GetComponent<HealthHandlingController>().TakeDamage(m_damageToDealWithKnife, true);
                    }
                    else
                    {
                        collisionHandler.CharacterMotor.GetComponent<HealthHandlingController>().TakeDamage(m_damageToDealWithBaseBall, true);
                    }
                }
                else if (hitInfo.collider.TryGetComponent(out HealthHandlingController healthController))
                {
                    if(PlayerDataManager.PlayerDataManager.Instance.HasUnlockedFork)
                    {
                        healthController.TakeDamage(m_damageToDealWithFork, true);
                    }
                    else if(PlayerDataManager.PlayerDataManager.Instance.HasUnlockedKnife)
                    {
                        healthController.TakeDamage(m_damageToDealWithKnife, true);
                    }
                    else
                    {
                        healthController.TakeDamage(m_damageToDealWithBaseBall, true);
                    }
                    
                }
            }
        }
    }
}