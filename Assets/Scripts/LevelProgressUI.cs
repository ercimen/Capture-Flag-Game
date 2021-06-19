using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelProgressUI : MonoBehaviour
{
    [Header("UI references :")]
   
   
    [SerializeField] private Text Leveltext;

   
    private float playerpos,enemypos;
    private void Start()
    {
        Leveltext.text = "level " + (SceneManager.GetActiveScene().buildIndex + 1);

    }
    private void Update()
    {
 
    }
   
}

