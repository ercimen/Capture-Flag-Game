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

    private void Awake()
    {
        ActiveBall = 0;
    }
    void Start()
    {
      fired = false;
      timer = 1f;
     
    }

  

    private void Update()
    {
        Attack();

        if (fired==true)
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


                timer = 1f;
            }
        }
       
    }


    void Attack()
    {
       
        if (fight)
        {
            targetPos = new Vector3(target.transform.position.x, target.transform.position.y + 3, target.transform.position.z);
            transform.LookAt(targetPos);

            if (target.activeInHierarchy)
            {
              Fire();
              fired = true;
            }

            
        } 
      
      
     
    }

    void Fire()
    {
            Balls[ActiveBall].transform.position = Vector3.Lerp(Balls[ActiveBall].transform.position, targetPos, 20f * Time.deltaTime);

    }
   
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            fight = true;
            Debug.Log("Tower Range ");
            if (!target)
            {
                  target = other.gameObject;
            }

            // StartCoroutine(AttackDelay(0.5f));
        }
    }

    public void FireBall(Vector3 position)
    {

        for (int i = 0; i < Balls.Length; i++)
        {

            if (!Balls[i].activeInHierarchy)
            {
                Balls[i].transform.position = position;
                Balls[i].transform.position += new Vector3(0, 2.2f, 0);
                Balls[i].SetActive(true);
                ActiveBall = i;
                return;
            }
        }
    }

   
}
