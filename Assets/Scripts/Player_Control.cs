using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    [SerializeField] float PlayerSpeed;
    public float HP;
    private Animator Animator;
    public bool fight;
    public bool guard;
    public bool Capturetower;
    bool BossTime;
    bool isPlayerBossCreated;
    GameObject target;

    Renderer Renderer, Renderer2;


    void Awake()
    {

        Animator = GetComponent<Animator>();
        Renderer = transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>();
        Renderer2 = transform.GetChild(1).transform.GetChild(1).GetComponent<Renderer>();
        HP = 1;
    }

    private void Start()
    {


    }

    void Update()
    {
        CheckBoss();
        if (HP>1)
        {
               transform.localScale = new Vector3(transform.localScale.x+(HP/3), transform.localScale.y + (HP / 3), transform.localScale.z+ (HP / 3));

        }
   
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
        if (GameManager.Instance.EnemyCaptureCount == 0) BossTime = true;

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

        if (Capturetower)
        {
            Animator.SetFloat("Speed", 1);
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(target.transform);
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyTrig"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;

        }

        if (other.CompareTag("Tower") && Capturetower)
        {
            guard = false;
            Capturetower = false;
            this.gameObject.SetActive(false);
            Debug.Log("Towere girdim");

        }

        if (other.CompareTag("TowerRange"))
        {
          

        }

        if (other.CompareTag("Ball") )
        {
            guard = false;
            Capturetower = false;
          //  this.gameObject.GetComponent<Rigidbody>().AddExplosionForce(10f, transform.position, 5f, 3.0F);
            this.gameObject.SetActive(false);
            Debug.Log("�ld�m ya La");
            other.gameObject.SetActive(false);
            

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyTrig"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;

        }

        if (other.CompareTag("TowerRange"))
        {
            if (other.gameObject.transform.parent.GetComponent<Capture_Point>().FlagOwner == 1)
            {
                Capturetower = false;
                Debug.Log("Tower Range ");
               // target = null;
               // target = other.gameObject.transform.GetChild(2).gameObject;
            }

            if (other.gameObject.transform.parent.GetComponent<Capture_Point>().FlagOwner == 2)
            {
                Capturetower = true;
                Debug.Log("Tower Range ");
                target = other.gameObject.transform.GetChild(2).gameObject;
            }


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            HP--;
            if (HP<=0)
            {
                 guard = false;
                 Capturetower = false;
                 this.gameObject.SetActive(false);
                 HP = 1;
            }


            GameManager.Instance.ChangeCountText(-1);
        }

        if (collision.collider.CompareTag("Ball2"))
        {
             this.gameObject.GetComponent<Rigidbody>().AddExplosionForce(300f, transform.position, 5f, 3.0F);
           // StartCoroutine(PassiveDelay(1f));
        }
    }

    IEnumerator PassiveDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);

    }
}


