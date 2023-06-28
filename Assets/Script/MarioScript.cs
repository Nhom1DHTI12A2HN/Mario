using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioScript : MonoBehaviour
{
    private float NhayThap = 4;
    private float RoiXuong = 2;
    private float NhayCao = 440;
    private float VanToc = 5;
    private float TocDo = 0;
    private bool DuoiDat = true;
    private bool ChuyenHuong = false;
    private bool QuayPhai = true;
    private float KiemTraGiuPhim = 0.2f;
    private float ThoiGianGiuPhim = 0;

    private Vector2 ViTriChet;

    public int CapDo = 0;
    public bool BienHinh = false;
    public bool chamCo = true;
    private int t = 0;
    GameObject laco;

    private AudioSource AmThanh;

    private Rigidbody2D r2d;

    private Animator HoatHoa;

    // Start is called before the first frame update
    void Start()
    {
        HoatHoa = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        AmThanh = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HoatHoa.SetFloat("TocDo", TocDo);
        HoatHoa.SetBool("DuoiDat", DuoiDat);
        HoatHoa.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
        BanDanVaTangToc();

        if(BienHinh == true)
        {
            switch(CapDo)
            {
                case 0:
                    {
                        StartCoroutine(MarioThuNho());
                        BienHinh = false;
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(MarioAnNam());
                        BienHinh = false;
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(MarioAnHoa());
                        BienHinh = false;
                        break;
                    }
                default:BienHinh = false; break;
            }
        }

        if (gameObject.transform.position.y < -7f && t == 0 )
        {
            StartCoroutine(marioDie());
        }
        
        
    }

    private void FixedUpdate()
    {
        DiChuyen();
    }

    void DiChuyen()
    {
        float PhimNhanTraiPhai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc * PhimNhanTraiPhai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc * PhimNhanTraiPhai);

        if (PhimNhanTraiPhai > 0 && !QuayPhai) HuongMatMario();
        if (PhimNhanTraiPhai < 0 && QuayPhai) HuongMatMario();
    }

    void HuongMatMario()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;

        if (TocDo > 0.17) StartCoroutine(MarioChuyenHuong());

    }

    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DuoiDat == true)
        {
            r2d.AddForce((Vector2.up) * NhayCao);
            DuoiDat = false;
            TaoAmThanh("nhay");
        }

        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NenDat")
        {
            DuoiDat = true;
        }

        if (collision.tag == "QuaMan" && chamCo == true)
        {
            TaoAmThanh("stageclear");
            
            chamCo = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "End")
        {
            
            SceneManager.LoadScene("Win");
            
        }
    }

    IEnumerator marioDie()
    {
        t = 1;
        TaoAmThanh("die");
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
        //yield return new WaitForSeconds(1f);
        print("xuong ho");
        SceneManager.LoadScene("ManHinhKetThuc");
    }
    IEnumerator MarioChuyenHuong()
    {
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.2f);
        ChuyenHuong = false;
    }

    void BanDanVaTangToc()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            ThoiGianGiuPhim += Time.deltaTime;
            if (ThoiGianGiuPhim < KiemTraGiuPhim)
            {
                print("Dang ban dan");
            }
            else 
            {
                VanToc = VanToc * 1.01f;
                if (VanToc > 9f) VanToc = 9f;
            };
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            VanToc = 9f;
            ThoiGianGiuPhim = 0;
        }
    }

    //Thay doi do lon cua Mario
    IEnumerator MarioAnNam()
    {
        float DoTre = 0.1f;
        TaoAmThanh("powerup");
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
    }

    IEnumerator MarioAnHoa()
    {
        float DoTre = 0.1f;
        TaoAmThanh("powerup");
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
    }

    IEnumerator MarioThuNho()
    {
        float DoTre = 0.1f;
        TaoAmThanh("thunho");
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
    }

    public void TaoAmThanh(string FileAmThanh)
    {
        AmThanh.PlayOneShot(Resources.Load<AudioClip>("Audio/" + FileAmThanh));
    }

    public void MarioChet()
    {
        ViTriChet = transform.localPosition;
        GameObject MarioChet = (GameObject)Instantiate(Resources.Load("Prefabs/MarioChet"));
        MarioChet.transform.localPosition = ViTriChet;
        
        Destroy(gameObject);

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
