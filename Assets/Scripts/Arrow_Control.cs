using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Control : MonoBehaviour
{
   
    public float inputX;
    [SerializeField] float ArrowSpeed;
    Ragdoll_Manager ragdolls; // Ragdoll manager araciligi ile oyuncu aktif edecegiz

    private void Awake()
    {
        ragdolls = FindObjectOfType<Ragdoll_Manager>();
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

        if (Input.GetKeyDown(KeyCode.Space)) ragdolls.CheckPassiveObjects(1, this.transform);


    }

}
