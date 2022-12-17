using UnityEngine;

namespace SmoothiesFarm.Farmer.UI
{
    public class GenericFarmerUIManager : MonoBehaviour
    {
        [Header("External Refs")]
        [SerializeField]
        private FarmerCharacterMotor m_characterMotor = null;
        protected FarmerCharacterMotor CharacterMotor => m_characterMotor;

        [Header("UI Refs")]
        [SerializeField]
        private ResourcesWidget m_resourcesWidget = null;
        protected ResourcesWidget ResourcesWidget => m_resourcesWidget;

        protected virtual void Start()
        {
            ResourcesWidget.SetBonbonAmount(PlayerDataManager.PlayerDataManager.Instance.Bonbons);
            ResourcesWidget.SetUnicornAmount(PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns);
            ResourcesWidget.SetSmoothiesAmount(PlayerDataManager.PlayerDataManager.Instance.SmoothiesPoint);
        }

        protected virtual void OnEnable()
        {
            PlayerDataManager.PlayerDataManager.Instance.OnBonbonValueChanged += HandleBonbonValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnUnicornValueChanged += HandleUnicornValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnSmoothiesValueChanged += HandleSmoothiesValueChanged;
        }

        protected virtual void OnDisable()
        {
            PlayerDataManager.PlayerDataManager.Instance.OnBonbonValueChanged -= HandleBonbonValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnUnicornValueChanged -= HandleUnicornValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnSmoothiesValueChanged -= HandleSmoothiesValueChanged;
        }

        private void HandleSmoothiesValueChanged(int a_smoothiesPoints)
        {
            ResourcesWidget.SetSmoothiesAmount(a_smoothiesPoints);
        }

        private void HandleUnicornValueChanged(int a_numberOfUnicorns)
        {
            ResourcesWidget.SetUnicornAmount(a_numberOfUnicorns);
        }

        private void HandleBonbonValueChanged(int a_bonbons)
        {
            ResourcesWidget.SetBonbonAmount(a_bonbons);
        }
    }
}