using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaCoRoi : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (a.GetComponent<GameController>().check == false)
        //{
        //    HaCo();
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(doiti());
            
        }
    }

    IEnumerator doiti()
    {
        HaCo();
        yield return new WaitForSeconds(1.5f);
        print("Doiti");

        //gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void HaCo()
    {
        print("ha co");
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
