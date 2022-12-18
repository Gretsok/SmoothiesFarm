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

        public Action<string> OnInteractionIndicationUpdated = null;

        private Interaction.Interactable m_currentInteractableInSight = null;

        private void Start()
        {
            SetBonbonsLeft(PlayerDataManager.PlayerDataManager.Instance.Bonbons);
        }

        private void OnEnable()
        {
            m_characterMotor.OnAttackPerformed += HandleAttackPerformed;
            PlayerDataManager.PlayerDataManager.Instance.OnBonbonValueChanged += SetBonbonsLeft;
        }

        private void OnDisable()
        {
            m_characterMotor.OnAttackPerformed -= HandleAttackPerformed;
            PlayerDataManager.PlayerDataManager.Instance.OnBonbonValueChanged -= SetBonbonsLeft;
        }

        private void HandleAttackPerformed()
        {
            
            if(Physics.Raycast(m_sightOrigin.position, m_sightOrigin.forward, out RaycastHit hitInfo, m_sightDistance))
            {
                if(hitInfo.collider.TryGetComponent(out Unicorn.UnicornCollisionHandler collisionHandler))
                {
                    if (m_bonbonsLeft <= 0 || !PlayerDataManager.PlayerDataManager.Instance.CanAddMoreUnicorn()) return;
                    Instantiate(collisionHandler.CharacterMotor.gameObject,
                        collisionHandler.CharacterMotor.gameObject.transform.position + Vector3.up * 2f,
                        collisionHandler.CharacterMotor.gameObject.transform.rotation);
                    --m_bonbonsLeft;
                    PlayerDataManager.PlayerDataManager.Instance.ConsumeBonbon();
                    PlayerDataManager.PlayerDataManager.Instance.AddUnicorn();

                }
                if(hitInfo.collider.TryGetComponent(out Interaction.Interactable interactable))
                {
                    interactable.Interact();
                }
            }
        }

        private void FixedUpdate()
        {
            var previousInteractable = m_currentInteractableInSight;
            m_currentInteractableInSight = GetInteractableInSight();
            if(previousInteractable != m_currentInteractableInSight)
            {
                if (m_currentInteractableInSight)
                {
                    OnInteractionIndicationUpdated?.Invoke(m_currentInteractableInSight.GetInfoText());
                }
                else
                {
                    OnInteractionIndicationUpdated?.Invoke("");
                }
            }
        }

        private Interaction.Interactable GetInteractableInSight()
        {
            if (Physics.Raycast(m_sightOrigin.position, m_sightOrigin.forward, out RaycastHit hitInfo, m_sightDistance))
            {
                if (hitInfo.collider.TryGetComponent(out Interaction.Interactable interactable))
                {
                    return interactable;
                }
            }
            return null;
        }

        public void SetBonbonsLeft(int a_bonbonsLeft)
        {
            m_bonbonsLeft = a_bonbonsLeft;
        }

    }
}