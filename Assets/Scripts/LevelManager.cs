using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int Number;
    public float Bossspeed, CamSpeed;
    public GameObject Camera;
    public GameObject Boss;
    public GameObject Confeti;
    // Start is called before the first frame update
    float zpos;
    float ypos;
    bool isMovingCam = true;
    void Start()
    {
        Number = 0;
    }
    private void Update()
    {
        BossMove();
        CameraMove();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckCollider")
        {
            Number += 1;
            if (GameManager.Instance.liveCount == Number)
            {
                CamSpeed = 0;
                Confeti.SetActive(true);
                Bossspeed = 0;
                GameManager.Instance.NextLevel();
            }
        }
    }
    void BossMove()
    {
        Vector3 forwardMove = Boss.transform.forward * (Bossspeed * Time.deltaTime);
        Boss.transform.position += forwardMove;
    }


    void CameraMove()
    {

        if (!Input.GetMouseButton(0))
        {
            Camera.transform.position += Vector3.forward * (CamSpeed * Time.deltaTime);
        }

    }
}
