using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    [SerializeField] float PlayerSpeed;
    private Animator Animator;
    public bool fight;
    GameObject target;
    void Awake()
    {

        Animator = GetComponent<Animator>();

    }


    void Update()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        Animator.SetFloat("Speed", 1);

        if (!fight)
        {
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(transform.position + Vector3.forward);
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
        //  transform.position = Vector3.Lerp(transform.position, target.transform.position,0.02f); // Moving

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;
          
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;
          
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.ChangeCountText(-1);
        }
    }

}


