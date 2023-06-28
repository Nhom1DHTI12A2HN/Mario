using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuaBep : MonoBehaviour
{
    Vector2 ViTriChet;
    GameObject gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ViTriChet = transform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            
            gameController.GetComponent<GameController>().getPoint();
            Destroy(gameObject);
            GameObject HinhBep = (GameObject)Instantiate(Resources.Load("Prefabs/RuaBep"));
            HinhBep.transform.localPosition = ViTriChet;
        }
    }
}
