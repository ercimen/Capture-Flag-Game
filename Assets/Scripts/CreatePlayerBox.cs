using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatePlayerBox : MonoBehaviour
{
    [SerializeField] byte PlayerCount;
    [SerializeField] Text txt;
   
    void Start()
    {
      
      //  txt.GetComponent<UnityEngine.UI.Text>().text = PlayerCount.ToString();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") Enemy_Manager.Instance.CreateBoX(PlayerCount, this.transform.position);
        if (other.tag == "Player") Ragdoll_Manager.Instance.CreateBoX(PlayerCount, this.transform.position);

    }
}
