using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Bonbon : MonoBehaviour
{

    public Action OnBonbonCaught;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FarmerCharacter"))
        {
            Destroy(this);
            OnBonbonCaught.Invoke();
        }
    }


    // qd detruit -> on bonbon caught invoke -> pour envoyer la donné 


}
