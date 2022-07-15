using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediation : StateMachineBehaviour
{           public GameObject Boss;
           
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Boss=GameObject.Find("Boss");
       Boss.layer=8;
        Boss.GetComponent<CircleCollider2D>().enabled=true;
        Boss.GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Static;
    
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         Boss=GameObject.Find("Boss");
         Boss.layer=3;
        Boss.GetComponent<CircleCollider2D>().enabled=false;
        Boss.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
