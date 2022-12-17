using System;
using UnityEngine;

namespace SmoothiesFarm.Farm.FarmManager
{
    public class OwnedFarmCell : MonoBehaviour
    {
        public int x { get; set; }
        public int y { get; set; }

        [SerializeField]
        private GameObject m_frontFences = null;
        [SerializeField]
        private GameObject m_backFences = null;
        [SerializeField]
        private GameObject m_rightFences = null;
        [SerializeField]
        private GameObject m_leftFences = null;

        public void SetUpFences(bool a_hasFront, bool a_hasBack, bool a_hasRight, bool a_hasLeft)
        {
            m_frontFences.SetActive(a_hasFront);
            m_backFences.SetActive(a_hasBack);
            m_rightFences.SetActive(a_hasRight);
            m_leftFences.SetActive(a_hasLeft);
        }

        public Action<int, int> OnExtensionRequested = null;

        public void RequestTerrainExtension(int a_xDelta, int a_yDelta)
        {
            OnExtensionRequested?.Invoke(x + a_xDelta, y + a_yDelta);
        }
    }
}