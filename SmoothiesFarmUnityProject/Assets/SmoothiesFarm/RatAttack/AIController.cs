using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System;
using SmoothiesFarm.Farm.Unicorn;

namespace SmoothiesFarm.RatAttack
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] 
        float chaseDistance = 5f;
        [SerializeField]
        private Mover mover;
        [SerializeField]
        private List<GameObject> targets;
        private GameObject target = null;
        
        private bool beginning;
        

        
        private void Start(){
            targets = new List<GameObject>();
            mover.SetMoveForward(true);
        }

        private void Update()
        {
            if(!beginning){
                if(target!=null){
                    if(Vector3.Distance(target.transform.position, transform.position) > chaseDistance){
                        mover.MoveTo(target.transform.position, 1f);
                    }
                    else{
                        mover.Cancel();
                        AttackBehaviour();
                    }
                }
            }
        }

        private void AttackBehaviour()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            UnicornCharacterMotor unicorn = other.gameObject.GetComponentInParent<UnicornCharacterMotor>();
            if(unicorn !=null && !targets.Contains(unicorn.gameObject)){
                targets.Add(unicorn.gameObject);
                if(target == null){
                    target = unicorn.gameObject;
                }
            }
        }

    }
}
