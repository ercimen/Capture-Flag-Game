using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] Ragdoll;
    [SerializeField] private List<int> RagdollActive;
    [SerializeField] private GameObject PlayerMidPos;
    [SerializeField] public float Player_Speed,Player_SpawnTime; 

    private byte createcount;
    private byte mod;
    Transform oldpos;

    #region Singleton

    private static Ragdoll_Manager _ưnstance;

    public static Ragdoll_Manager Instance
    {
        get
        {
            if (_ưnstance == null)
                _ưnstance = FindObjectOfType<Ragdoll_Manager>();
            return _ưnstance;
        }
    }
    #endregion

    private void Awake()
    {
        RagdollActive = new List<int>();
       

        if (Player_Speed==0)  Player_Speed = 5;
        if (Player_SpawnTime == 0) Player_SpawnTime = 0.3f;

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
   
   
    public void CheckPassiveObjects(byte Hp, byte count, Transform position)
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
                Ragdoll[i].gameObject.GetComponent<Player_Control>().HP = Hp;
                if (createcount == count)
                { 
                     CheckActive(); 
                    return;  
                }
                       
            }
        }

      
    }

    public void CreateGuard(byte count, Transform position)
    {
        createcount = 0;
        int raw = 1;
        int column = 0;
        GameManager.Instance.ChangeCountText(count);
        for (int i = 0; i < Ragdoll.Length; i++)
        {

            if (!Ragdoll[i].activeInHierarchy)
            {
                createcount++;
                column++;
                if (createcount == 5)
                { 
                    raw = 2;
                    column = 0;
                    
                }
               // Ragdoll[i].GetComponent<Player_Control>().fight = false;
                Ragdoll[i].transform.position = position.position;
                Ragdoll[i].transform.position+= new Vector3(column-3, 1, 2*raw);
               // Ragdoll[i].GetComponent<Player_Control>().guard = true; 
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
                Ragdoll[i].gameObject.GetComponent<Player_Control>().HP = 3;
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
                Ragdoll[i].transform.position += new Vector3(createcount - 3, -1, 1);

                Ragdoll[i].SetActive(true);
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
