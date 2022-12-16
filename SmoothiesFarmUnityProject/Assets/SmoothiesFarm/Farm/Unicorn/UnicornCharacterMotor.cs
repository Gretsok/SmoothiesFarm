using UnityEngine;

namespace SmoothiesFarm.Farm.Unicorn
{
    [RequireComponent(typeof(Rigidbody))]
    public class UnicornCharacterMotor : MonoBehaviour
    {
        private Rigidbody m_rigidbody = null;

        [Header("Params")]
        [SerializeField]
        private float m_movementSpeed = 3f;

        private Vector2 m_movementsInputs = default;
        public void SetMovementInputs(Vector2 a_inputs)
        {
            if (a_inputs.sqrMagnitude > 1f)
            {
                a_inputs.Normalize();
            }

            m_movementsInputs = a_inputs;
        }

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            float magnitude = m_movementsInputs.magnitude;
            var movementToApply = transform.TransformDirection(new Vector3(m_movementsInputs.x, 0f, m_movementsInputs.y));
            movementToApply.y = 0f;
            movementToApply = movementToApply.normalized * magnitude * m_movementSpeed;
            movementToApply.y = m_rigidbody.velocity.y;


            m_rigidbody.velocity = movementToApply;
        }
    }
}