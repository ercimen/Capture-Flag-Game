using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Check_Collider : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Death;
    public bool Velocity;
    void Start()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jump")
        {
          Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,4f,2f) * 2, ForceMode.Impulse); // Jump  
        }

        if (other.tag=="KillPlayer")
        {
           
            Player.SetActive(false);
           
            GameManager.Instance.ChangeCountText(-1);
        }

        if (other.tag == "Velocity")
        {
          //  AddForce(new Vector3(0, 0.5f, 0.2f) * 150, ForceMode.Impulse); // Jump 
          //  Velocity = true;
        }

        if (other.tag == "Ground")
        {
            Velocity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish")
        {
          Player.GetComponent<Animator>().enabled = false;
          Player.GetComponent<Player_Control>().enabled=false;
        }
    }

}
