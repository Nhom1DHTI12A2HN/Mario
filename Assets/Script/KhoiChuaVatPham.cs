using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhoiChuaVatPham : MonoBehaviour
{
    private float DoNayCuaKhoi = 0.4f;
    private float TocDoNay = 4f;
    private bool DuocNay = true;
    private Vector2 ViTriLucDau;

    public bool ChuaNam = false;
    public bool ChuaXu = false;
    public bool ChuaHoa = false;

    public int SoLuongXu = 1;

    GameObject Mario;
    GameObject gameController;

    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }

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
        if(collision.collider.tag == "VaCham" && collision.contacts[0].normal.y > 0)
        {
            ViTriLucDau = transform.position;
            
            KhoiNayLen();
            
        }
        else
        {
            print("Chi va cham");
        }
    }

    void KhoiNayLen()
    {
        if (DuocNay)
        {
            DuocNay = false;
            if (ChuaNam)
            {
                NamVaHoa();
            }
            else if (ChuaXu)
            {
                HienThiXu();
            }
            StartCoroutine(KhoiNay());
          
        }
    }

    IEnumerator KhoiNay()
    {
        while (true)
        {
            print("Da day");
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay * Time.deltaTime);
            if (transform.localPosition.y >= ViTriLucDau.y + DoNayCuaKhoi) break;
            yield return null;     
        }
        while (true)
        {
            print("ha xuong");
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay * Time.deltaTime);
            if (transform.localPosition.y <= ViTriLucDau.y) break;
            Destroy(gameObject);
            GameObject KhoiRong = (GameObject)Instantiate(Resources.Load("Prefabs/KhoiTrong"));
            KhoiRong.transform.position = ViTriLucDau;
            yield return null;
        }
    }

    void NamVaHoa()
    {
        int CapDoHienTai = Mario.GetComponent<MarioScript>().CapDo;
        GameObject Nam = null;
        if (CapDoHienTai == 0) Nam = (GameObject)Instantiate(Resources.Load("Prefabs/Nam"));
        else Nam = (GameObject)Instantiate(Resources.Load("Prefabs/Hoa"));
        Mario.GetComponent<MarioScript>().TaoAmThanh("thunho");
        Nam.transform.SetParent(this.transform.parent);
        Nam.transform.localPosition = new Vector2(ViTriLucDau.x, ViTriLucDau.y + 0.7f);
    }

    void HienThiXu()
    {
        
        GameObject DongXu = (GameObject)Instantiate(Resources.Load("Prefabs/Xu"));
        Mario.GetComponent<MarioScript>().TaoAmThanh("coin");
        DongXu.transform.SetParent(this.transform.parent);
        DongXu.transform.localPosition = new Vector2(ViTriLucDau.x, ViTriLucDau.y + 1f);
        StartCoroutine(XuNay(DongXu));
        
    }
    IEnumerator XuNay(GameObject dongXu)
    {
        while (true)
        {
            print("xu");
            gameController.GetComponent<GameController>().getPoint();
            dongXu.transform.localPosition = new Vector2(dongXu.transform.localPosition.x, dongXu.transform.localPosition.y + 2f * Time.deltaTime);
            if (dongXu.transform.localPosition.y >= ViTriLucDau.y + 1f) break;
            yield return null ;

        }

        while (true)
        {
            print("ha xuong");
            dongXu.transform.localPosition = new Vector2(dongXu.transform.localPosition.x, dongXu.transform.localPosition.y - 6 * Time.deltaTime);
            if (dongXu.transform.localPosition.y <= ViTriLucDau.y + 1) break;

            yield return null;
        }
    }
}
