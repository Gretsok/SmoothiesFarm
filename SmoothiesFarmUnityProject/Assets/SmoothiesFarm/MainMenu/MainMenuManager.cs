using UnityEngine;

namespace SmoothiesFarm.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Play()
        {
            Debug.Log("Play");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}