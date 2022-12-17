using System;
using UnityEngine;

namespace SmoothiesFarm.Farm.RatAttack
{
    public class FarmerRatAttackBehavior : MonoBehaviour
    {
        [SerializeField]
        private Farmer.FarmerCharacterMotor m_characterMotor = null;
        [SerializeField]
        private Transform m_sightOrigin = null;
        [SerializeField]
        private float m_sightDistance = 3f;

        
        public Action<int> OnBonbonConsumed = null;

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
            
            if(Physics.Raycast(m_sightOrigin.position, m_sightOrigin.forward, out RaycastHit hitInfo, m_sightDistance))
            {
                if(hitInfo.collider.TryGetComponent(out Unicorn.UnicornCollisionHandler collisionHandler))
                {
                    Instantiate(collisionHandler.CharacterMotor.gameObject,
                        collisionHandler.CharacterMotor.gameObject.transform.position + Vector3.up * 2f,
                        collisionHandler.CharacterMotor.gameObject.transform.rotation);
                    
                }
            }
        }
    }
}