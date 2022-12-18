using SmoothiesFarm.Farm.Unicorn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.RatAttack
{
    [RequireComponent(typeof(Rigidbody))]
    public class RatCharacterMotor : MonoBehaviour
    {
        private Rigidbody m_rigidbody = null;

        [SerializeField]
        private float m_maxSpeed = 5f;
        private float m_currentSpeed = 0f;

        [SerializeField]
        private int m_attackDamage = 2;
        [SerializeField]
        private float m_attackCooldown = 0.5f;
        private float m_lastTimeOfAttack = 0f;

        [SerializeField]
        private RatAnimationController m_animationController = null;

        public void ChangeDirection(Vector3 a_newDirection)
        {
            transform.LookAt(transform.position + a_newDirection);
        }

        public void SetSpeed(float a_normalizedSpeed)
        {
            m_currentSpeed = m_maxSpeed * Mathf.Clamp01(a_normalizedSpeed);
            m_animationController.SetSpeed(a_normalizedSpeed);
        }

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var movementToApply = transform.forward * m_currentSpeed;
            float magnitude = movementToApply.magnitude;
            movementToApply.y = 0f;
            movementToApply = movementToApply.normalized * magnitude;
            movementToApply.y = m_rigidbody.velocity.y;

            m_rigidbody.velocity = movementToApply;
        }

        public void Attack(Vector3 a_attackDirection)
        {
            if (Time.time - m_lastTimeOfAttack < m_attackCooldown) return;

            m_animationController.Hit();
            if(Physics.Raycast(transform.position, a_attackDirection, out RaycastHit outInfo))
            {
                if(outInfo.collider.TryGetComponent(out UnicornCollisionHandler unicornHandler))
                {
                    unicornHandler.CharacterMotor.GetComponent<HealthHandlingController>().TakeDamage(m_attackDamage);
                    m_lastTimeOfAttack = Time.time;
                }
            }
        }
    }
}