using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Control : MonoBehaviour
{

    [SerializeField] float PlayerSpeed;
    public float HP;
    private Animator Animator;
    public bool fight;
    GameObject target;
    public bool Capturetower;
    void Awake()
    {

        Animator = GetComponent<Animator>();
        HP = 1; 

    }


    void Update()
    {
        if (Enemy_Manager.Instance.BossTime)
        {
            this.gameObject.SetActive(false);
        }
        EnemyMove();
        
        if (HP>1)
        {
            // transform.localScale = new Vector3(transform.localScale.x + (HP / 30), transform.localScale.y + (HP / 30), transform.localScale.z + (HP / 30));
            transform.localScale = new Vector3(HP, HP,HP);
            Debug.Log("HP" + HP);
        }
 

    }
    void EnemyMove()
    {
        Animator.SetFloat("Speed", 1);
        if (!fight)
        {
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(transform.position + Vector3.back);
        }

        if (fight)
        {

            if (target.activeInHierarchy)
            {
                transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
                transform.LookAt(target.transform);
            }

            if (!target.activeInHierarchy)
            {
                fight = false;
                return;
            }

        }

        if (Capturetower)
        {
            Animator.SetFloat("Speed", 1);
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(target.transform);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTrig"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;
        }
        if (other.CompareTag("Tower") && Capturetower)
        {
           
            Capturetower = false;
            this.gameObject.SetActive(false);
            Debug.Log("Towere girdim");

        }

        if (other.CompareTag("TowerRange"))
        {
            if (other.gameObject.transform.parent.GetComponent<Capture_Point>().FlagOwner == 1)
            {
                Capturetower = true;
                Debug.Log("Tower Range ");
                target = other.gameObject.transform.GetChild(2).gameObject;
            }

        }

        if (other.CompareTag("Ball"))
        {
           
            Capturetower = false;
            this.gameObject.SetActive(false);
            Debug.Log("Öldüm ya La");
            other.gameObject.SetActive(false);

        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerTrig"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            HP--;
            if (HP <= 0)
            {
                Capturetower = false;
                this.gameObject.SetActive(false);
                HP = 1;
            }
        }
    }

}