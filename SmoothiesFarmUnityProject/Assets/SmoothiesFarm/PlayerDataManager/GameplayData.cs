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
        [SerializeField]
        private int m_costToExtendFarm = 5;
        public int CostToExtendFarm => m_costToExtendFarm;
        [SerializeField]
        private int m_costToBuyFirstUnicorn = 1;
        public int CostToBuyFirstUnicorn => m_costToBuyFirstUnicorn;

        [SerializeField]
        private int m_bonbonCostToUnlockKnife = 2;
        public int BonbonCostToUnlockKnife => m_bonbonCostToUnlockKnife;
        [SerializeField]
        private int m_bonbonCostToUnlockFork = 4;
        public int BonbonCostToUnlockFork => m_bonbonCostToUnlockFork;

        [Header("Reward")]
        [SerializeField]
        private int m_smoothiesPerUnicornKilled = 2;
        public int SmoothiesPerUnicornKilled => m_smoothiesPerUnicornKilled;

        [Header("Misc Rules")]
        [SerializeField]
        private float m_timeBetweenDelivering = 60f;
        public float TimeBetweenDelivering => m_timeBetweenDelivering;
        [SerializeField]
        private int m_unicornPerFarmCell = 10;
        public int UnicornPerFarmCell => m_unicornPerFarmCell;
        [SerializeField]
        private float m_sweetHuntDuration = 10f;
        public float SweetHuntDuration => m_sweetHuntDuration;

    }
}