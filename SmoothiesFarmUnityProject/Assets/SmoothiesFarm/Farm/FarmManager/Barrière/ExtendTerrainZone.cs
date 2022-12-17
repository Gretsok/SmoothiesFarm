using UnityEngine;

namespace SmoothiesFarm.Farm.FarmManager
{
    public class ExtendTerrainZone : MonoBehaviour
    {
        [SerializeField]
        private OwnedFarmCell m_farmCell = null;
        [SerializeField]
        private Vector2Int m_requestOffset = default;
        public void RequestTerrainExtension()
        {
            m_farmCell.RequestTerrainExtension(m_requestOffset.x, m_requestOffset.y);
        }
    }
}