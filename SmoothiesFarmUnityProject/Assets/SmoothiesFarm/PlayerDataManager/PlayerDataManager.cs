using System;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.PlayerDataManager
{
    public class PlayerDataManager : MonoBehaviour
    {
        #region Singleton
        private static PlayerDataManager s_playerDataManager = null;

        private void Awake()
        {
            if(s_playerDataManager && s_playerDataManager != this)
            {
                Destroy(gameObject);
                return;
            }           
        }

        public static PlayerDataManager Instance
        {
            get
            {
                if(!s_playerDataManager)
                {
                    var prefab = Resources.Load<PlayerDataManager>("PlayerDataManager");
                    s_playerDataManager = Instantiate(prefab);
                    DontDestroyOnLoad(s_playerDataManager);
                    s_playerDataManager.SetUpManager();
                }
                return s_playerDataManager;
            }
        }

        public static void DeleteManager()
        {
            if (s_playerDataManager)
            {
                Destroy(s_playerDataManager.gameObject);
            }
            s_playerDataManager = null;
        }
        #endregion

        [SerializeField]
        private GameplayData m_gameplayData = null;
        public GameplayData GameplayData => m_gameplayData;

        private int m_bonbons = 0;
        private int m_smoothiesPoints = 0;
        private int m_numberOfUnicorns = 0;

        private int m_roundSurvived = 0;
        public int RoundSurvived => m_roundSurvived;
        private List<Farm.FarmManager.SFarmCellInfos> m_farmCells = null;
        public List<Farm.FarmManager.SFarmCellInfos> FarmCells => m_farmCells;

        private float m_timeOfEndOfLastDelivering = 0;
        public float TimeOfEndOfLastDelivering => m_timeOfEndOfLastDelivering;
        private int m_totalSmoothies = 0;
        public int TotalSmoothies => m_totalSmoothies;
        public int Bonbons 
        { 
            get { return m_bonbons; } 
            
            private set 
            {
                m_bonbons = value;
                OnBonbonValueChanged?.Invoke(m_bonbons);
            } 
        }

        public int SmoothiesPoint
        {
            get { return m_smoothiesPoints; }

            private set
            {
                m_smoothiesPoints = value;
                m_totalSmoothies += value;
                OnSmoothiesValueChanged?.Invoke(m_smoothiesPoints);
            }
        }

        public int NumberOfUnicorns
        {
            get { return m_numberOfUnicorns; }

            private set
            {
                m_numberOfUnicorns = value;
                if(m_numberOfUnicorns < 0)
                {
                    m_numberOfUnicorns = 0;
                }
                OnUnicornValueChanged?.Invoke(m_numberOfUnicorns);
            }
        }


        public Action<int> OnBonbonValueChanged = null;
        public Action<int> OnSmoothiesValueChanged = null;
        public Action<int> OnUnicornValueChanged = null;
        public Action OnFarmCellsUpdated = null;

        private void SetUpManager()
        {
            Bonbons = m_gameplayData.StartingNumberOfBonbons;
            m_smoothiesPoints = m_gameplayData.StartingNumberOfSmoothiersPoints;
            m_numberOfUnicorns = m_gameplayData.StartingNumberOfUnicorn;
            m_farmCells = new List<Farm.FarmManager.SFarmCellInfos>(m_gameplayData.StartingFarmCells);
            m_timeOfEndOfLastDelivering = Time.time;
        }
        public void AddBonbons(int a_bonbonToAdd)
        {
            Bonbons += a_bonbonToAdd;
        }


        public void ConsumeBonbon()
        {
            --Bonbons;
        }

        public void SaveFarmCells(List<Farm.FarmManager.SFarmCellInfos> a_cells)
        {
            m_farmCells = a_cells;
            OnFarmCellsUpdated?.Invoke();
        }

        public bool CanAddMoreUnicorn()
        {
            return m_numberOfUnicorns < GameplayData.UnicornPerFarmCell * FarmCells.Count;
        }

        public void AddUnicorn()
        {
            ++NumberOfUnicorns;
        }

        public void RemoveUnicorn()
        {
            --NumberOfUnicorns;
        }

        public bool TryToPayInSmoothies(int a_amountOfSmoothies)
        {
            if(m_smoothiesPoints >= a_amountOfSmoothies)
            {
                SmoothiesPoint -= a_amountOfSmoothies;
                return true;
            }
            return false;
        }

        public bool TryToPayInBonbons(int a_amountOfBonbons)
        {
            if (m_bonbons >= a_amountOfBonbons)
            {
                Bonbons -= a_amountOfBonbons;
                return true;
            }
            return false;
        }

        public void AddSmoothiesPoint(int a_smoothiesPoints)
        {
            SmoothiesPoint += a_smoothiesPoints;
        }

        public void NotifyEndOfDelivering()
        {
            m_timeOfEndOfLastDelivering = Time.time;
            ++m_roundSurvived;
        }

        private bool m_hasUnlockedKnife = false;
        private bool m_hasUnlockedFork = false;
        public bool HasUnlockedKnife => m_hasUnlockedKnife;
        public bool HasUnlockedFork => m_hasUnlockedFork;

        public void UnlockKnife()
        {
            m_hasUnlockedKnife = true;
        }

        public void UnlockFork()
        {
            m_hasUnlockedFork = true;
        }

    }
}