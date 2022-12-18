using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.SweetHunt
{
    public class SweetHuntGameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Bonbon Bonbon;
        [SerializeField] private List<SpawnPoint> SpawnPoint;
        [SerializeField]
        private int m_numberOfBonbons;
        private bool m_gameHasEnded = false;
        private float m_timeOfStart = 0;

        private int m_score = 0;
        public int Score => m_score;

        public Action<int> OnScoreUpdated = null;

        public float TimeLeft => PlayerDataManager.PlayerDataManager.Instance.GameplayData.SweetHuntDuration - (Time.time - m_timeOfStart);
        void Start()
        {
            SpawnPoint?.ForEach(sp => sp.OnBonbonCaught += HandleBonbonCaught);

            for (int i = 0; i < 3; i++)
            {
                SpawnNewBonbon();
            }
            m_timeOfStart = Time.time;
        }

        private void Update()
        {
            if (Time.time - m_timeOfStart > PlayerDataManager.PlayerDataManager.Instance.GameplayData.SweetHuntDuration)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            if (m_gameHasEnded) return;
            m_gameHasEnded = true;

            PlayerDataManager.PlayerDataManager.Instance.AddBonbons(m_score);
            SceneManager.LoadSceneAsync(1);
        }


        public List<SpawnPoint> GetAvailableSpawnPoint()
        {
            return SpawnPoint.FindAll(s => s.IsAvailable);
        }

        public void HandleBonbonCaught()
        {
            m_score++;
            OnScoreUpdated?.Invoke(m_score);
            SpawnNewBonbon();
        }

        public void SpawnNewBonbon()
        {
            var availablePoints = GetAvailableSpawnPoint();
            int i = UnityEngine.Random.Range(0, availablePoints.Count);

            Bonbon bonbon = Instantiate(Bonbon, availablePoints[i].transform);
            bonbon.transform.localPosition = Vector3.zero;
            availablePoints[i].Setbonbon(bonbon);
        }

    }
}