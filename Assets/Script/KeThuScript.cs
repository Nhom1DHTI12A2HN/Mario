using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeThuScript : MonoBehaviour
{
    GameObject Mario;
    Vector2 ViTriChet;

    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        ViTriChet = transform.localPosition;
        if (gameObject.transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.contacts[0].normal.x < 0 || collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.y > 0) && collision.collider.tag == "Player")
        {
            if (Mario.GetComponent<MarioScript>().CapDo >= 1)
            {
                Mario.GetComponent<MarioScript>().CapDo = 0;
  
                Mario.GetComponent<MarioScript>().BienHinh = true;
            }
            else Mario.GetComponent<MarioScript>().MarioChet();
        }

        
    }

}
