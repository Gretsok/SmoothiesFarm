using System;
using UnityEngine;

namespace SmoothiesFarm.Farm.FarmManager
{
    public class FarmInfoDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text m_text = null;
        [SerializeField]
        private Color m_normalColor = Color.white;
        [SerializeField]
        private Color m_fullColor = Color.red;

        private void OnEnable()
        {
            PlayerDataManager.PlayerDataManager.Instance.OnUnicornValueChanged += UpdateDisplay;
            PlayerDataManager.PlayerDataManager.Instance.OnFarmCellsUpdated += HandleFarmCellsUpdated;
            UpdateDisplay(PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns);
        }


        private void OnDisable()
        {
            PlayerDataManager.PlayerDataManager.Instance.OnUnicornValueChanged -= UpdateDisplay;
            PlayerDataManager.PlayerDataManager.Instance.OnFarmCellsUpdated -= HandleFarmCellsUpdated;
        }

        private void Update()
        {
            // transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));
            transform.rotation = Camera.main.transform.rotation;
        }


        private void HandleFarmCellsUpdated()
        {
            UpdateDisplay(PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns);
        }

        private void UpdateDisplay(int a_numberOfUnicorns)
        {
            if (a_numberOfUnicorns >=
                PlayerDataManager.PlayerDataManager.Instance.GameplayData.UnicornPerFarmCell 
                * PlayerDataManager.PlayerDataManager.Instance.FarmCells.Count)
            {
                m_text.color = m_fullColor;
            }
            else
            { 
                m_text.color = m_normalColor;
            }

            m_text.text = $"{a_numberOfUnicorns}/{PlayerDataManager.PlayerDataManager.Instance.GameplayData.UnicornPerFarmCell * PlayerDataManager.PlayerDataManager.Instance.FarmCells.Count} unicorns";
        }
    }
}
