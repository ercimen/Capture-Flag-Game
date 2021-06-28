using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Archer : MonoBehaviour
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
    [SerializeField] GameObject EndLevelTarget;


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
      //  CheckBoss();
        if (HP > 1)
        {
            transform.localScale = new Vector3(HP, HP, HP);
           
        }
 PlayerMove();
    }




    void CheckBoss()
    {
        if (GameManager.Instance.EnemyCaptureCount == 0) BossTime = true;

        if (!BossTime)
        {
            PlayerMove();
        }
        if (BossTime)
        {
            guard = false;
            Capturetower = false;
            Animator.SetFloat("Speed", 1);
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(EndLevelTarget.transform.position);
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
            Debug.Log(target.gameObject.name);
          //  Animator.SetFloat("Speed", 0);
          
           
            if (target.activeInHierarchy)
            {
                transform.LookAt(target.gameObject.transform.position);
                 Animator.SetTrigger("Shoot");
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
        if (collision.collider.tag == "Jump")
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 4f, 2f) * 2, ForceMode.Impulse); // Jump  
        }

        if (collision.collider.tag == "KillPlayer")
        {

            guard = false;
            Capturetower = false;
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Death(0.3f));

            }

            HP = 1;

            GameManager.Instance.ChangeCountText(-1);
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            HP--;
            if (HP <= 0)
            {
                guard = false;
                Capturetower = false;
                if (gameObject.activeInHierarchy)
                {
                    StartCoroutine(Death(0.3f));

                }

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
    IEnumerator Death(float waitTime)
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(waitTime);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    IEnumerator PassiveDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);

    }
}


