using System;
using UnityEngine;

namespace SmoothiesFarm.Utils
{
    public class CollisionsRelay : MonoBehaviour
    {
        public Action<Collider> onTriggerEntered = null;
        public Action<Collider> onTriggerExited = null;
        public Action<Collision> onCollisionEntered = null;
        public Action<Collision> onCollisionExited = null;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExited?.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            onCollisionEntered?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            onCollisionExited?.Invoke(collision);
        }
    }
}