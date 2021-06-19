using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_Control : MonoBehaviour
{
    private Check_Collider CheckCol;
    private Player_Control PlayerCont;
    [HideInInspector]
    public byte PlayerAction; // Player Hareket değerleri 0,1
    [SerializeField] float PlayerSpeed;
    [SerializeField] Transform Player;
    private float RandomPos;
    Rigidbody Hips;
    private Animator Animator;
    void Awake()
    {
        CheckCol = FindObjectOfType<Check_Collider>();
        PlayerCont = FindObjectOfType<Player_Control>();
        Animator = GetComponent<Animator>();
        Hips = transform.GetChild(0).GetComponent<Rigidbody>();
    }
    private void Start()
    {
        InvokeRepeating("RandomLook", 2f, 2f);
    }

    private void Update()
    {
        ActionNumber();
    }
    void FixedUpdate()
    {
        PlayerMove();
    }
    void PlayerMove()
    { 
        
        if (PlayerAction==1) Hips.transform.rotation = Quaternion.Euler(0,RandomPos*0.02f,RandomPos); // rasgele hareket 
        switch (PlayerAction)
        {
            case 0: // Just Stop
                Animator.enabled = false;
                break;
            case 1:
                Animator.enabled = true;
                Hips.velocity = transform.forward * (PlayerSpeed * Time.deltaTime);
               // RbHips.transform.position += transform.forward * PlayerSpeed * Time.deltaTime; // Moving
                break;
        }    

    }
    void ActionNumber()
    {
        PlayerAction = 1; // Moving
        if (Input.GetMouseButton(0)) PlayerAction = 0; // Stop
        if (CheckCol.Velocity == true) PlayerAction =0; // SpeedBoost
    }
    void RandomLook()
    {
        if (PlayerAction == 1)
        {
            RandomPos = Random.Range(-5f,5f );
        }
    }
}
