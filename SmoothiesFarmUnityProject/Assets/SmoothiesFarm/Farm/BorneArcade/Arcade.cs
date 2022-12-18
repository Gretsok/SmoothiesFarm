using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.Farm
{
    public class Arcade : MonoBehaviour
    {
        public void StartMinigame()
        {
            Debug.Log("Minigame !!! Yeye !!!");
            if (PlayerDataManager.PlayerDataManager.Instance.TryToPay(PlayerDataManager.PlayerDataManager.Instance.GameplayData.CostToPlayOnArcadeMachine))
            {
                SceneManager.LoadSceneAsync(3);
            }
        }
    }
}