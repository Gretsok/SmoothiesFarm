using UnityEngine;

namespace SmoothiesFarm.Utils
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField]
        private float m_rotationSpeed = 90f;
        [SerializeField]
        private Vector3 m_axis = Vector3.up;

        private void FixedUpdate()
        {
            transform.Rotate(m_axis, m_rotationSpeed * Time.fixedDeltaTime);
        }
    }
}