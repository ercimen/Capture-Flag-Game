using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [SerializeField] public Transform[] CapturePoints;
    public Transform EndGame,EndGame2;
    bool endgame1;
    public float offset;
    Vector3 firstpos;

    int CurrentPos;
    bool PosChangedPlus;
    bool PosChangedBack;

    #region Singleton

    private static Camera_Control _instance;

    public static Camera_Control Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Camera_Control>();
            return _instance;
        }
    }
    #endregion
    void Start()
    {
        CurrentPos = 0;
        firstpos = CapturePoints[CurrentPos].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPos = GameManager.Instance.CaptureCount();
        if (CurrentPos < 1) CurrentPos = 1;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, CapturePoints[CurrentPos - 1].transform.position.z+offset), 0.02f);
        
    }

    public void EndGameFc()
    {

            transform.position = Vector3.Lerp(transform.position, EndGame.transform.position, 0.01f);
            transform.rotation = Quaternion.Lerp(transform.rotation, EndGame.rotation, Time.time*0.2f );
 
       
    }

    public void LoseGame()
    {
        transform.LookAt(new Vector3(0, 0, 0));
        transform.position = Vector3.Lerp(transform.position, EndGame2.transform.position, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, EndGame2.rotation, Time.time * 0.1f);

    }
    public void ChangeCamPos(int count)
    {
        if (count == 1)
        {
            PosChangedPlus = true;
            PosChangedBack = false;
        }
        if (count == -1)
        {
            PosChangedBack = true;
            PosChangedPlus = false;
        }
    }
}
