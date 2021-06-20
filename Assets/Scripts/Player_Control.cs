using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    [SerializeField] float PlayerSpeed;
    private Animator Animator;
    public bool fight;
    public bool guard;
    bool BossTime;
    bool isPlayerBossCreated;
    GameObject target;

    Renderer Renderer, Renderer2;


    void Awake()
    {

        Animator = GetComponent<Animator>();
        Renderer = transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>();
        Renderer2 = transform.GetChild(1).transform.GetChild(1).GetComponent<Renderer>();
    }

    private void Start()
    {


    }

    void Update()
    {
        CheckBoss();

    }

    void isGuard()
    {
        if (guard)
        {
          //  Renderer.material.SetColor("_Color", Color.yellow);
          //  Renderer2.material.SetColor("_Color", Color.yellow);
          //  transform.localScale = new Vector3(3f, 3f, 3f);
        }
        if (!guard)
        {
           // Renderer.material.SetColor("_Color", Color.blue);
           // Renderer2.material.SetColor("_Color", Color.blue);
            transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }
 

    void CheckBoss()
    {
        if (GameManager.Instance.EnemyCaptureCount == 1) BossTime = true;

        if (!BossTime)
        {
            isGuard();
            PlayerMove();
        }
        if (BossTime && !isPlayerBossCreated)
        {
           
            CreatePlayerBoss();
        }

    }

    void CreatePlayerBoss()
    {
        float zpos = transform.position.z;
        float targetzpos = Camera_Control.Instance.CapturePoints[3].position.z;
        if (!guard)
        {
 if (zpos <= targetzpos)
        {
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
           // transform.LookAt(Camera_Control.Instance.CapturePoints[3]);
        }

        if (zpos >= targetzpos)
        {
            Animator.SetFloat("Speed", 0);
        }
        }
       
    }
    void PlayerMove()
    {



        if (!fight && !guard)
        {
            Animator.SetFloat("Speed", 1);
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(transform.position + Vector3.forward);
        }
        if (!fight && guard)
        {
            Animator.SetFloat("Speed", 0);
        }

        if (fight)
        {
            Animator.SetFloat("Speed", 1);
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
            guard = false;
            this.gameObject.SetActive(false);

            GameManager.Instance.ChangeCountText(-1);
        }
    }

}


