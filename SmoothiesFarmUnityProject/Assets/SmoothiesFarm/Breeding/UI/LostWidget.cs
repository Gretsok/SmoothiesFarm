using TMPro;
using UnityEngine;

namespace SmoothiesFarm.Farm.Breeding.UI
{
    public class LostWidget : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_widgetContainer = null;
        [SerializeField]
        private TextMeshProUGUI m_gameStats = null;

        private void Awake()
        {
            m_widgetContainer.SetActive(false);
        }

        public void DisplayWidget()
        {
            m_widgetContainer.SetActive(true);
            m_gameStats.text = $"You survived {PlayerDataManager.PlayerDataManager.Instance.RoundSurvived} deliverings and earned a total of {PlayerDataManager.PlayerDataManager.Instance.TotalSmoothies} smoothies points !";
        }
    }
}