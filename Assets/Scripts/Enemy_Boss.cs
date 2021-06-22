using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : MonoBehaviour
{

    [SerializeField] float PlayerSpeed;
    [SerializeField] int HP;
    private Animator Animator;
    public bool fight;
    GameObject target;
    void Awake()
    {

        Animator = GetComponent<Animator>();

    }


    void Update()
    {

        EnemyMove();


    }
    void EnemyMove()
    {
        Animator.SetFloat("Speed", 0);
        if (!fight)
        {
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(transform.position + Vector3.back);
        }

        if (fight)
        {

            if (target.activeInHierarchy)
            {
                Animator.SetFloat("Speed", 0);
                transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
                transform.LookAt(target.transform);
            }

            if (!target.activeInHierarchy)
            {
                fight = false;
                return;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;
            Debug.Log("triglendim");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;
            Debug.Log("triglendim durdum");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }

}