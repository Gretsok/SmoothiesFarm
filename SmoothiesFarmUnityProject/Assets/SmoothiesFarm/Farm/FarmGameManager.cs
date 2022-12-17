using UnityEngine;

namespace SmoothiesFarm.Farm
{
    public class FarmGameManager : MonoBehaviour
    {
        [SerializeField]
        private Farmer.FarmerCharacterMotor m_farmerMotor = null;
        [SerializeField]
        private FarmManager.FarmManager m_farmManager = null;

        private void Start()
        {
            PlayerDataManager.PlayerDataManager.Instance.SetUpNewScene(m_farmerMotor);
            m_farmManager.SetUpFarm(PlayerDataManager.PlayerDataManager.Instance.FarmCells);
        }

        private void OnDestroy()
        {
            PlayerDataManager.PlayerDataManager.Instance.SaveFarmCells(m_farmManager.OwnedCellsInfos);
        }

    }
}