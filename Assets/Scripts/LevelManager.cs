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


        if (GameManager.Instance.EnemyCaptureCount==0)
        {
            Camera_Control.Instance.EndGameFc();
            GameManager.Instance.NextLevel();
        }
        
    }

}
