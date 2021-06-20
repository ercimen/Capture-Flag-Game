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
   
   
}
