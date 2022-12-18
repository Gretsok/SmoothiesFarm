using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.Farm
{
    public class FarmGameManager : MonoBehaviour
    {
        [SerializeField]
        private FarmManager.FarmManager m_farmManager = null;


        private bool m_hasLost = false;
        private bool m_deliveringTimeStarted = false;

        public Action OnLost = null;
        public Action OnDeliveringTime = null;

        private void Start()
        {
            m_deliveringTimeStarted = false;
            if (PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns <= 0)
            {
                if (PlayerDataManager.PlayerDataManager.Instance.TryToPay(PlayerDataManager.PlayerDataManager.Instance.GameplayData.CostToBuyFirstUnicorn))
                {
                    PlayerDataManager.PlayerDataManager.Instance.AddUnicorn();
                    m_farmManager.AddUnicorn();
                }
                else
                {
                    Lose();
                }
            }
            
            
        }

        private async void Lose()
        {
            m_hasLost = true;
            OnLost?.Invoke();
            await Task.Delay(5000);
            SceneManager.LoadSceneAsync(0);
        }

        private void Update()
        {
            if (m_deliveringTimeStarted) return;
            if (m_hasLost) return;
            if(Time.time - PlayerDataManager.PlayerDataManager.Instance.TimeOfEndOfLastDelivering 
                > PlayerDataManager.PlayerDataManager.Instance.GameplayData.TimeBetweenDelivering)
            {
                Deliver();
            }
        }

        private void Deliver()
        {
            Debug.Log("Deliver !!");
            m_deliveringTimeStarted = true;
            DeliveringRoutine();
        }

        private async void DeliveringRoutine()
        {
            OnDeliveringTime?.Invoke();
            await Task.Delay(3000);
            SceneManager.LoadSceneAsync(2);
        }
    }
}