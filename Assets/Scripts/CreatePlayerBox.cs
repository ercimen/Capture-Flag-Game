using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatePlayerBox : MonoBehaviour
{
    [SerializeField] byte PlayerCount;
    [SerializeField] Text txt;
    Enemy_Manager enemies; // Ragdoll manager araciligi ile oyuncu aktif edecegiz
    void Start()
    {
        enemies = FindObjectOfType<Enemy_Manager>();
        txt.GetComponent<UnityEngine.UI.Text>().text = PlayerCount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {  
            enemies.CheckPassiveObjects(PlayerCount, this.transform.position);
          
            Destroy(this.gameObject);
        }
    }
}
