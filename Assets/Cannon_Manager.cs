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
    public bool firetoEnemy;
    bool fired;

    private void Awake()
    {
        ActiveBall = 0;
    }
    void Start()
    {
 firetoEnemy = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
    }


    void Attack()
    {
       
        if (fight)
        {
            targetPos = new Vector3(target.transform.position.x, target.transform.position.y + 3, target.transform.position.z);
            transform.LookAt(targetPos);
        } 
        if (firetoEnemy == true)
            {
            FireBall(transform.position);
            Fire();
            }

    }
    IEnumerator AttackDelay(float waitTime)
    {
        firetoEnemy = false;
        yield return new WaitForSeconds(waitTime);
        firetoEnemy = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fight = true;
            firetoEnemy = true;
            Debug.Log("Tower Range ");
            target = other.gameObject;
            // StartCoroutine(AttackDelay(0.5f));
            Attack();
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

    void Fire()
    {
        if (ActiveBall >= 1)
        {
            ActiveBall = 0;
        }
        Balls[ActiveBall].transform.position = Vector3.Lerp(Balls[ActiveBall].transform.position, targetPos, 0.2f);
        
        Debug.Log("ActiveBall" + ActiveBall);
        if (!Balls[ActiveBall].activeInHierarchy)
        {
            ActiveBall++;
            FireBall(transform.position);
        }
    }
}
