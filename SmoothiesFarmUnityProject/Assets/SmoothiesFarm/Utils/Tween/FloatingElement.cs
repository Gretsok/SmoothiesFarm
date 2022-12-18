using UnityEngine;

namespace SmoothiesFarm.Utils
{
    public class FloatingElement : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_firstPosition = default;
        [SerializeField]
        private Vector3 m_secondPosition = default;
        [SerializeField]
        private float m_periodDuration = 1f;

        private float value = 0f;


        // Update is called once per frame
        void FixedUpdate()
        {
            transform.localPosition = Vector3.Lerp(m_firstPosition, m_secondPosition, Mathf.Abs(Mathf.Cos(value)));
            value += Time.fixedDeltaTime * Mathf.PI * (m_periodDuration / 2f);
        }
    }
}