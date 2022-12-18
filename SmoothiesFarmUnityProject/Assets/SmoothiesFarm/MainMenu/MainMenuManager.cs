using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_credits = null;
        [SerializeField]
        private GameObject m_menu = null;


        private void Start()
        {
            HideCredits();
        }

        public void Play()
        {
            PlayerDataManager.PlayerDataManager.DeleteManager();
            SceneManager.LoadSceneAsync(1);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ShowCredits()
        {
            m_credits.SetActive(true);
            m_menu.SetActive(false);
        }

        public void HideCredits()
        {
            m_credits.SetActive(false);
            m_menu.SetActive(true);
        }
    }
}