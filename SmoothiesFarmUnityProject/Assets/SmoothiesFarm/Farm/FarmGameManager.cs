using UnityEngine;

namespace SmoothiesFarm.Farm
{
    public class FarmGameManager : MonoBehaviour
    {
        [SerializeField]
        private Farmer.FarmerCharacterMotor m_farmerMotor = null;
        [SerializeField]
        private Farmer.UI.FarmerUIManager m_farmerUIManager = null;

        private void Start()
        {
            PlayerDataManager.PlayerDataManager.Instance.SetUpNewScene(m_farmerMotor);
            m_farmerUIManager.ResourcesWidget.SetBonbonAmount(PlayerDataManager.PlayerDataManager.Instance.Bonbons);
            m_farmerUIManager.ResourcesWidget.SetUnicornAmount(PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns);
            m_farmerUIManager.ResourcesWidget.SetSmoothiesAmount(PlayerDataManager.PlayerDataManager.Instance.SmoothiesPoint);
        }

        private void OnEnable()
        {
            PlayerDataManager.PlayerDataManager.Instance.OnBonbonValueChanged += HandleBonbonValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnUnicornValueChanged += HandleUnicornValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnSmoothiesValueChanged += HandleSmoothiesValueChanged;
        }

        private void OnDisable()
        {
            PlayerDataManager.PlayerDataManager.Instance.OnBonbonValueChanged -= HandleBonbonValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnUnicornValueChanged -= HandleUnicornValueChanged;
            PlayerDataManager.PlayerDataManager.Instance.OnSmoothiesValueChanged -= HandleSmoothiesValueChanged;
        }

        private void HandleSmoothiesValueChanged(int a_smoothiesPoints)
        {
            m_farmerUIManager.ResourcesWidget.SetSmoothiesAmount(a_smoothiesPoints);
        }

        private void HandleUnicornValueChanged(int a_numberOfUnicorns)
        {
            m_farmerUIManager.ResourcesWidget.SetUnicornAmount(a_numberOfUnicorns);
        }

        private void HandleBonbonValueChanged(int a_bonbons)
        {
            m_farmerUIManager.ResourcesWidget.SetBonbonAmount(a_bonbons);
        }
    }
}