using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text txtPoint;
    public static int gamePoint = 0;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getPoint()
    {
        gamePoint++;
        txtPoint.text = "Point: " + gamePoint.ToString();
        GiuDiem.diem = gamePoint;
    }

    private void OnDisable()
    {
        //PlayerPrefs.SetInt("score", gamePoint);
        
    }

}
