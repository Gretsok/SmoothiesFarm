using System;
using UnityEngine;

namespace SmoothiesFarm.Farm.Breeding
{
    public class FarmerBreedingController : MonoBehaviour
    {
        [SerializeField]
        private Farmer.FarmerCharacterMotor m_characterMotor = null;
        [SerializeField]
        private Transform m_sightOrigin = null;
        [SerializeField]
        private float m_sightDistance = 3f;

        private int m_bonbonsLeft = 0;
        public Action<int> OnBonbonConsumed = null;

        private void Awake()
        {
            SetBonbonsLeft(5);
        }

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
            if (m_bonbonsLeft <= 0) return;
            if(Physics.Raycast(m_sightOrigin.position, m_sightOrigin.forward, out RaycastHit hitInfo, m_sightDistance))
            {
                if(hitInfo.collider.TryGetComponent(out Unicorn.UnicornCollisionHandler collisionHandler))
                {
                    Instantiate(collisionHandler.CharacterMotor.gameObject,
                        collisionHandler.CharacterMotor.gameObject.transform.position + Vector3.up * 2f,
                        collisionHandler.CharacterMotor.gameObject.transform.rotation);
                    ConsumeOneBonbon();
                }
            }
        }

        public void SetBonbonsLeft(int a_bonbonsLeft)
        {
            m_bonbonsLeft = a_bonbonsLeft;
        }

        private void ConsumeOneBonbon()
        {
            --m_bonbonsLeft;
            OnBonbonConsumed?.Invoke(m_bonbonsLeft);
        }
    }
}