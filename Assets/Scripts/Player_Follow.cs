using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Follow : MonoBehaviour
{
    Quaternion toRotation;
    Vector3 Direction;
    float waittime = 0.2f;

    private void Start()
    {
       // PlayerMove();
    }
    void FixedUpdate()
    {
        PlayerMove();
    }
    void PlayerMove()
    {

        float inputX = Input.GetAxis("Horizontal");
       

        Direction = new Vector3(inputX, 0, 0);
        Direction.Normalize();
        toRotation = Quaternion.LookRotation(Direction, Vector3.up);

        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
 
                transform.GetChild(i).GetChild(0).GetComponent<Rigidbody>().transform.Translate(Direction * Time.deltaTime * 3);
                transform.GetChild(i).GetChild(0).GetComponent<Rigidbody>().transform.rotation = Quaternion.RotateTowards(transform.GetChild(i).GetChild(0).GetComponent<Rigidbody>().transform.rotation, toRotation, 10);
              
         }
          

       }
     }

   





   

