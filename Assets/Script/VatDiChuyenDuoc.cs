using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatDiChuyenDuoc : MonoBehaviour
{
    public float VanToc = 2;
    public bool DiChuyenTrai = true;

    private void FixedUpdate()
    {
        Vector2 DiChuyen = transform.localPosition;
        if (DiChuyenTrai)
        {
            DiChuyen.x -= VanToc * Time.deltaTime;
        }
        else
        {
            DiChuyen.x += VanToc * Time.deltaTime;
        }
        transform.localPosition = DiChuyen;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.x < 0 && collision.collider.tag != "Player")
        {
            
            DiChuyenTrai = false;
            QuayMat();
        }
        else if (collision.contacts[0].normal.x > 0 && collision.collider.tag != "Player")
        {
            
            DiChuyenTrai = true;
            QuayMat();
        }
    }
    void QuayMat()
    {
        DiChuyenTrai = !DiChuyenTrai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
    }

}
