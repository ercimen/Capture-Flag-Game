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
       // Vector2 MousePos = Input.mousePosition;
       // MousePos = Camera.main.ScreenToViewportPoint(MousePos);
          transform.Translate(Direction * Time.deltaTime * 5, Space.World);
       // transform.position = new Vector3 (MousePos.x*3,transform.position.y,transform.position.z);
          if (Input.GetKeyDown(KeyCode.Space)) Ragdoll_Manager.Instance.CheckPassiveObjects(1, this.transform);
      //  if (Input.GetMouseButtonDown(0)) Ragdoll_Manager.Instance.CheckPassiveObjects(1, this.transform);


    }

}
