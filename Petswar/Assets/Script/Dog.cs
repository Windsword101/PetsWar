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
    public Image attCD, powerCD, hugeCD, protectionCD;
    public float scripthp;

    // 力道範圍
    protected GameObject str_bar;
    protected GameObject hp_bar;
    protected float str;
    protected float _str;
    protected float timer;
    protected float _timer = 1f;
    protected Animator ani;
    protected bool huge = false, power = false, protection = false;
    protected float powerTimer, _powerTimer = 3f;
    protected float HugeTimer, _HugeTimer = 10f;
    protected float protectionTimer, _protectionTimer = 15f;
    protected AudioSource throwsound, deadsound, hitsound;



    private void Awake()
    {

        hp_bar = GameObject.Find("dogHp");
        str_bar = GameObject.Find("dogBar");
    }
    private void Start()
    {
        deadsound = GameObject.Find("Dead").GetComponent<AudioSource>();
        throwsound = GameObject.Find("Throw").GetComponent<AudioSource>();
        hitsound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        Physics.IgnoreLayerCollision(8, 9);
        scripthp = hp;
        powerTimer = _powerTimer;
        HugeTimer = _HugeTimer;
        protectionTimer = _protectionTimer;
        ani = GetComponent<Animator>();
    }
    private void Update()
    {

        timer -= Time.deltaTime;
        HugeTimer -= Time.deltaTime;
        protectionTimer -= Time.deltaTime;
        attCD.fillAmount = timer / _timer;
        powerCD.fillAmount = powerTimer / _powerTimer;
        hugeCD.fillAmount = HugeTimer / _HugeTimer;
        protectionCD.fillAmount = protectionTimer / _protectionTimer;
        hp_bar.GetComponent<Image>().fillAmount = scripthp / hp;
        str_bar.GetComponent<Image>().fillAmount = _str / 600f;
        str = Mathf.Clamp(_str, 0f, 600f);
        if (protection == true)
        {
            StartCoroutine("Protection");
        }
        if(scripthp > 0)
        Power();
        
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
        if (Input.GetKeyDown(KeyCode.S) && HugeTimer <= 0)
        {
            huge = true;
            HugeTimer = 10f;
        }
        if (Input.GetKeyDown(KeyCode.A) && powerTimer <= 0)
        {

            power = true;
            powerTimer = 3f;
        }

        if (Input.GetKeyDown(KeyCode.D) && protectionTimer <= 0)
        {
            protection = true;
            protectionTimer = 15f;
        }
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

            temp.GetComponent<ThrowObject>().turn = 999f;
        }

        temp.GetComponent<Rigidbody>().AddForce(0, 500, 0);
        temp.GetComponent<Rigidbody>().AddForce(temp.transform.forward * str);
        Destroy(temp, 20f);
        huge = false;
        power = false;
        timer = 1f;
        throwsound.Play();
    }
    public IEnumerator Protection()
    {
        Protectionsphere.SetActive(true);
        yield return new WaitForSeconds(2f);
        protection = false;
        Protectionsphere.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Prop" && other.gameObject.name != prop.name + "(Clone)")
        {
            hitsound.Play();
            ani.SetTrigger("GetHit");
            scripthp -= damage;
            powerTimer--;
            Destroy(other.gameObject);
            Dead();
        }
    }

    public void Dead()
    {
        if (scripthp <= 0)
        {
            deadsound.Play();
            ani.SetTrigger("Death");
            GetComponent<Collider>().enabled = false;
        }
    }
}
