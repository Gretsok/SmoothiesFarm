using UnityEngine;
using UnityEngine.AI;


namespace SmoothiesFarm.RatAttack
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 6f;
        Animator characterAnimator;
        NavMeshAgent nmAgent;
        Vector3 moveForward;
        bool isMoveForward;


        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            nmAgent = GetComponent<NavMeshAgent>();
        }

        private void Update(){
            if(isMoveForward){
                nmAgent.destination += new Vector3(0,0,-1);
            }

        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            nmAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            nmAgent.destination = destination;
            nmAgent.isStopped = false;
        }

        public void SetMoveForward(bool isMoveForward){
            this.isMoveForward = isMoveForward;
            nmAgent.speed = isMoveForward? maxSpeed: 0;
        }

        public void Cancel()
        {
            nmAgent.isStopped = true;
        }
    }

}
