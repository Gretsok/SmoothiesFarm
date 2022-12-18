using UnityEngine;

namespace SmoothiesFarm.Farmer.Equipment
{
    public class FarmerEquipmentController : MonoBehaviour
    {
        [SerializeField]
        private bool m_hasWeapon = false;
        [SerializeField]
        private GameObject m_hand = null;
        [SerializeField]
        private GameObject m_baseBall = null;
        [SerializeField]
        private GameObject m_knife = null;
        [SerializeField]
        private GameObject m_fork = null;

        private void Awake()
        {
            if(m_hasWeapon)
            {
                if(PlayerDataManager.PlayerDataManager.Instance.HasUnlockedFork)
                {
                    EquipFork();
                }
                else if(PlayerDataManager.PlayerDataManager.Instance.HasUnlockedKnife)
                {
                    EquipKnife();
                }
                else
                {
                    EquipeBaseBall();
                }
            }
            else
            {
                EquipHand();
            }
        }

        private void EquipHand()
        {
            m_hand.SetActive(true);
            m_baseBall.SetActive(false);
            m_knife.SetActive(false);
            m_fork.SetActive(false);
        }

        private void EquipeBaseBall()
        {
            m_hand.SetActive(false);
            m_baseBall.SetActive(true);
            m_knife.SetActive(false);
            m_fork.SetActive(false);
        }

        private void EquipKnife()
        {
            m_hand.SetActive(false);
            m_baseBall.SetActive(false);
            m_knife.SetActive(true);
            m_fork.SetActive(false);
        }

        private void EquipFork()
        {
            m_hand.SetActive(false);
            m_baseBall.SetActive(false);
            m_knife.SetActive(false);
            m_fork.SetActive(true);
        }
    }
}