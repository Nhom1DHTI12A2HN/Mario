using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlEnd : MonoBehaviour
{
    public Text txtYourPoint;
    int yourPoint = GameController.gamePoint;

    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        txtYourPoint.text = "Your Point: " + yourPoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hienDiem()
    {
        
    }




}
