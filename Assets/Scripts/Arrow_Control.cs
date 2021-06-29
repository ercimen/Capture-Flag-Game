using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Control : MonoBehaviour
{

    public float inputX;
    [SerializeField] float ArrowSpeed;
    float timer,firstTimer;


    private void Awake()
    {
      
    }

    private void Start()
    {
        timer = Ragdoll_Manager.Instance.Player_SpawnTime;
        firstTimer = timer;
    }
    void Update()
    {
        if (GameManager.Instance.isGameStarted == true)
        {
            ArrowMove();
        }

    }
    void ArrowMove()
    {


        inputX = Input.GetAxis("Horizontal");
        Vector3 Direction = new Vector3(inputX, 0, 0);
        transform.Translate(Direction * Time.deltaTime * 5, Space.World);

        timer -= Time.deltaTime;
       
        if (Input.GetKeyDown(KeyCode.Space) && timer<=0)
        {
            timer = firstTimer;

            Ragdoll_Manager.Instance.CheckPassiveObjects(1, this.transform);
        }

      


    }

}
