using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornManager : MonoBehaviour
{
    [SerializeField]
    private int unicornAmount = 1;

    [SerializeField]
    private float radius = 5f;

    [SerializeField]
    private GameObject unicornPrefab;

    private Transform instantiatePoint;


    private void Start()
    {
        for(int i = 0; i < unicornAmount; i++){
            Vector3 randomPoint = Random.insideUnitCircle * radius;
            randomPoint.z = transform.position.z;
            Instantiate(unicornPrefab, randomPoint, Quaternion.identity);
        }
    }
}
