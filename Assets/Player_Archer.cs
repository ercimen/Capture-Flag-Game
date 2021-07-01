using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Archer : MonoBehaviour
{
    [SerializeField] GameObject[] Arrows;

    int ActiveArrow;
    public bool fight;
    public bool arrowshoot;
    Animator anim;

    [SerializeField] GameObject FireEffect;


    private void Awake()
    {
        ActiveArrow = 0;
        anim = GetComponent<Animator>();

    }
 



    private void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {

            Fire();

            if (ActiveArrow == 0)  Arrows[0].transform.position += transform.forward * Time.deltaTime * 20;
            if (ActiveArrow == 1)  Arrows[1].transform.position += transform.forward * Time.deltaTime * 20;


        }


    }

    void Fire()
    {
        anim.SetTrigger("Shoot");
    }

    void FireArrow() // Trigger - Archer Animasyon Event
    {
        arrowshoot = true;
        ActiveArrow++;
        Debug.Log("Animasyondan geldim " + ActiveArrow);
        if (ActiveArrow == 2)
        {
            ActiveArrow = 0;
            Arrows[0].gameObject.SetActive(true);
            Arrows[1].gameObject.SetActive(false);
            Arrows[0].transform.LookAt(transform.forward);
            Arrows[0].transform.Rotate(new Vector3(90, 0, 0));
            Arrows[0].transform.position = transform.position + Vector3.up * 2f;
        }
        if (ActiveArrow == 1)
        {
            Arrows[1].gameObject.SetActive(true);
            Arrows[0].gameObject.SetActive(false);
            Arrows[1].transform.LookAt(transform.forward);
            Arrows[1].transform.Rotate(new Vector3(90, 0, 0));
            Arrows[1].transform.position = transform.position + Vector3.up * 2.2f;

        }

    }
  

}
