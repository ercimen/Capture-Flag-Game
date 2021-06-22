using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] Balls;
    GameObject target;
    Vector3 targetPos;
    int ActiveBall;
    bool fight;
    public bool fired;
    float timer;
    float attackDelay;
    public int CannonOwner;

    [SerializeField] GameObject FireEffect;


    private void Awake()
    {
        ActiveBall = 0;
        attackDelay = 1.5f;
        CannonOwner = transform.parent.gameObject.GetComponent<Capture_Point>().FlagOwner;

    }
    void Start()
    {
        fired = false;
        timer = attackDelay;

    }



    private void Update()
    {

        Attack();
        AttackDelay();


    }


    void AttackDelay()
    {
        if (fired == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                ActiveBall++;
                Debug.Log("ActiveBall" + ActiveBall);
                if (ActiveBall == 2)
                {
                    ActiveBall = 0;
                    Balls[0].gameObject.SetActive(true);
                    Balls[1].gameObject.SetActive(false);
                    Balls[0].transform.position = transform.position;
                    Balls[0].transform.position += new Vector3(0, 2.2f, 0);
                    fired = false;

                }
                if (ActiveBall == 1)
                {
                    Balls[1].gameObject.SetActive(true);
                    Balls[0].gameObject.SetActive(false);
                    Balls[1].transform.position = transform.position;
                    Balls[1].transform.position += new Vector3(0, 2.2f, 0);
                    fired = false;
                }


                timer = attackDelay;
            }
        }
    }

    void Attack()
    {
       
        if (fight)
        {
            targetPos = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
            transform.LookAt(targetPos);

            if (target.activeInHierarchy)
            {
                Fire();
                fired = true;
               
            }

        }

        if (!fight)
        {
            target = null;
          //  if (CannonOwner==1) transform.LookAt(new Vector3(0, transform.position.y, 0));
          //  if (CannonOwner == 2) transform.LookAt(new Vector3(0, transform.position.y, -200));
        }



    }

    void Fire()
    {
       
        Balls[ActiveBall].transform.position = Vector3.Lerp(Balls[ActiveBall].transform.position, targetPos, 20f * Time.deltaTime);
        // Balls[ActiveBall].GetComponent<Rigidbody>().velocity = targetPos*20;
        
    }


    private void OnTriggerStay(Collider other)
    {
        /*
        if (other.CompareTag("Player") && CannonOwner == 1)
        {
            fight = false;
            target = null;
            Debug.Log("Dostum geldi");
        }
        if (other.CompareTag("Enemy") && CannonOwner == 2)
        {

            fight = false;
            target = null;
            Debug.Log("Dostum geldi");
        }
        */

        if (other.CompareTag("Player") && CannonOwner !=1)
        {

                fight = true;
                Debug.Log("Tower Range ");
          
            target = other.gameObject;
            if (!target)
                {
                    target = other.gameObject;
                }
        }
       
        if (other.CompareTag("Enemy") && CannonOwner != 2)
        {

                fight = true;
                Debug.Log("Tower Range ");
            target = other.gameObject;
            if (!target)
                {
                    target = other.gameObject;
                }

        }
        
       
    }

}
