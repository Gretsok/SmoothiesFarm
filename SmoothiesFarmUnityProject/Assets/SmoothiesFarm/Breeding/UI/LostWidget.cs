using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.Farm.Breeding.UI
{
    public class LostWidget : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_widgetContainer = null;

        private void Awake()
        {
            m_widgetContainer.SetActive(false);
        }

        public void DisplayWidget()
        {
            m_widgetContainer.SetActive(true);
        }
    }
}