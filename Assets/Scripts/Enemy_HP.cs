using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HP : MonoBehaviour
{
    [SerializeField] float HP;
    [SerializeField] Text txt;
    [SerializeField] GameObject Flag;
    float FlagChangePosAmount;
    [SerializeField] GameObject Confeti;
    void Start()
    {
     
        txt.GetComponent<UnityEngine.UI.Text>().text = HP.ToString();
        
        // Flag.gameObject.transform.position -= new Vector3(0, -10, 0);
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HP--;
            txt.GetComponent<UnityEngine.UI.Text>().text = HP.ToString();
             FlagChangePosAmount = 5 * (HP / 100);
         Flag.transform.localPosition = new Vector3(0,FlagChangePosAmount , 0);
        }
        if (HP <= 0)
        {
           
            Confeti.SetActive(true);

            GameManager.Instance.NextLevel();
        }

    }
}
