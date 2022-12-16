using UnityEngine;

namespace SmoothiesFarm.Farmer
{
    public class BobbingController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody m_rigidbody = null;
        [SerializeField]
        private Cinemachine.CinemachineVirtualCamera m_virtualCamera = null;
        private Cinemachine.CinemachineBasicMultiChannelPerlin m_multiChannelPerlin = null;

        private Coroutine m_activationRoutine = null;

        [SerializeField]
        private bool m_enableBobbing = true;

        private void Awake()
        {
            m_multiChannelPerlin = m_virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            if (!m_enableBobbing)
                m_multiChannelPerlin.m_FrequencyGain = 0f;
        }

        private void FixedUpdate()
        {
            if(m_enableBobbing)
                m_multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(0f, 0.03f, Mathf.Clamp01(m_rigidbody.velocity.magnitude / 5f));
        }
    }
}