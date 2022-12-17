using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Play()
        {
            PlayerDataManager.PlayerDataManager.DeleteManager();
            SceneManager.LoadSceneAsync(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}