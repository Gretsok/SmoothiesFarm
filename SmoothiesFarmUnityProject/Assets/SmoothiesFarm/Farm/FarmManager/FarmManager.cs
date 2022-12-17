using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.Farm.FarmManager
{
    public class FarmManager : MonoBehaviour
    {
        [SerializeField]
        private Unicorn.UnicornCharacterMotor m_unicornMotorPrefab = null;
        private List<Unicorn.UnicornCharacterMotor> m_instantiatedUnicorns = new List<Unicorn.UnicornCharacterMotor>();


    }
}