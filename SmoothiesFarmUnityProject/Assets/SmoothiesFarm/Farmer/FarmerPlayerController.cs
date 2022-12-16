using UnityEngine;

namespace SmoothiesFarm.Farmer
{
    public class FarmerPlayerController : MonoBehaviour
    {
        private FarmerControls m_controls = null;

        [SerializeField]
        private FarmerCharacterMotor m_characterMotor = null;

        private void Awake()
        {
            m_controls = new FarmerControls();
        }

        private void OnEnable()
        {
            m_controls.Enable();
            m_controls.Gameplay.Jump.started += Jump_started;
            m_controls.Gameplay.Jump.canceled += Jump_canceled;
            m_controls.Gameplay.Attack.started += Attack_started;
            m_controls.Gameplay.Attack.canceled += Attack_canceled;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Attack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (m_characterMotor)
                m_characterMotor.IsAttacking = false;
        }

        private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (m_characterMotor)
                m_characterMotor.IsAttacking = true;
        }

        private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (m_characterMotor)
                m_characterMotor.IsJumping = false;
        }

        private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (m_characterMotor)
                m_characterMotor.IsJumping = true;
        }

        private void Update()
        {
            if(m_characterMotor)
            {
                m_characterMotor.SetMovementInputs(m_controls.Gameplay.Movement.ReadValue<Vector2>());
                m_characterMotor.SetLookAroundInputs(m_controls.Gameplay.LookAround.ReadValue<Vector2>());
            }
        }

        private void OnDisable()
        {
            m_controls.Gameplay.Jump.started -= Jump_started;
            m_controls.Gameplay.Jump.canceled -= Jump_canceled;
            m_controls.Gameplay.Attack.started -= Attack_started;
            m_controls.Gameplay.Attack.canceled -= Attack_canceled;
            m_controls.Disable();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}