using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Control : MonoBehaviour
{

    [SerializeField] float PlayerSpeed;
    [SerializeField] GameObject EndLevelTarget;
    public float HP;
    private Animator Animator;
    public bool fight;
    GameObject target;
    public bool Capturetower;
    bool BossTime;
    void Awake()
    {

        Animator = GetComponent<Animator>();
        HP = 1;

    }


    void Update()
    {
        if (GameManager.Instance.isGameStarted == true)
        {
            if (GameManager.Instance.PlayerCaptureCount == 0)
            {
                Capturetower = false;
                target = null;
                fight = false;
                transform.LookAt(EndLevelTarget.transform.position);
            }
            EnemyMove();

            if (HP > 1)
            {
                transform.localScale = new Vector3(HP, HP, HP);

            }
        }




    }
    void EnemyMove()
    {
        Animator.SetFloat("Speed", 1);
        if (!fight)
        {
            transform.LookAt(transform.position + Vector3.back);
            transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving

            if (Capturetower)
            {
                fight = false;
                Animator.SetFloat("Speed", 1);
                transform.LookAt(target.transform);
                transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
            }
        }

        if (fight)
        {

            if (target.activeInHierarchy)
            {
                transform.LookAt(target.transform);
                transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving

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

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerTrig"))
        {
            fight = true;
            target = other.gameObject.transform.parent.gameObject;
        }

        if (other.CompareTag("TowerRange"))
        {
            if (other.gameObject.transform.parent.GetComponent<Capture_Point>().FlagOwner == 1)
            {
                Capturetower = true;
                Debug.Log("Tower Range " + Capturetower);
                target = other.gameObject.transform.GetChild(2).gameObject;
            }
            if (other.gameObject.transform.parent.GetComponent<Capture_Point>().FlagOwner != 1)
            {
                Capturetower = false;
            }


        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Jump")
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 4f, -2f) * 2, ForceMode.Impulse); // Jump  
        }

        if (collision.collider.tag == "KillPlayer")
        {

            Capturetower = false;
            fight = false;

            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Death(0.3f));
            }
            HP = 1;
        }


        if (collision.collider.CompareTag("Player"))
        {
            HP--;
            if (HP <= 0)
            {
                Capturetower = false;
                fight = false;
                if (gameObject.activeInHierarchy)
                {
                    StartCoroutine(Death(0.3f));

                }
                HP = 1;
            }
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

}