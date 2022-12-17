using UnityEngine;

namespace SmoothiesFarm.Utils
{
    public class RepositionWhenGoingUnderMap : MonoBehaviour
    {
        private Vector3 m_startPosition = default;
        private void Start()
        {
            m_startPosition = transform.position;
        }

        private void LateUpdate()
        {
            if(transform.position.y < -25f)
            {
                transform.position = m_startPosition;
                transform.rotation = Quaternion.identity;
            }
        }
    }
}