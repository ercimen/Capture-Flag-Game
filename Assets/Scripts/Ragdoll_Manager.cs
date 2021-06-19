using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] Ragdoll;
    [SerializeField] private List<int> RagdollActive;
    [SerializeField] private GameObject PlayerMidPos;
    [SerializeField] private float MidposSpeed; // == PlayerSpeed;
    public byte Ragdoll_StartCount;
    private byte createcount;
    private byte mod;
    Transform oldpos;
    private void Awake()
    {
        RagdollActive = new List<int>();
        GameManager.Instance.ChangeCountText(Ragdoll_StartCount);
    }

    private void Start()
    {
        // InvokeRepeating("CheckActive", 1f, 1f);
        CheckActive();
    }
    private void FixedUpdate()
    {
        //   CheckActive();

        
       
    }
   
   
    public void CheckPassiveObjects(byte count, Transform position)
    {
        createcount = 0;
        GameManager.Instance.ChangeCountText(count);
        for (int i = 0; i < Ragdoll.Length; i++)
        {
         
            if (!Ragdoll[i].activeInHierarchy)
            {
                createcount++;
                Ragdoll[i].SetActive(true);
                Ragdoll[i].GetComponent<Player_Control>().fight = false;
                Ragdoll[i].transform.position = position.position;
                Ragdoll[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 1f, 0f) * 2, ForceMode.Impulse);
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
