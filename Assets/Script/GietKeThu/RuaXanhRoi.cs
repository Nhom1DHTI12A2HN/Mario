using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuaXanhRoi : MonoBehaviour
{
    GameObject gameController;
    private AudioSource amThanh;
    public AudioClip amthanh;
    
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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y < 0 && collision.collider.tag == "Player")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(amthanh);
            gameController.GetComponent<GameController>().getPoint();
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            
            if (gameObject.transform.position.y < -7) Destroy(gameObject);
        }
    }
    public void taoAmThanh(string tenfile)
    {
        amThanh.PlayOneShot(Resources.Load<AudioClip>("Audio/" + tenfile));
    }
}
