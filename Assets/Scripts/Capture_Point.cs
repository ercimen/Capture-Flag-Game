using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture_Point : MonoBehaviour
{
    [SerializeField] float HP;
    [SerializeField] int TowerNumber;
    float FirstHP;
    [SerializeField] Text txt;
    [SerializeField] GameObject Flag;
    float FlagChangePosAmount;
    Renderer Renderer;
    [SerializeField] Renderer Renderer2, Renderer3;

    [SerializeField] public byte FlagOwner; // 1 Player 2 Enemy
    bool FlagOwnerChanged;
    bool BossTime;
    bool HPCoverForBug;
    Vector3 oldpos;
    void Start()
    {
        oldpos = transform.position;
        FirstHP = HP;
        HPCoverForBug = false;
        Renderer = transform.GetChild(0).transform.GetChild(2).GetComponent<Renderer>();

        if (FlagOwner == 1) Renderer.material.SetColor("_Color", Color.blue);
        if (FlagOwner == 2) Renderer.material.SetColor("_Color", Color.red);

        InvokeRepeating(nameof(RandomCreate), 3f, 3f);

    }

    void RandomCreate()
    {
        if (GameManager.Instance.isGameStarted == true)
        {
            StartCoroutine(RandomCreateIE());
        }

    }


    IEnumerator RandomCreateIE()
    {
        if (!BossTime)
        {
            float randomTime = Random.Range(3f, 5f);

            yield return new WaitForSeconds(randomTime);
            if (FlagOwner == 1) Ragdoll_Manager.Instance.CreateTowerWarrior(1, transform.position);
            if (FlagOwner == 2) Enemy_Manager.Instance.CreateTowerWarrior(1, transform.position);

        }

    }

    private void Update()
    {
        if (GameManager.Instance.isGameStarted == true)
        {
          /*
            if (GameManager.Instance.PlayerCaptureCount > TowerNumber)
            {
                HP = 11111;
                HPCoverForBug = true;
            }
            if (GameManager.Instance.PlayerCaptureCount == TowerNumber && HPCoverForBug)
            {
                HP = FirstHP;
                HPCoverForBug = false;
            }
           */


            if (GameManager.Instance.EnemyCaptureCount == 0) BossTime = true;
            if (GameManager.Instance.PlayerCaptureCount == 0) BossTime = true;

            if (BossTime)
            {
                HP = 1000;
            }

            if (FlagOwnerChanged)
            {
                if (FlagOwner == 1)
                {
                    StartCoroutine(Confeti(1f));
                    //    Ragdoll_Manager.Instance.CreateGuard(10, transform);
                    // Ragdoll_Manager.Instance.CheckPassiveObjects(10, this.transform);

                    Renderer.material.SetColor("_Color", Color.blue);

                    HP = FirstHP;

                    GameManager.Instance.ChangeCountCapture(1, "Player");


                }
                if (FlagOwner == 2)
                {
                    Renderer.material.SetColor("_Color", Color.red);

                    HP = FirstHP;


                    GameManager.Instance.ChangeCountCapture(1, "Enemy");

                }
                FlagOwnerChanged = false;
            }


        }





    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (FlagOwner != 1)
            {
                HP--;
                StartCoroutine(Dust(1f));
                if (HP <= 0)
                {
                    FlagOwner = 1;
                    FlagOwnerChanged = true;
                }

            }
            if (FlagOwner == 1)
            {
                //   HP++;
                if (HP >= FirstHP)
                {
                    HP = FirstHP;
                }
            }

            // txt.GetComponent<UnityEngine.UI.Text>().text = HP.ToString();

        }

        if (other.CompareTag("Enemy"))
        {
            if (FlagOwner != 2)
            {
                HP--;
                StartCoroutine(Dust(1f));
                if (HP <= 0)
                {
                    FlagOwner = 2;
                    FlagOwnerChanged = true;
                }
            }
            if (FlagOwner == 2)
            {
                //  HP++;
                if (HP >= FirstHP)
                {
                    HP = FirstHP;
                }
            }

            // txt.GetComponent<UnityEngine.UI.Text>().text = HP.ToString();

        }

    }

    IEnumerator Confeti(float waitTime)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(waitTime);
        transform.GetChild(1).gameObject.SetActive(false);

    }
    IEnumerator Dust(float waitTime)
    {
        transform.position -= new Vector3(0, 0.1f, 0);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(waitTime);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.position += new Vector3(0, 0.1f, 0);

    }
}