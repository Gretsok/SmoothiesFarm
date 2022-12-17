using System.Collections;
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
        [SerializeField]
        private float m_timeToRotate = 1f;
        public float CurrentSpeed 
        { 
            get
            {
                var vel = m_rigidbody.velocity;
                vel.y = 0f;
                return vel.magnitude;
            }
        }

        private Vector2 m_movementsInputs = default;
        public void SetMovementInputs(Vector2 a_inputs)
        {
            if (a_inputs.sqrMagnitude > 1f)
            {
                a_inputs.Normalize();
            }

            m_movementsInputs = a_inputs;
        }

        private bool m_canChangeDirection = true;
        public void SetDirection(Vector3 a_direction)
        {
            if (!m_canChangeDirection) return;
            StartCoroutine(ChangingDirectionRoutine(a_direction));
        }
        private Coroutine m_changingDirectinCoroutine = null;
        private IEnumerator ChangingDirectionRoutine(Vector3 a_direction)
        {
            m_canChangeDirection = false;
            if(m_changingDirectinCoroutine != null)
            {
                StopCoroutine(m_changingDirectinCoroutine);
            }

            float timeOfStart = Time.time;
            while(Time.time - timeOfStart < m_timeToRotate)
            {
                yield return null;
                transform.forward = Vector3.Slerp(transform.forward, a_direction, (Time.time - timeOfStart) / m_timeToRotate);
            }
            transform.forward = a_direction;
            m_canChangeDirection = true;
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