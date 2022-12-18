using SmoothiesFarm.Farmer.UI;
using System;
using UnityEngine;

namespace SmoothiesFarm.Farm.Breeding.UI
{
    public class BreedingFarmerUIManager : GenericFarmerUIManager
    {
        [SerializeField]
        private InteractionInfoWidget m_interactionInfoWidget = null;
        protected InteractionInfoWidget InteractionInfoWidget => m_interactionInfoWidget;
        [SerializeField]
        private TimeBeforeDeliveringWidget m_timeBeforeDeliveringWidget = null;
        [SerializeField]
        private ItIsDeliveringTimeWidget m_itIsDeliveringTimeWidget = null;
        [SerializeField]
        private LostWidget m_lostWidget = null;

        [SerializeField]
        private FarmGameManager m_farmGameManager = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            if(CharacterMotor.TryGetComponent(out FarmerBreedingController breedingController))
            {
                breedingController.OnInteractionIndicationUpdated += HandleInteractionIndicationUpdated;
            }
            m_farmGameManager.OnLost += HandleLose;
            m_farmGameManager.OnDeliveringTime += HandleDeliveringTime;
        }

        private void HandleDeliveringTime()
        {
            m_itIsDeliveringTimeWidget.DisplayText();
        }

        private void HandleLose()
        {
            m_lostWidget.DisplayWidget();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (CharacterMotor && CharacterMotor.TryGetComponent(out FarmerBreedingController breedingController))
            {
                breedingController.OnInteractionIndicationUpdated -= HandleInteractionIndicationUpdated;
            }
            m_farmGameManager.OnLost -= HandleLose;
            m_farmGameManager.OnDeliveringTime -= HandleDeliveringTime;
        }

        private void Update()
        {
            m_timeBeforeDeliveringWidget.SetTimer(Mathf.Max(PlayerDataManager.PlayerDataManager.Instance.GameplayData.TimeBetweenDelivering 
                - (Time.time - PlayerDataManager.PlayerDataManager.Instance.TimeOfEndOfLastDelivering), 0f));
        }

        private void HandleInteractionIndicationUpdated(string a_info)
        {
            InteractionInfoWidget.SetInfo(a_info);
        }
    }
}