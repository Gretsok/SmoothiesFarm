using System;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.Farm.FarmManager
{
    public class FarmManager : MonoBehaviour
    {
        [Header("Grid")]
        [SerializeField]
        private OwnedFarmCell m_ownedFarmCellPrefab = null;
        [SerializeField]
        private CloudFarmCell m_cloudFarmCellPrefab = null;
        [SerializeField]
        private int m_columnsOfCellToAddOnEachSide = 7;
        [SerializeField]
        private int m_linesToAdd = 15;
        [SerializeField]
        private OwnedFarmCell m_firstOwnedFarm = null;
        private List<OwnedFarmCell> m_instantiatedOwnedFarmCell = null;
        private List<CloudFarmCell> m_instantiatedCloudFarmCell = null;

        private List<SFarmCellInfos> m_ownedCellsInfos = null;
        public List<SFarmCellInfos> OwnedCellsInfos => m_ownedCellsInfos;

        [SerializeField]
        private Unicorn.UnicornCharacterMotor m_unicornMotorPrefab = null;
        private List<Unicorn.UnicornCharacterMotor> m_instantiatedUnicorns = new List<Unicorn.UnicornCharacterMotor>();

        private void Start()
        {
            SetUpFarm(PlayerDataManager.PlayerDataManager.Instance.FarmCells);
        }

        private void OnDestroy()
        {
            PlayerDataManager.PlayerDataManager.Instance.SaveFarmCells(OwnedCellsInfos);
        }

        private void SetUpFarm(List<SFarmCellInfos> a_ownedCellInfos)
        {
            m_instantiatedOwnedFarmCell = new List<OwnedFarmCell>();
            m_instantiatedCloudFarmCell = new List<CloudFarmCell>();

            m_instantiatedOwnedFarmCell.Add(m_firstOwnedFarm);
            m_firstOwnedFarm.x = 0;
            m_firstOwnedFarm.y = 0;

            for(int x = -m_columnsOfCellToAddOnEachSide; x <= m_columnsOfCellToAddOnEachSide; ++x)
            {
                for(int y = 0; y <= m_linesToAdd; ++y)
                {
                    if(x != 0 || y != 0)
                    {
                        if (a_ownedCellInfos.Find(c => c.x == x && c.y == y) != null)
                        {
                            var cell = Instantiate(m_ownedFarmCellPrefab,
                                m_firstOwnedFarm.transform.position + m_firstOwnedFarm.transform.right * x * 10f + m_firstOwnedFarm.transform.forward * y * 10f,
                                m_firstOwnedFarm.transform.rotation,
                                transform);
                            cell.x = x;
                            cell.y = y;
                            m_instantiatedOwnedFarmCell.Add(cell);
                        }
                        else
                        {
                            var cloud = Instantiate(m_cloudFarmCellPrefab,
                                m_firstOwnedFarm.transform.position + m_firstOwnedFarm.transform.right * x * 10f + m_firstOwnedFarm.transform.forward * y * 10f,
                                m_firstOwnedFarm.transform.rotation,
                                transform);
                            cloud.x = x;
                            cloud.y = y;
                            m_instantiatedCloudFarmCell.Add(cloud);
                        }
                    }
                }
            }

            for(int i = 0; i < m_instantiatedOwnedFarmCell.Count; ++i)
            {
                bool hasFront, hasBack, hasRight, hasLeft = false;
                hasFront = a_ownedCellInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x && c.y == m_instantiatedOwnedFarmCell[i].y + 1) == null;
                hasBack = a_ownedCellInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x && c.y == m_instantiatedOwnedFarmCell[i].y - 1) == null;
                hasRight = a_ownedCellInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x + 1 && c.y == m_instantiatedOwnedFarmCell[i].y) == null;
                hasLeft = a_ownedCellInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x - 1 && c.y == m_instantiatedOwnedFarmCell[i].y) == null;
                m_instantiatedOwnedFarmCell[i].SetUpFences(hasFront, hasBack, hasRight, hasLeft);

                m_instantiatedOwnedFarmCell[i].OnExtensionRequested += HandleExtensionRequested;
            }

            // a_ownedCellInfos.RemoveAll(c => c.x == 0 && c.y == 0);
            m_ownedCellsInfos = a_ownedCellInfos;


            for(int i = 0; i < PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns; ++i)
            {
                AddUnicorn();
            }
        }

        public void AddUnicorn()
        {
            int randCell = UnityEngine.Random.Range(0, m_instantiatedOwnedFarmCell.Count);
            var unicorn = Instantiate(m_unicornMotorPrefab,
                m_instantiatedOwnedFarmCell[randCell].transform.position + Vector3.up
                + m_instantiatedOwnedFarmCell[randCell].transform.forward * UnityEngine.Random.Range(-3f, 3f)
                + m_instantiatedOwnedFarmCell[randCell].transform.right * UnityEngine.Random.Range(-3f, 3f),
                Quaternion.identity,
                transform);
            m_instantiatedUnicorns.Add(unicorn);
        }

        private void HandleExtensionRequested(int x, int y)
        {
            if (m_ownedCellsInfos.Find(c => c.x == x && c.y == y) != null) return;
            if (y < 0 || x < -m_columnsOfCellToAddOnEachSide || x > m_columnsOfCellToAddOnEachSide) return;
            if (!PlayerDataManager.PlayerDataManager.Instance.TryToPayInSmoothies(PlayerDataManager.PlayerDataManager.Instance.GameplayData.CostToExtendFarm)) return;

            var cell = Instantiate(m_ownedFarmCellPrefab,
                                m_firstOwnedFarm.transform.position + m_firstOwnedFarm.transform.right * x * 10f + m_firstOwnedFarm.transform.forward * y * 10f,
                                m_firstOwnedFarm.transform.rotation,
                                transform);
            cell.x = x;
            cell.y = y;
            m_instantiatedOwnedFarmCell.Add(cell);

            SFarmCellInfos newCellInfos = new SFarmCellInfos();
            newCellInfos.x = x;
            newCellInfos.y = y;
            m_ownedCellsInfos.Add(newCellInfos);

            cell.OnExtensionRequested += HandleExtensionRequested;

            var cloud = m_instantiatedCloudFarmCell.Find(c => c.x == x && c.y == y);
            if (cloud)
                Destroy(cloud.gameObject);

            for (int i = 0; i < m_instantiatedOwnedFarmCell.Count; ++i)
            {
                bool hasFront, hasBack, hasRight, hasLeft = false;
                hasFront = m_ownedCellsInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x && c.y == m_instantiatedOwnedFarmCell[i].y + 1) == null;
                hasBack = m_ownedCellsInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x && c.y == m_instantiatedOwnedFarmCell[i].y - 1) == null;
                hasRight = m_ownedCellsInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x + 1 && c.y == m_instantiatedOwnedFarmCell[i].y) == null;
                hasLeft = m_ownedCellsInfos.Find(c => c.x == m_instantiatedOwnedFarmCell[i].x - 1 && c.y == m_instantiatedOwnedFarmCell[i].y) == null;
                m_instantiatedOwnedFarmCell[i].SetUpFences(hasFront, hasBack, hasRight, hasLeft);
            }

            PlayerDataManager.PlayerDataManager.Instance.SaveFarmCells(OwnedCellsInfos);
        }
    }
}