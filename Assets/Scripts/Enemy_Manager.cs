using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] Ragdoll;
    [SerializeField] private List<int> RagdollActive;
    [SerializeField] private GameObject PlayerMidPos;
    [SerializeField] private float MidposSpeed; // == PlayerSpeed;
    public byte Ragdoll_StartCount;
    private byte createcount;
    Vector3 EnemyPos;
    private void Awake()
    {
        RagdollActive = new List<int>();
     //   GameManager.Instance.ChangeCountText(Ragdoll_StartCount);
    }

    private void Start()
    {
        InvokeRepeating(nameof(RandomCreate), 1f, 1f);
        CheckActive();
    }
    private void FixedUpdate()
    {
        //   CheckActive();
       

    }

    void RandomCreate() => StartCoroutine(RandomCreateIE());
    IEnumerator RandomCreateIE()
    {
        float maxX, minX;
        
        maxX = 5;
        minX = -4;
        float randomPos = Random.Range(minX, maxX);
        float randomTime = Random.Range(0.5f, 1.5f);
        EnemyPos = new Vector3(randomPos, 1.6f, 28.5f);
        yield return new WaitForSeconds(randomTime);
        CheckPassiveObjects(1, EnemyPos);
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
                Ragdoll[i].transform.position = position+new Vector3(0,0,createcount*1.3f);
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
