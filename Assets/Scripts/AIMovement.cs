using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    //Variables 
    public float speed = 1.5f;
    public float minGoalDistance = 0.05f;
    public Transform player;
    public float chaseDistance = 3f;

    //an array of GameObjects
    public Transform[] waypoints;
    public int waypointIndex = 0;
    //public GameObject position0;
    //public GameObject position1;


    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //checking distance to player, if closer to player AI runs to player - are within the player chase distance
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            //Move towards the player
            AIMoveTowards(player);
        }

        else
        {
            //Move towards our waypoints
            WaypointUpdate();
            AIMoveTowards(waypoints[waypointIndex]); //the number is called the index
        }
    }

    private void WaypointUpdate()
    {
        Vector2 AIPosition = transform.position;

        //If we are near the goal
        if (Vector2.Distance(AIPosition, waypoints[waypointIndex].position) < minGoalDistance)
        {
            // ++ increment by 1
            //increase the value of waypointIndex up by 1
            waypointIndex++;

            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }

    private void AIMoveTowards(Transform goal)
    {
        Vector2 AIPosition = transform.position;

        //if we are not near the goal
        if (Vector2.Distance(AIPosition, goal.position) > minGoalDistance)
        {

            //method 3
            //direction from A to B
            // is B - A
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }

    }

}
