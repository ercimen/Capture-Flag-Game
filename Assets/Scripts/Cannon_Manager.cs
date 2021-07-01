using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] Balls;
    GameObject target;
    Vector3 targetPos,targetArrow;
    int ActiveBall;
    public bool fight;
    public bool fired,arrowshoot;
    float timer;
    float attackDelay;
    public int CannonOwner;
    Animator anim;

    [SerializeField] GameObject FireEffect;


    private void Awake()
    {
        ActiveBall = 0;
        attackDelay = 1.5f;
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        fired = false;
        arrowshoot = false;
        timer = attackDelay;

    }



    private void Update()
    {

        Attack();
        if (arrowshoot)
        {
            if (ActiveBall == 0)
            {
                Balls[0].transform.position += transform.forward*Time.deltaTime*20;
                fired = false;

            }
            if (ActiveBall == 1)
            {
                Balls[1].transform.position += transform.forward*Time.deltaTime*20;
                fired = false;
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
        anim.SetTrigger("Shoot");
    }

    void FireArrow()
    {
        arrowshoot = true; 
        ActiveBall++;
        Debug.Log("Animasyondan geldim "+ActiveBall);
        if (ActiveBall == 2)
        {
            targetArrow = targetPos;
            ActiveBall = 0;
            Balls[0].gameObject.SetActive(true);
            Balls[1].gameObject.SetActive(false);
            Balls[0].transform.LookAt(transform.forward);
            Balls[0].transform.Rotate(new Vector3(90, 0, 0));

            Balls[0].transform.position = transform.position+Vector3.up*2.2f;
         

        }
        if (ActiveBall == 1)
        {
            targetArrow = targetPos;
            Balls[1].gameObject.SetActive(true);
            Balls[0].gameObject.SetActive(false);
            Balls[1].transform.LookAt(transform.forward);
            Balls[1].transform.Rotate(new Vector3(90, 0, 0));
            Balls[1].transform.position = transform.position + Vector3.up * 2.2f;

        }

    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("EnemyArcherTrig") )
        {

                fight = true;
               
          
           
            if (target==null)
                {
                    target = other.gameObject;
                }
        }
 
       
    }

}
