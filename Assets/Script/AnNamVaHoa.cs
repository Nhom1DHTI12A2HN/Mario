using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnNamVaHoa : MonoBehaviour
{
    GameObject Mario;

    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (Mario.GetComponent<MarioScript>().CapDo < 2)
            {
                Mario.GetComponent<MarioScript>().CapDo += 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
            }
            Destroy(gameObject);
        }


    }
}
