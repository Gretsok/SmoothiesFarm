using UnityEngine;
using System;
using SmoothiesFarm.Farmer;

namespace SmoothiesFarm.SweetHunt
{
    public class Bonbon : MonoBehaviour
    {
        public Action OnBonbonCaught;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FarmerCharacterMotor characterMotor))
            {
                OnBonbonCaught?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}