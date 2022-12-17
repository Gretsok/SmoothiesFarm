using UnityEngine;

namespace SmoothiesFarm.Utils.Tween
{
    public class ScaleTween : MonoBehaviour
    {
        [SerializeField]
        private Vector2 m_scaleRange = new Vector2(0.9f, 1.1f);
        [SerializeField]
        private float m_periodDuration = 1f;

        private float value = 0f;


        // Update is called once per frame
        void FixedUpdate()
        {
            transform.localScale = Vector3.one * Mathf.Lerp(m_scaleRange.x, m_scaleRange.y, Mathf.Abs(Mathf.Cos(value)));
            value += Time.fixedDeltaTime * Mathf.PI * (m_periodDuration / 2f);
        }
    }
}