using UnityEngine;

namespace SmoothiesFarm.SweetHunt
{
    public class SweetHuntUIManager : MonoBehaviour
    {
        [SerializeField]
        private SweetHuntGameManager m_gameManager = null;

        [SerializeField]
        private ScoreDisplayWidget m_scoreDisplayWidget = null;
        [SerializeField]
        private TimeLeftWidget m_timeLeftWidget = null;

        private void OnEnable()
        {
            m_gameManager.OnScoreUpdated += HandleScoreUpdated;
            HandleScoreUpdated(m_gameManager.Score);

        }

        private void OnDisable()
        {
            m_gameManager.OnScoreUpdated -= HandleScoreUpdated;
        }

        private void HandleScoreUpdated(int obj)
        {
            m_scoreDisplayWidget.SetScore(obj);
        }

        private void Update()
        {
            m_timeLeftWidget.SetTimeLeft(m_gameManager.TimeLeft);
        }
    }
}