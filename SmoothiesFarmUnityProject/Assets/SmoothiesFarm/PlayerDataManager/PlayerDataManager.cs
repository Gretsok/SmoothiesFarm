using System;
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
                OnSmoothiesValueChanged?.Invoke(m_smoothiesPoints);
            }
        }

        public int NumberOfUnicorns
        {
            get { return m_numberOfUnicorns; }

            private set
            {
                m_numberOfUnicorns = value;
                OnUnicornValueChanged?.Invoke(m_numberOfUnicorns);
            }
        }


        public Action<int> OnBonbonValueChanged = null;
        public Action<int> OnSmoothiesValueChanged = null;
        public Action<int> OnUnicornValueChanged = null;

        private void SetUpManager()
        {
            Bonbons = m_gameplayData.StartingNumberOfBonbons;
            m_smoothiesPoints = m_gameplayData.StartingNumberOfSmoothiersPoints;
            m_numberOfUnicorns = m_gameplayData.StartingNumberOfUnicorn;
        }

        public void SetUpNewScene(Farmer.FarmerCharacterMotor a_farmerMotor)
        {
            if(a_farmerMotor.TryGetComponent(out Farm.Breeding.FarmerBreedingController breedingController))
            {
                breedingController.SetBonbonsLeft(m_bonbons);
                breedingController.OnBonbonConsumed += HandleBonbonsConsumed;
            }
        }

        private void HandleBonbonsConsumed(int a_bonbonLeft)
        {
            Bonbons = a_bonbonLeft;
        }
    }
}