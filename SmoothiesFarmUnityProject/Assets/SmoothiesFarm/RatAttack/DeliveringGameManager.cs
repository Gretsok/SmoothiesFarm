using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.RatAttack
{
    public class DeliveringGameManager : MonoBehaviour
    {
        [Header("Terrain Generation")]
        [SerializeField]
        private float m_numberOfUnicornsPerTile = 5;
        private List<GameObject> m_terrainTiles = null;
        [SerializeField]
        private GameObject m_tilePrefab = null;

        [Header("Unicorn Instantiation")]
        [SerializeField]
        private Farm.Unicorn.UnicornCharacterMotor unicornPrefab;
        private List<Farm.Unicorn.UnicornCharacterMotor> m_instantiatedUnicorns = null;

        [Header("Unicorn Death Feedback")]
        [SerializeField]
        private ParticleSystem m_unicornDeathVFX = null;


        [Header("Rats Instantiation")]
        [SerializeField]
        private AnimationCurve m_spawnRateCurve = AnimationCurve.Linear(0f, 3f, 1f, 0.5f);
        [SerializeField]
        private float m_timeToReachMaxRate = 10f;

        [Header("Rats Death Feedback")]
        [SerializeField]
        private ParticleSystem m_ratDeathVFX = null;

        [Header("Smoothies Management")]
        [SerializeField]
        private int m_smoothiesToEarnPerUnicornKilled = 1;
        private int m_totalSmoothiesEarned = 0;

        

        private void Start()
        {
            GenerateTerrain();
            InstantiateUnicorns();
        }

        private void InstantiateUnicorns()
        {
            m_instantiatedUnicorns = new List<SmoothiesFarm.Farm.Unicorn.UnicornCharacterMotor>();
            for (int i = 0; i < PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns; i++)
            {
                int randCell = Random.Range(0, m_terrainTiles.Count);
                var unicorn = Instantiate(unicornPrefab,
                            m_terrainTiles[randCell].transform.position + Vector3.up
                            + m_terrainTiles[randCell].transform.forward * Random.Range(-3f, 3f)
                            + m_terrainTiles[randCell].transform.right * Random.Range(-3f, 3f),
                            Quaternion.identity,
                            transform);
                m_instantiatedUnicorns.Add(unicorn);
                unicorn.GetComponent<HealthHandlingController>().OnDeath += HandleUnicordDeath;
            }
        }

        private void HandleUnicordDeath(HealthHandlingController obj, bool a_fromPlayer)
        {
            if(m_unicornDeathVFX)
            {
                var vfx = Instantiate(m_unicornDeathVFX, obj.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                vfx.Play();
            }
            m_instantiatedUnicorns.Remove(obj.GetComponent<Farm.Unicorn.UnicornCharacterMotor>());
            if(a_fromPlayer)
            {
                m_totalSmoothiesEarned += m_smoothiesToEarnPerUnicornKilled;
            }
            Destroy(obj.gameObject);

            if(m_instantiatedUnicorns.Count <= 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            SceneManager.LoadSceneAsync(1);
            PlayerDataManager.PlayerDataManager.Instance.AddSmoothiesPoint(m_totalSmoothiesEarned);
            PlayerDataManager.PlayerDataManager.Instance.NotifyEndOfDelivering();
        }

        private void GenerateTerrain()
        {
            float numberOfTiles = PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns / m_numberOfUnicornsPerTile;
            int size = Mathf.RoundToInt(Mathf.Sqrt(numberOfTiles)) + 1; // We had one to give a bit more space
            m_terrainTiles = new List<GameObject>();

            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    var tile = Instantiate(m_tilePrefab,
                        transform.position + transform.forward * y * 10f + transform.right * x * 10f,
                        Quaternion.identity,
                        transform);
                    m_terrainTiles.Add(tile);
                }
            }
        }
    }
}