using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.PlayerDataManager
{
    [CreateAssetMenu(fileName = "Gameplay Data", menuName = "SmoothiesFarm/PlayerDataManager/GameplayData")]
    public class GameplayData : ScriptableObject
    {
        [Header("Starting Values")]
        [SerializeField]
        private int m_startingNumberOfBonbons = 1;
        public int StartingNumberOfBonbons => m_startingNumberOfBonbons;
        [SerializeField]
        private int m_startingNumberOfSmoothiesPoints = 10;
        public int StartingNumberOfSmoothiersPoints => m_startingNumberOfSmoothiesPoints;
        [SerializeField]
        private int m_startingNumberOfUnicorn = 1;
        public int StartingNumberOfUnicorn => m_startingNumberOfUnicorn;
        [SerializeField]
        private List<Farm.FarmManager.SFarmCellInfos> m_startingFarmCells = null;
        public List<Farm.FarmManager.SFarmCellInfos> StartingFarmCells => m_startingFarmCells;

        [Header("Costs")]
        [SerializeField]
        private int m_costToPlayOnArcadeMachine = 10;
        public int CostToPlayOnArcadeMachine => m_costToPlayOnArcadeMachine;

    }
}