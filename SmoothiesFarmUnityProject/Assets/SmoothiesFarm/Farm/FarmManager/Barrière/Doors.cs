using System.Collections;
using UnityEngine;

namespace SmoothiesFarm.Farm.FarmManager
{
    public class Doors : MonoBehaviour
    {
        [SerializeField]
        private Transform m_pivot1 = null;
        [SerializeField]
        private Transform m_pivot2 = null;

        [SerializeField]
        private float m_pivot1OpenAngle = 45f;
        [SerializeField]
        private float m_pivot2OpenAngle = 45f;

        [SerializeField]
        private float m_openingAndClosingDuration = 0.5f;


        public void Activate()
        {
            StartCoroutine(MovingDoorsRoutine(m_pivot1OpenAngle, m_pivot2OpenAngle));
        }

        public void Deactivate()
        {
            StartCoroutine(MovingDoorsRoutine(0f, 0f));
        }

        private Coroutine m_movingCoroutine = null;
        private IEnumerator MovingDoorsRoutine(float a_targetPivot1Angle, float a_targetPivot2Angle)
        {

            if (m_movingCoroutine != null)
            {
                StopCoroutine(m_movingCoroutine);
            }

            float timeOfStart = Time.time;
            Vector3 eulerAngle;
            while (Time.time - timeOfStart < m_openingAndClosingDuration)
            {
                yield return null;
                eulerAngle = m_pivot1.transform.localEulerAngles;
                eulerAngle.y = Mathf.Lerp(eulerAngle.y, a_targetPivot1Angle, (Time.time - timeOfStart) / m_openingAndClosingDuration);
                m_pivot1.transform.localEulerAngles = eulerAngle;

                eulerAngle = m_pivot2.transform.localEulerAngles;
                eulerAngle.y = Mathf.Lerp(eulerAngle.y, a_targetPivot2Angle, (Time.time - timeOfStart) / m_openingAndClosingDuration);
                m_pivot2.transform.localEulerAngles = eulerAngle;
            }

            eulerAngle = m_pivot1.transform.localEulerAngles;
            eulerAngle.y = a_targetPivot1Angle;
            m_pivot1.transform.localEulerAngles = eulerAngle;

            eulerAngle = m_pivot2.transform.localEulerAngles;
            eulerAngle.y = a_targetPivot2Angle;
            m_pivot2.transform.localEulerAngles = eulerAngle;
        }



        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Farmer.FarmerCharacterMotor farmer))
            {
                Activate();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Farmer.FarmerCharacterMotor farmer))
            {
                Deactivate();
            }
        }
    }
}