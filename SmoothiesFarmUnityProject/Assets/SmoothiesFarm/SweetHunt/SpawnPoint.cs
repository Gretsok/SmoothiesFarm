using System;
using UnityEngine;

namespace SmoothiesFarm.SweetHunt
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsAvailable { get; private set; } = true;
        public Action OnBonbonCaught = null;

        public void Setbonbon(Bonbon b)
        {
            IsAvailable = false;
            b.OnBonbonCaught += HandleBonbonCaught;
        }


        public void HandleBonbonCaught()
        {
            OnBonbonCaught?.Invoke();
            IsAvailable = true;
        }
    }
}