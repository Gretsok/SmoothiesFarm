using TMPro;
using UnityEngine;

namespace SmoothiesFarm.SweetHunt
{
    public class ScoreDisplayWidget : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_scoreText = null;

        public void SetScore(int a_score)
        {
            m_scoreText.text = a_score.ToString();
        }
    }
}