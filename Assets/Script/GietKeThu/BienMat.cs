using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BienMat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BienMat1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BienMat1()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
