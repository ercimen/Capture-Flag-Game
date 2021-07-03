using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Control : MonoBehaviour
{

    public float inputX;
    [SerializeField] float ArrowSpeed;
    float timer,firstTimer;
    [SerializeField] GameObject Slider;
    float SliderSize;
    bool isSliderPressed;


    private void Awake()
    {
        isSliderPressed = false;
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
        if (!Input.GetKey(KeyCode.Space))
        {
           SliderSize = 0;
        }
        

        inputX = Input.GetAxis("Horizontal");
        Vector3 Direction = new Vector3(inputX, 0, 0);
        transform.Translate(Direction * Time.deltaTime * 5, Space.World);

        timer -= Time.deltaTime;
       
        if (Input.GetKeyDown(KeyCode.Space) && timer<=0)
        {
            timer = firstTimer;

            Ragdoll_Manager.Instance.CheckPassiveObjects(1,1, this.transform);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SliderSize += 0.002f;
            Slider.transform.localScale= new Vector3(SliderSize, 1, 1); 
                if (SliderSize>=1)
            {
                Ragdoll_Manager.Instance.CheckPassiveObjects(4,1, this.transform);
                SliderSize = 0f;
            }
        }
    }

}
