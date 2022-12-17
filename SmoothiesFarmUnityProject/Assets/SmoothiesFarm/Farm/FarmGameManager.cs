using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.Farm
{
    public class FarmGameManager : MonoBehaviour
    {
        private bool m_deliveringTimeStarted = false;

        private void Start()
        {
            m_deliveringTimeStarted = false;
        }


        private void Update()
        {
            if (m_deliveringTimeStarted) return;
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
            await System.Threading.Tasks.Task.Yield();
            SceneManager.LoadSceneAsync(2);
        }
    }
}