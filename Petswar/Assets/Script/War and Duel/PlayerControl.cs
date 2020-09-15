using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using KID;

public class PlayerControl : MonoBehaviour
{
    float h;
    float v;
    #region 經典模式
    public PlayerData playerdata;
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
    [Header("移動速度")]
    public float movespeed = 10f;
    [Header("移動範圍")]
    public float moveArea = 999f;
    protected Vector3 angle = new Vector3(0, 180, 0);
    protected Vector3 originalposition;

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
    protected Rigidbody rb;
    #endregion
    #region 跑步模式
    [Header("加速度")]
    public float _runspeed;
    public Sprite stand,runL, runR;
    private Rigidbody2D rig;
    private bool run = false;
    private float runspeed;
    #endregion

    private void Awake()
    {
        if(Application.loadedLevelName == "Game02_running")
        {
            rig = gameObject.GetComponent<Rigidbody2D>();
        }
        if (Application.loadedLevelName == "GameScene")
        {
            if (gameObject.name == "TDog")
            {
                playerdata.targetA = GameObject.Find("Tturtle");
                playerdata.targetB = GameObject.Find("Tcat");
                playerdata.targetC = GameObject.Find("Tribb");
                hp_bar = GameObject.Find("dogHp");
                str_bar = GameObject.Find("dogBar");
            }
            if (gameObject.name == "Tcat")
            {
                playerdata.targetA = GameObject.Find("Tturtle");
                playerdata.targetB = GameObject.Find("TDog");
                playerdata.targetC = GameObject.Find("Tribb");
                hp_bar = GameObject.Find("catHp");
                str_bar = GameObject.Find("catBar");
            }
            if (gameObject.name == "Tturtle")
            {
                playerdata.targetA = GameObject.Find("TDog");
                playerdata.targetB = GameObject.Find("Tcat");
                playerdata.targetC = GameObject.Find("Tribb");
                hp_bar = GameObject.Find("turtleHp");
                str_bar = GameObject.Find("turtleBar");
            }
            if (gameObject.name == "Tribb")
            {
                playerdata.targetA = GameObject.Find("TDog");
                playerdata.targetB = GameObject.Find("Tturtle");
                playerdata.targetC = GameObject.Find("Tcat");
                hp_bar = GameObject.Find("ribbHp");
                str_bar = GameObject.Find("ribbBar");
            }
        }
    }
    private void Start()
    {
        originalposition = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        if (Application.loadedLevelName == "GameScene")
        {

            deadsound = GameObject.Find("Dead").GetComponent<AudioSource>();
            throwsound = GameObject.Find("Throw").GetComponent<AudioSource>();
            hitsound = GameObject.Find("HitSound").GetComponent<AudioSource>();
            Physics.IgnoreLayerCollision(8, 9);
            scripthp = hp;
            powerTimer = _powerTimer;
            HugeTimer = _HugeTimer;
            protectionTimer = _protectionTimer;
        }
        ani = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (Application.loadedLevelName == "Game03_Balance" || Application.loadedLevelName == "Game04_Tagyoure it") Move();

    }
    private void Update()
    {
        if (Application.loadedLevelName == "Game02_running") Running();

        if (Application.loadedLevelName == "GameScene")
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
            if (scripthp > 0)
            {

                Power();
            }
            if (scripthp > 0 && timer <= 0f)
            {
                AimTargetA();
                AimTargetB();
                AimTargetC();
            }
        }

    }
    // 按Z瞄準烏龜發射
    private void AimTargetA()
    {
        if (Input.GetKey(playerdata.a))
        {
            hit = playerdata.targetA;
            _str += speed;

        }
        if (Input.GetKeyUp(playerdata.a))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按X瞄準貓發射
    private void AimTargetB()
    {
        if (Input.GetKey(playerdata.b))
        {
            hit = playerdata.targetB;
            _str += speed;

        }
        if (Input.GetKeyUp(playerdata.b))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按C瞄準兔子發射
    private void AimTargetC()
    {
        if (Input.GetKey(playerdata.c))
        {
            hit = playerdata.targetC;
            _str += speed;

        }
        if (Input.GetKeyUp(playerdata.c))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    private void Power()
    {
        if (Input.GetKeyDown(playerdata.backward) && HugeTimer <= 0)
        {
            huge = true;
            HugeTimer = 10f;
        }
        if (Input.GetKeyDown(playerdata.left) && powerTimer <= 0)
        {

            power = true;
            powerTimer = 3f;
        }

        if (Input.GetKeyDown(playerdata.right) && protectionTimer <= 0)
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
        //經典模式被投擲物投中
        if (other.gameObject.tag == "Prop" && other.gameObject.name != prop.name + "(Clone)")
        {
            hitsound.Play();
            ani.SetTrigger("GetHit");
            scripthp -= damage;
            powerTimer--;
            Destroy(other.gameObject);
            Dead();
        }
        //鬼抓人場景碰到帽子
        if (other.gameObject.tag == "Hat")
        {
            other.gameObject.transform.SetParent(gameObject.transform);
            other.gameObject.transform.localPosition = new Vector3(0, 1.125f, 2);
        }
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FinishLine")
        {
            print(gameObject.name + "win");
            gameObject.GetComponent<SpriteRenderer>().sprite = stand;
            rig.constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<PlayerControl>().enabled = false;
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
    private void Move()
    {

        if (Input.GetKey(playerdata.left)) h = -1;
        if (Input.GetKey(playerdata.right)) h = 1;
        if (Input.GetKey(playerdata.forwad)) v = 1;
        if (Input.GetKey(playerdata.backward)) v = -1;
        if (Input.GetKeyUp(playerdata.left)) h = 0;
        if (Input.GetKeyUp(playerdata.right)) h = 0;
        if (Input.GetKeyUp(playerdata.forwad)) v = 0;
        if (Input.GetKeyUp(playerdata.backward)) v = 0;
        //float h = Input.GetAxis("Horizontal1");
        //float v = Input.GetAxis("Vertical1");
        rb.AddForce(transform.forward * Math.Abs(h) * movespeed);
        rb.AddForce(transform.forward * Math.Abs(v) * movespeed);

        if (v == 1) angle = new Vector3(0, 0, 0);               // 前 Y 0
        else if (v == -1) angle = new Vector3(0, 180, 0);       // 後 Y 180
        else if (h == 1) angle = new Vector3(0, 90, 0);         // 右 Y 90
        else if (h == -1) angle = new Vector3(0, 270, 0);       // 左 Y 270
        transform.eulerAngles = angle;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, originalposition.x - moveArea, originalposition.x + moveArea), transform.position.y, Mathf.Clamp(transform.position.z, originalposition.z - moveArea, originalposition.z + moveArea));
    }
    private void Running()
    {
        if (runspeed > 0)
        {
            runspeed -= 0.02f;
        }
        if (Input.GetKeyDown(playerdata.left) && run == false)
        {
            runspeed += _runspeed;
            gameObject.GetComponent<SpriteRenderer>().sprite = runL;
            run = true;
        }
        if (Input.GetKeyUp(playerdata.left))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stand;
        }
        if (Input.GetKeyDown(playerdata.right) && run == true)
        {
            rig.AddForce(new Vector2(runspeed, 0));
            gameObject.GetComponent<SpriteRenderer>().sprite = runR;
            run = false;
        }
        if (Input.GetKeyUp(playerdata.right))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stand;
        }
    }
}
