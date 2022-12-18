using SmoothiesFarm.RatAttack.Rat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothiesFarm.RatAttack
{
    public class DeliveringGameManager : MonoBehaviour
    {
        private float m_timeOfStart = 0;

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
        private RatCharacterMotor m_ratPrefab = null;
        [SerializeField]
        private AnimationCurve m_spawnCooldownPerTwoUnicornCurve = AnimationCurve.Linear(0f, 5f, 1f, 2f);
        [SerializeField]
        private float m_timeToReachMaxRate = 10f;
        private float m_lastTimeRatSpawned = 0;
        private List<RatCharacterMotor> m_instantiatedRats = null;

        [Header("Rats Death Feedback")]
        [SerializeField]
        private ParticleSystem m_ratDeathVFX = null;

        [Header("Smoothies Management")]
        [SerializeField]
        private int m_smoothiesToEarnPerUnicornKilled = 1;
        private int m_totalSmoothiesEarned = 0;

        

        private void Start()
        {
            m_timeOfStart = Time.time;
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

        private void Update()
        {
            float currentSpawnCooldown = m_spawnCooldownPerTwoUnicornCurve.Evaluate((Time.time - m_timeOfStart) / m_timeToReachMaxRate);
            if(Time.time - m_lastTimeRatSpawned > currentSpawnCooldown)
            {
                SpawnRat();
            }
        }

        private void SpawnRat()
        {
            if (m_instantiatedRats == null) 
                m_instantiatedRats = new List<RatCharacterMotor>();

            for(int i = 0; i < PlayerDataManager.PlayerDataManager.Instance.NumberOfUnicorns / 2f; ++i)
            {
                int randCell = Random.Range(0, m_terrainTiles.Count);
                var rat = Instantiate(m_ratPrefab,
                    m_terrainTiles[randCell].transform.position + Vector3.up
                    + m_terrainTiles[randCell].transform.forward * Random.Range(-3f, 3f)
                    + m_terrainTiles[randCell].transform.right * Random.Range(-3f, 3f),
                    Quaternion.identity,
                    transform);
                rat.GetComponent<HealthHandlingController>().OnDeath += HandleRatDeath;
                rat.GetComponent<Rat.RatController>().OnTargetKilled += HandleRatTargetKilled;
                rat.GetComponent<Rat.RatController>().SetTarget(GetRandomUnicorn());
            }
            
            m_lastTimeRatSpawned = Time.time;
        }

        private void HandleRatTargetKilled(RatController obj)
        {
            obj.SetTarget(GetRandomUnicorn());
        }

        private void HandleRatDeath(HealthHandlingController arg1, bool arg2)
        {
            if (m_ratDeathVFX)
            {
                var vfx = Instantiate(m_ratDeathVFX, arg1.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                vfx.Play();
            }
            m_instantiatedRats.Remove(arg1.GetComponent<RatCharacterMotor>());
            Destroy(arg1.gameObject);
        }

        private Farm.Unicorn.UnicornCharacterMotor GetRandomUnicorn()
        {
            if(m_instantiatedUnicorns.Count > 0)
                return m_instantiatedUnicorns[Random.Range(0, m_instantiatedUnicorns.Count)];
            return null;
        }
    }
}