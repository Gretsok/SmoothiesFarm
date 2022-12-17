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

        protected override void OnEnable()
        {
            base.OnEnable();
            if(CharacterMotor.TryGetComponent(out FarmerBreedingController breedingController))
            {
                breedingController.OnInteractionIndicationUpdated += HandleInteractionIndicationUpdated;
            }
        }

        private void OnDisable()
        {
            if (CharacterMotor.TryGetComponent(out FarmerBreedingController breedingController))
            {
                breedingController.OnInteractionIndicationUpdated -= HandleInteractionIndicationUpdated;
            }
        }

        private void HandleInteractionIndicationUpdated(string a_info)
        {
            InteractionInfoWidget.SetInfo(a_info);
        }
    }
}