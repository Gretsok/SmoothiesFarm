using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Bonbon Bonbon;
    [SerializeField] private List<SpawnPoint> SpawnPoint;
    private List<SpawnPoint> _SpawnPoint;
    private int BonbonNb;

    void Start()
    {
        // choose 3 spawn point
        for (int i = 0; i < 3; i++)
        {
            SpawnNewBonbon();
        }
    }

    public void GetAvailableSpawnPoint()
    {
        _SpawnPoint= SpawnPoint.FindAll(s => s.IsAvailable);           
    }

    public void HandleBonbonCaught()
    {
        SpawnNewBonbon();
    }

    //fait spawnner un nouveau bonbon à l'emplacement d'un spwnpoint dispo puis rend le spawnpoint indisponible 
    public void SpawnNewBonbon()
    {
        GetAvailableSpawnPoint();
        int i = Random.Range(0, _SpawnPoint.Count - 1);
        
        Bonbon bonbon =  Instantiate(Bonbon, _SpawnPoint[i].transform);
        bonbon.transform.localPosition = Vector3.zero;
        _SpawnPoint[i].Setbonbon(bonbon);
        SpawnPoint.Find(S => S = _SpawnPoint[i]).IsAvailable =false;
    }

}
