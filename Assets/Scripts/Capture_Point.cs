using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture_Point : MonoBehaviour
{
    [SerializeField] float HP;
    float FirstHP;
    [SerializeField] Text txt;
    [SerializeField] GameObject Flag;
    float FlagChangePosAmount;
    Renderer Renderer;

    [SerializeField] byte FlagOwner; // 1 Player 2 Enemy
    bool FlagOwnerChanged;
    void Start()
    {
      
        FirstHP = HP;
        txt.GetComponent<UnityEngine.UI.Text>().text = HP.ToString();

        Renderer =transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        
        if (FlagOwner==1) Renderer.material.SetColor("_Color", Color.blue*0.7f);
        if (FlagOwner == 2) Renderer.material.SetColor("_Color", Color.red*0.7f);
    }

    private void Update()
    {
        if (FlagOwnerChanged)
        {
            if (FlagOwner==1)
            {
                Renderer.material.SetColor("_Color", Color.blue*0.7f);
                HP = FirstHP;
                FlagChangePosAmount = 5 * (HP / FirstHP);
                Flag.transform.localPosition = new Vector3(0, FlagChangePosAmount, 0);
                GameManager.Instance.ChangeCountCapture(1,"Player");

            }
            if (FlagOwner == 2)
            {
                Renderer.material.SetColor("_Color", Color.red*0.7f);
                HP = FirstHP;
                FlagChangePosAmount = 5 * (HP / FirstHP);
                Flag.transform.localPosition = new Vector3(0, FlagChangePosAmount, 0);
                GameManager.Instance.ChangeCountCapture(1, "Enemy");
            }
            FlagOwnerChanged = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            if (FlagOwner!=1)
            {
             HP--;
                if (HP <= 0)
                 {
                FlagOwner = 1;
                FlagOwnerChanged = true;
                }
            }
            if (FlagOwner == 1)
            {
                HP++;
                if (HP >= FirstHP)
                {
                    HP = FirstHP;
                }
            }

            // txt.GetComponent<UnityEngine.UI.Text>().text = HP.ToString();
            FlagChangePosAmount = 5 * (HP / FirstHP );
            Flag.transform.localPosition = new Vector3(0, FlagChangePosAmount, 0); 
        }

        if (other.CompareTag("Enemy"))
        {
            if (FlagOwner != 2)
            {
                HP--;
                if (HP <= 0)
                {
                    FlagOwner = 2;
                    FlagOwnerChanged = true;
                }
            }
            if (FlagOwner == 2)
            {
                HP++;
                if (HP >= FirstHP)
                {
                    HP = FirstHP;
                }
            }

            // txt.GetComponent<UnityEngine.UI.Text>().text = HP.ToString();
            FlagChangePosAmount = 5 * (HP / FirstHP);
            Flag.transform.localPosition = new Vector3(0, FlagChangePosAmount, 0);
        }

    }
}