using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public GameObject position0; 
    public GameObject position1;


    // Start is called before the first frame update
    private void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        //method 1
        Vector2 AIPosition = transform.position;
        if (transform.position.x > position0.transform.position.x)
        {
            //move right
            AIPosition.x += (1 * Time.deltaTime);
            transform.position = AIPosition;
        }

        else
        {
            //move left
            AIPosition.x -= (1 * Time.deltaTime);
            transform.position = AIPosition;
        }

        //method 2
        if (transform.position.y > position0.transform.position.x)
        {
            transform.position += (Vector3) Vector2.up * 1 * Time.deltaTime;
        }

        else
        {
            transform.position -= (Vector3) Vector2.up * 1 * Time.deltaTime;
        }
        */

        //direction from A to B
        // is B - A
        //method 3

        Vector2 directionToPos0 = (Vector2) (position0.transform.position - transform.position);
        directionToPos0.Normalize();
        transform.position += (Vector3) directionToPos0 * 1 * Time.deltaTime;


    }
}
