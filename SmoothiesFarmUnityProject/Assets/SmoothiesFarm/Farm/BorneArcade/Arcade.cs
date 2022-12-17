using UnityEngine;

namespace SmoothiesFarm.Farm
{
    public class Arcade : MonoBehaviour
    {
        public void StartMinigame()
        {
            Debug.Log("Minigame !!! Yeye !!!");
            if (PlayerDataManager.PlayerDataManager.Instance.TryToPay(PlayerDataManager.PlayerDataManager.Instance.GameplayData.CostToPlayOnArcadeMachine))
            {
                PlayerDataManager.PlayerDataManager.Instance.AddBonbons(PlayerDataManager.PlayerDataManager.Instance.GameplayData.TEMP_REWARD_ARCADE);
            }
        }
    }
}