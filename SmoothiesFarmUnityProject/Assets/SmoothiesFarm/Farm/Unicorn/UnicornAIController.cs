using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.Farm.Unicorn
{
    public class UnicornAIController : MonoBehaviour
    {
        [SerializeField]
        private UnicornCharacterMotor m_motor = null;
        [SerializeField]
        private Vector2 m_walkDuration = new Vector2(2f, 6f);
        [SerializeField]
        private Vector2 m_stayStillDuration = new Vector2(6f, 20f);
        private float m_timeToUse = 0f;
        private float m_lastStepTime = 0f;
        private bool m_isWalking = false;


        #region Walking Params
        [SerializeField]
        private List<Transform> m_sightOrigins = null;
        #endregion

        private void Awake()
        {
            m_timeToUse = Random.Range(m_stayStillDuration.x, m_stayStillDuration.y);
            m_lastStepTime = Time.time;
        }

        private void FixedUpdate()
        {
            CheckBehaviourToHave();
            HandleCurrentBehaviour();
        }

        private void CheckBehaviourToHave()
        {
            if(Time.time - m_lastStepTime > m_timeToUse)
            {
                m_isWalking = !m_isWalking;
                m_lastStepTime = Time.time;
                if (m_isWalking)
                {
                    m_timeToUse = Random.Range(m_walkDuration.x, m_walkDuration.y);
                    ChangeToRandomDirection();
                }
                else
                {
                    m_timeToUse = Random.Range(m_stayStillDuration.x, m_stayStillDuration.y);
                }
                
            }
        }

        private void HandleCurrentBehaviour()
        {
            if (m_isWalking)
            {
                bool canGoForward = true;
                for(int i = 0; i < m_sightOrigins.Count; ++i)
                {
                    if (Physics.Raycast(m_sightOrigins[i].position, transform.forward, 0.5f))
                    {
                        canGoForward = false;
                        Debug.DrawLine(m_sightOrigins[i].position, m_sightOrigins[i].position + transform.forward * 0.5f, Color.red);
                        break;
                    }
                    else
                    {
                        Debug.DrawLine(m_sightOrigins[i].position, m_sightOrigins[i].position + transform.forward * 0.5f, Color.green);
                    }
                }
                if (!canGoForward)
                {
                    ChangeToRandomDirection();
                }
                m_motor.SetMovementInputs(new Vector2(0f, 1f));
            }
            else
            {
                m_motor.SetMovementInputs(default);
            }
        }

        private void ChangeToRandomDirection()
        {
            float deg = Random.Range(0f, 360f);
            var direction = new Vector3(Mathf.Cos(deg), 0f, Mathf.Sin(deg));
            m_motor.SetDirection(transform.TransformDirection(direction));
        }

    }
}