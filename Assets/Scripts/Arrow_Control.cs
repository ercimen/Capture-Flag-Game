using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Control : MonoBehaviour
{
   
    public float inputX;
    [SerializeField] float ArrowSpeed;
    

    private void Awake()
    {
        
    }
    void Update()
    {
        ArrowMove();
    }
    void ArrowMove()
    {


        inputX = Input.GetAxis("Horizontal");
        Vector3 Direction = new Vector3(inputX, 0, 0);
        transform.Translate(Direction * Time.deltaTime * 5, Space.World);

        if (Input.GetKeyDown(KeyCode.Space)) Ragdoll_Manager.Instance.CheckPassiveObjects(1, this.transform);


    }

}
