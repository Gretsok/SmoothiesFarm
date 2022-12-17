using UnityEngine;

namespace SmoothiesFarm.Farm.Unicorn
{
    public class UnicornCollisionHandler : MonoBehaviour
    {
        [SerializeField]
        private UnicornCharacterMotor m_motor = null;
        public UnicornCharacterMotor CharacterMotor => m_motor;
    }
}