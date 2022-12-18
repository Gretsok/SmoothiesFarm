using UnityEngine;

namespace SmoothiesFarm.Farm
{
    public class CutoUnlockable : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            if (PlayerDataManager.PlayerDataManager.Instance.HasUnlockedKnife)
            {
                gameObject.SetActive(false);
            }
        }

        public void UnlockKnife()
        {
            if(PlayerDataManager.PlayerDataManager.Instance.TryToPayInBonbons(PlayerDataManager.PlayerDataManager.Instance.GameplayData.BonbonCostToUnlockKnife))
            {
                PlayerDataManager.PlayerDataManager.Instance.UnlockKnife();
            }
            gameObject.SetActive(false);
        }
    }
}