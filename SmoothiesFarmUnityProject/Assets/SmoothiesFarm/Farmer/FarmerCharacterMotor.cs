using System;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.Farmer
{
    [RequireComponent(typeof(Rigidbody))]
    public class FarmerCharacterMotor : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField]
        private float m_movementSpeed = 5f;
        [SerializeField]
        private float m_cameraSensitivy = 30f;
        [SerializeField]
        private Vector2 m_cameraVerticalClampValues = new Vector2(-45f, 45f);
        [SerializeField]
        private float m_attackCooldown = 1f;
        private float m_lastTimeOfAttack = 0f;
        private float m_lastTimeOfJump = 0f;
        [SerializeField]
        private float m_jumpVelocity = 7f;
        
        [Header("Refs")]
        [SerializeField]
        private Transform m_cameraTransform = null;
        [SerializeField]
        private Utils.CollisionsRelay m_groundDetection = null;
        private bool m_isGrounded = false;


        public Action OnAttackPerformed = null;
        
        private float m_verticalCameraRotation;


        private Rigidbody m_rigidbody = null;

        public bool IsAttacking { get; set; } = false;
        public bool IsJumping { get; set; } = false;


        private Vector2 m_movementsInputs = default;
        public void SetMovementInputs(Vector2 a_inputs)
        {
            if(a_inputs.sqrMagnitude > 1f)
            {
                a_inputs.Normalize();
            }

            m_movementsInputs = a_inputs;
        }

        private Vector2 m_lookAroundInputs = default;

        public void SetLookAroundInputs(Vector2 a_inputs)
        {
            m_lookAroundInputs = a_inputs;
        }


        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            m_groundDetection.onTriggerEntered += HandleGroundDetectionTriggered;
            m_groundDetection.onTriggerExited += HandleGroundDetectionUntriggered;
        }

        private void OnDisable()
        {
            m_groundDetection.onTriggerEntered -= HandleGroundDetectionTriggered;
            m_groundDetection.onTriggerExited -= HandleGroundDetectionUntriggered;
        }

        void FixedUpdate()
        {
            HandleMovement();
            HandleAttacking();
            HandleJumping();
        }

        private void HandleMovement()
        {
            float magnitude = m_movementsInputs.magnitude;
            var movementToApply = transform.TransformDirection(new Vector3(m_movementsInputs.x, 0f, m_movementsInputs.y));
            movementToApply.y = 0f;
            movementToApply = movementToApply.normalized * magnitude * m_movementSpeed;
            movementToApply.y = m_rigidbody.velocity.y;


            m_rigidbody.velocity = movementToApply;


            m_verticalCameraRotation -= m_lookAroundInputs.y * m_cameraSensitivy * Time.fixedDeltaTime;
            m_verticalCameraRotation = Mathf.Clamp(m_verticalCameraRotation, m_cameraVerticalClampValues.x, m_cameraVerticalClampValues.y);

            m_cameraTransform.localRotation = Quaternion.Euler(m_verticalCameraRotation, 0f, 0f);
            transform.Rotate(transform.up, m_lookAroundInputs.x * m_cameraSensitivy * Time.fixedDeltaTime);
        }

        private void HandleAttacking()
        {
            if (!IsAttacking) return;
            if (Time.time - m_lastTimeOfAttack < m_attackCooldown) return;
            m_lastTimeOfAttack = Time.time;

            OnAttackPerformed?.Invoke();
        }

        private void HandleJumping()
        {
            if (!IsJumping) return;
            if (Time.time - m_lastTimeOfJump < 0.2f) return;
            if (!m_isGrounded) return;
            m_lastTimeOfJump = Time.time;

            m_rigidbody.AddForce(Vector3.up * m_jumpVelocity, ForceMode.VelocityChange);
        }

        private List<Collider> m_groundedDetected = new List<Collider>();
        private void HandleGroundDetectionUntriggered(Collider obj)
        {
            if (m_groundedDetected.Contains(obj))
            {
                m_groundedDetected.RemoveAll(x => x == obj);
            }
            m_isGrounded = m_groundedDetected.Count > 0;
        }

        private void HandleGroundDetectionTriggered(Collider obj)
        {
            if (!m_groundedDetected.Contains(obj))
            {
                m_groundedDetected.Add(obj);
            }
            m_isGrounded = m_groundedDetected.Count > 0;
        }
    }
}
