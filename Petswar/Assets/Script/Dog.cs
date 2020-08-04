using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    public GameObject hit;
    public GameObject Protectionsphere;
    [Header("集氣速度"), Range(0f, 5f)]
    public float speed = 4f;
    public float hp = 100f;
    [Header("丟擲物品")]
    public GameObject prop;
    [Header("傷害值")]
    public float damage = 10f;
    public Image attCD;

    // 力道範圍
    protected GameObject str_bar;
    protected GameObject hp_bar;
    protected float scripthp;
    protected float str;
    protected float _str;
    protected float timer;
    protected float _timer = 2f;
    protected Animator ani;
    protected bool huge = false, power = false, protection = false;


    private void Awake()
    {

        hp_bar = GameObject.Find("dogHp");
        str_bar = GameObject.Find("dogBar");
    }
    private void Start()
    {
        Physics.IgnoreLayerCollision(8, 9);
        scripthp = hp;
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        attCD.fillAmount = timer / _timer;
        hp_bar.GetComponent<Image>().fillAmount = scripthp / hp;
        str_bar.GetComponent<Image>().fillAmount = _str / 600f;
        str = Mathf.Clamp(_str, 0f, 600f);
        if(protection == true)
        {
            StartCoroutine("Protection");
        }
        Power();
        Dead();
        if (scripthp > 0 && timer <= 0f)
        {
            AimTturtle();
            AimTcat();
            AimTribb();
        }


    }
    // 按Z瞄準烏龜發射
    private void AimTturtle()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            hit = GameObject.Find("Tturtle");
            _str += speed;

        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按X瞄準貓發射
    private void AimTcat()
    {
        if (Input.GetKey(KeyCode.X))
        {
            hit = GameObject.Find("Tcat");
            _str += speed;

        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按C瞄準兔子發射
    private void AimTribb()
    {
        if (Input.GetKey(KeyCode.C))
        {
            hit = GameObject.Find("Tribb");
            _str += speed;

        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    private void Power()
    {
        if (Input.GetKeyDown(KeyCode.S)) huge = true;
        if (Input.GetKeyDown(KeyCode.A)) power = true;
        if (Input.GetKeyDown(KeyCode.D)) protection = true;
    }
    public void Fire()
    {
        GameObject temp = Instantiate(prop, transform.position + transform.up * 1.5f, transform.rotation);
        temp.transform.LookAt(hit.transform.position + transform.up * 1.5f);
        if (huge == true) temp.transform.localScale = new Vector3(temp.transform.localScale.x * 2, temp.transform.localScale.y * 2, temp.transform.localScale.z * 2);
        if (power == true)
        {
            temp.GetComponent<Rigidbody>().useGravity = false;
            temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            temp.GetComponent<Rigidbody>().AddForce(temp.transform.forward * str * 2);
            //temp.transform.Rotate(new Vector3(50f, 0, 0));
            temp.GetComponent<ThrowObject>().turn = 999f;
        }
        //Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        temp.GetComponent<Rigidbody>().AddForce(0, 500, 0);
        temp.GetComponent<Rigidbody>().AddForce(temp.transform.forward * str);
        Destroy(temp, 5f);
        huge = false;
        power = false;
        timer = 2f;
    }
    public IEnumerator Protection()
    {
        Protectionsphere.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        Protectionsphere.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Prop" && other.gameObject.name != prop.name + "(Clone)")
        {
            ani.SetTrigger("GetHit");
            scripthp -= damage;
            Destroy(other.gameObject);
        }

    }

    public void Dead()
    {
        if (scripthp <= 0)
        {
            ani.SetTrigger("Death");
        }
    }

}
