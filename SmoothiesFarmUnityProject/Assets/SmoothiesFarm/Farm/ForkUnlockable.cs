using UnityEngine;

namespace SmoothiesFarm.Farm
{
    public class ForkUnlockable : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            if (!PlayerDataManager.PlayerDataManager.Instance.HasUnlockedKnife || PlayerDataManager.PlayerDataManager.Instance.HasUnlockedFork)
            {
                gameObject.SetActive(false);
            }
        }

        public void UnlockFork()
        {
            if (PlayerDataManager.PlayerDataManager.Instance.TryToPayInBonbons(PlayerDataManager.PlayerDataManager.Instance.GameplayData.BonbonCostToUnlockFork))
            {
                PlayerDataManager.PlayerDataManager.Instance.UnlockFork();
            }
            gameObject.SetActive(false);
        }
    }
}