using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsAvailable = true;
    public Bonbon bonbon;
    void Start()
    {
        
    }

    private void Update()
    {
         
    }
    public void Setbonbon(Bonbon b)
    {
        bonbon = b;
        bonbon.OnBonbonCaught += HandleBonbonCaught;
    }


    public void HandleBonbonCaught()
    {
        IsAvailable = false;
    }
}
