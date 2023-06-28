using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioChet : MonoBehaviour
{
    Vector2 ViTriChet;
    public float TocDoNay = 0.1f;
    public float DoNayCao = -2f;
    private bool check = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        StartCoroutine(HMarioChet());
    }

    IEnumerator HMarioChet()
    {
        
        while (check == false)
        {
            print("cao");
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay * Time.deltaTime);
            if (transform.localPosition.y >= ViTriChet.y + DoNayCao)
            {
                check = true;
                break;
            }
                
            yield return null;
        }
        while (true)
        {
            print("thap");
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay * Time.deltaTime);
            if (transform.localPosition.y <= -50f)
            {
                //yield return new WaitForSeconds(1.0f);
                Destroy(gameObject);
                SceneManager.LoadScene("ManHinhKetThuc");
                             
                break;
            }
            yield return null;
        }
    }



}
