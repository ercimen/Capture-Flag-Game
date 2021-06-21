using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatePlayerBox : MonoBehaviour
{
    [SerializeField] byte PlayerCount;
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") Enemy_Manager.Instance.CreateBoX(PlayerCount, this.transform.position+new Vector3(0,0,-1f));
        if (other.tag == "Player") Ragdoll_Manager.Instance.CreateBoX(PlayerCount, this.transform.position+new Vector3(0,0,2f));
    }
}
