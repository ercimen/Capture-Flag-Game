using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    float PlayerSpeed;
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
        PlayerSpeed = Ragdoll_Manager.Instance.Player_Speed;
        RbforChilds(true);
        ColforChilds(false);
        transform.GetChild(3).gameObject.GetComponent<Collider>().enabled = true;
    }

    void Update()
    {
        CheckBoss();
        if (HP > 1)
        {
            transform.localScale = new Vector3(HP, HP, HP);
        }

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



        if (!fight)
        {
            Animator.SetFloat("Speed", 1);
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            transform.LookAt(transform.position + Vector3.forward);

            if (Capturetower)
            {
                Animator.SetFloat("Speed", 1);
                transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
                transform.LookAt(target.transform);
            }
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
            }

            if (other.gameObject.transform.parent.GetComponent<Capture_Point>().FlagOwner == 2)
            {
                Capturetower = true;

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
        if (collision.collider.CompareTag("Block"))
        {

            RagdollDeath();
        }

        if (collision.collider.tag == "KillPlayer")
        {

            guard = false;
            Capturetower = false;
            if (gameObject.activeInHierarchy)
            {
                RagdollDeath();
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

    }

    void RagdollDeath()
    {
        Animator.enabled = false;
        RbforChilds(false);
        ColforChilds(true);
        StartCoroutine(DeathRagAnim(1f));
    }

    void RbforChilds(bool state)
    {

        Rigidbody[] rbc = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childrb in rbc)
        {
            childrb.isKinematic = state;
        }
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    void ColforChilds(bool state)
    {
        Collider[] Colc = GetComponentsInChildren<Collider>();
        foreach (Collider ChildCol in Colc)
        {
            ChildCol.enabled = state;
        }
        gameObject.GetComponent<Collider>().enabled = true;
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
        RbforChilds(true);
        ColforChilds(false);
        transform.GetChild(3).gameObject.GetComponent<Collider>().enabled = true;
        RefreshStats();
    }

    void RefreshStats()
    {
        Animator.enabled = true;
        guard = false;
        Capturetower = false;
        HP = 1;
        target = null;
        fight = false;
    }
    IEnumerator DeathRagAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(waitTime / 3);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        RbforChilds(true);
        ColforChilds(false);
        transform.GetChild(3).gameObject.GetComponent<Collider>().enabled = true;
        RefreshStats();
    }




    IEnumerator PassiveDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);

    }
}


