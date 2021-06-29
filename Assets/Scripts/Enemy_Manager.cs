using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] Ragdoll;
    [SerializeField] private List<int> RagdollActive;
    [SerializeField] private GameObject PlayerMidPos;
    [Header("EnemySpawn")]
    [SerializeField] public float SpawnTime,EnemySpeed;

    
    private byte createcount;
    Vector3 EnemyPos;
    public bool BossTime;
    #region Singleton

    private static Enemy_Manager _Instance;

    public static Enemy_Manager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<Enemy_Manager>();
            return _Instance;
        }
    }
    #endregion
    private void Awake()
    {
        RagdollActive = new List<int>();
        if (SpawnTime==0) SpawnTime = 0.5f;
        if (EnemySpeed == 0) EnemySpeed = 3f;


        //   GameManager.Instance.ChangeCountText(Ragdoll_StartCount);
    }

    private void Start()
    {
        InvokeRepeating(nameof(RandomCreate), SpawnTime, SpawnTime);
        CheckActive();
    }
    private void Update()
    {
        if (GameManager.Instance.isGameStarted == true)
        {
            //   CheckActive();
            if (GameManager.Instance.EnemyCaptureCount == 0) BossTime = true;
        }





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
        if (GameManager.Instance.isGameStarted == true)
        {
            if (!BossTime)
            {
                float maxX, minX;

                maxX = 13;
                minX = -12;
                float randomPos = Random.Range(minX, maxX);
                float randomTime = Random.Range(SpawnTime,SpawnTime+1.5f);
                EnemyPos = new Vector3(randomPos, 1.6f, 80f);
                yield return new WaitForSeconds(randomTime);
                CheckPassiveObjects(1, EnemyPos);
            }
        }
    }
    public void CheckPassiveObjects(byte count, Vector3 position)
    {
        createcount = 0;
        int row = 1;
        // GameManager.Instance.ChangeCountText(count);
        for (int i = 0; i < Ragdoll.Length; i++)
        {

            if (!Ragdoll[i].activeInHierarchy)
            {
                createcount++;
                Ragdoll[i].SetActive(true);
                Ragdoll[i].GetComponent<Enemy_Control>().fight = false;
                Ragdoll[i].transform.position = position + new Vector3(0, 0, createcount * 1.3f);
                if (createcount == count)
                {
                    CheckActive();
                    return;
                }

            }
        }


    }

    public void CreateBoX(byte count, Vector3 position)
    {
        createcount = 0;
        GameManager.Instance.ChangeCountText(count);
        for (int i = 0; i < Ragdoll.Length; i++)
        {

            if (!Ragdoll[i].activeInHierarchy)
            {
                createcount++;


                Ragdoll[i].transform.position = position;
                Ragdoll[i].transform.position += new Vector3(createcount - 3, 0, -5);

                Ragdoll[i].SetActive(true);
                if (createcount == count)
                {
                    CheckActive();
                    return;
                }

            }
        }
    }

    public void CreateTowerWarrior(byte count, Vector3 position)
    {
        createcount = 0;
        GameManager.Instance.ChangeCountText(count);
        for (int i = 0; i < Ragdoll.Length; i++)
        {
            if (!Ragdoll[i].activeInHierarchy)
            {
                createcount++;
                // Ragdoll[i].GetComponent<Player_Control>().fight = false;
                Ragdoll[i].transform.position = position;
                // Ragdoll[i].transform.position += new Vector3(column - 3, 1, 2 * raw);
                // Ragdoll[i].GetComponent<Player_Control>().guard = true; 
                Ragdoll[i].SetActive(true);
                Ragdoll[i].gameObject.GetComponent<Enemy_Control>().HP = 3;
                if (createcount == count)
                {
                    CheckActive();
                    return;
                }

            }
        }
    }
    public void CheckActive()
    {
        RagdollActive.Clear();

        for (int i = 0; i < Ragdoll.Length; i++)
        {
            if (Ragdoll[i].activeInHierarchy)
            {
                RagdollActive.Add(i);
            }
        }

    }






}
