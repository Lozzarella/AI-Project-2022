using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State //comma seperated list of identifiers
    {
        Attack,
        Defence,
        RunAway,
        BerryPicking,
    }

    public State currentState;
    public AIMovement aiMovement;

    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        NextState();
    }

    private void NextState()
    {
        //runs one of the cases that matches the value (in this example the valye is currenState)
        switch(currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
        }
    }

    //Corutine is a special method that can be paused and returned to later
    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        while (currentState == State.Attack)
        {
            aiMovement.AIMoveTowards(aiMovement.player);//Move towards the player
            Debug.Log("Currently Attacking");

            if (Vector2.Distance(transform.position, aiMovement.player.position) >= aiMovement.chaseDistance) //if we are within the player chase distance
            {
                currentState = State.BerryPicking;
                
            }
                        
            yield return null;
        }

        Debug.Log("Attack: Exit");
        NextState();
    }

    private IEnumerator DefenceState()
    {
        Debug.Log("Defence: Enter");
        while (currentState == State.Defence)
        {
            Debug.Log("Currently Defending");
            yield return null;
        }

        Debug.Log("Defence: Exit");
        NextState();
    }

    private IEnumerator RunAwayState()
    {
        Debug.Log("RunAway: Enter");
        while (currentState == State.RunAway)
        {
            Debug.Log("Currently Running Away");
            yield return null;
        }

        Debug.Log("RunAway: Exit");
        NextState();
    }

    private IEnumerator BerryPickingState()
    {
        Debug.Log("Berry Picking: Enter");//logs when we enter berry picking

        aiMovement.LowestDistance(); //check what the lowest distance is

        while (currentState == State.BerryPicking)//run once every frame, sounds like update function/method
        {
            aiMovement.WaypointUpdate();//correct way point we are going to and increases the index
            aiMovement.AIMoveTowards(aiMovement.waypoints[aiMovement.waypointIndex]);//move the AI towards the waypoint we are up to
            Debug.Log("Currently Berry Picking");//logs when we are currently berry picking
            
            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance) //if we are within the player chase distance
            {
                currentState = State.Attack;
                
            }

            yield return null;//returns to method on the very next frame                         
        }

        Debug.Log("Berry Picking: Exit");//logs when we exit berry picking
        NextState();//exit the berry picking state and move on to the next state

    }
}