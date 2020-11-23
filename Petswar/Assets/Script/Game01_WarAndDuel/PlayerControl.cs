using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KID;

public class PlayerControl : MonoBehaviour
{
    #region 分數儲存
    public int PlayerScore;
    #endregion
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
    protected int game01_Score;
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
    public Sprite stand, runL, runR;
    private Rigidbody2D rig;
    private bool run = false;
    private float runspeed;
    #endregion
    #region 搶蛋糕模式
    [Header("持有蛋糕時間計算")]
    public Text game04_scoretext;
    //蛋糕持有秒數
    public float game04_caketimer;
    //分數
    public int game04_Score;
    //是否持有蛋糕
    private bool cakeOn;
    #endregion
    #region 躲避球模式
    public int game03_life = 3;
    //是否進入無敵狀態
    private bool isInvincible;
    //無敵狀態持續時間
    private float timeSpentInvincible;
    #endregion
    #region 打排球模式
    [Header("跳躍高度")]
    public float jumpHeight;
    // 是否碰到地面
    private bool isGround = true;
    // 往前撲的時候限制移動
    private bool move = true;
    //向前撲
    private float dashTime;

    #endregion

    private void Awake()
    {
        move = true;
        if (Application.loadedLevelName == "Game02_running")
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
        ani = GetComponent<Animator>();
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
    }
    private void FixedUpdate()
    {
        if (Application.loadedLevelName == "Game03_Balance" || Application.loadedLevelName == "Game04_Tagyoure it" || Application.loadedLevelName == "Game05_volleyball" || Application.loadedLevelName == "Game07_DodgeBall") Move();
        if (Application.loadedLevelName == "Game05_volleyball") JumpAndDash();
    }
    private void Update()
    {
        if (Application.loadedLevelName == "Game07_DodgeBall") ThrowBall();
        if (Application.loadedLevelName == "Game05_volleyball")
        {
            Serve();
            dashTime -= Time.deltaTime;
            if (dashTime <= 0) move = true;
        }
        ;
        if (Application.loadedLevelName == "Game04_Tagyoure it") Score();
        if (Application.loadedLevelName == "Game03_Balance") Invincible();
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
    // 按鍵A瞄準發射
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
    // 按鍵B瞄準發射
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
    // 按鍵C瞄準發射
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGround = true;
            print(isGround);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            game03_life -= 1;
            Game03_Death();
            StartCoroutine("Game03_LeavePlatform");
        }

        if (collision.gameObject.tag == "Floor")
        {
            isGround = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //排球模式抽籤
        if (other.gameObject.tag == "DrawLots")
        {
            Game05_Manager gm = GameObject.Find("Game05_Manager").GetComponent<Game05_Manager>();
            int randomNumber;
            randomNumber = Random.Range(0, 2);
            Vector3 pos = other.gameObject.transform.position;
            pos.y = -0.49f * 2;
            other.gameObject.transform.position = pos;
            pos.y = 0;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.position = pos;
            if (randomNumber == 0 && gm.groupA < 2)
            {
                gm.groupA++;
                other.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 255);
                for (int i = 0; i < gm._position.Count - 2; i++)
                {
                    if (gm._position[i].childCount == 0) gameObject.transform.SetParent(gm._position[i]);

                }
            }
            else
            {
                gm.groupB++;
                if (gm.groupB <= 2) other.GetComponent<Renderer>().material.color = new Color(0, 0, 255, 255);
                else other.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 255);
                for (int i = 2; i < gm._position.Count; i++)
                {
                    if (gm._position[i].childCount == 0) gameObject.transform.SetParent(gm._position[i]);
                    else
                    {
                        for (int j = 0; j < gm._position.Count - 2; j++)
                        {
                            if (gm._position[j].childCount == 0) gameObject.transform.SetParent(gm._position[j]);
                        }
                    }
                }

            }
            other.GetComponent<BoxCollider>().enabled = false;
        }
        //排球模式殺球
        if (other.gameObject.tag == "Ball")
        {
            print("Hit");
            if (Input.GetKeyDown(playerdata.b))
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(100, 50, 0));
            }
        }

        // 躲避模式被障礙物撞到
        if (other.gameObject.tag == "Obstacle")
        {
            gameObject.layer = 15;
            isInvincible = true;
            game03_life -= 1;
            ani.SetTrigger("GetHit");
            Game03_Death();

        }
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
    }
    private void OnTriggerStay(Collider other)
    {
        /*//舊排球模式把球抓起來
        if (other.gameObject.tag == "Ball" && gameObject.transform.Find("ball(Clone)") == null)
        {
            if (Input.GetKeyDown(playerdata.a))
            {
                other.gameObject.transform.SetParent(gameObject.transform);
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>(), true);
                if (other.gameObject.GetComponent<Rigidbody>() == true)
                {
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                other.gameObject.transform.localPosition = new Vector3(0, 2, 2);
            }
        }*/
        if (other.gameObject.tag == "Ball")
        {
            if (Input.GetKeyDown(playerdata.b) && isGround == false)
            {
                if (gameObject.transform.position.x >= 0)
                    other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-80, -30, 0));
                else
                    other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(80, -30, 0));
            }
        }
        //鬼抓人場景碰到帽子
        if (other.gameObject.tag == "Cake")
        {
            if (Input.GetKeyDown(playerdata.a))
            {
                other.gameObject.transform.SetParent(gameObject.transform);
                other.gameObject.transform.localPosition = new Vector3(0, 5, 0);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //跑步遊戲抵達終點
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
            ani.SetTrigger("Death");
            GetComponent<Collider>().enabled = false;
            deadsound.Play();
        }
    }
    private void Move()
    {
        if (move)
        {
            Vector3 mVelocity = Vector3.zero;
            /*if (Input.GetKey(playerdata.forward) && Input.GetKey(playerdata.right)) angle = new Vector3(0, 45, 0);
            if (Input.GetKey(playerdata.forward) && Input.GetKey(playerdata.left)) angle = new Vector3(0, 315, 0);
            if (Input.GetKey(playerdata.backward) && Input.GetKey(playerdata.right)) angle = new Vector3(0, 135, 0);
            if (Input.GetKey(playerdata.backward) && Input.GetKey(playerdata.left)) angle = new Vector3(0, 225, 0);*/

            if (Input.GetKey(playerdata.left))
            {
                mVelocity.x = -1.0f;
                angle = new Vector3(0, 270, 0);
                //rb.AddForce(transform.forward * movespeed);
            }
            if (Input.GetKey(playerdata.right))
            {
                mVelocity.x = 1.0f;
                angle = new Vector3(0, 90, 0);
                //rb.AddForce(transform.forward * movespeed);
            }
            if (Input.GetKey(playerdata.forward))
            {
                mVelocity.z = 1.0f;
                angle = new Vector3(0, 0, 0);
                //rb.AddForce(transform.forward * movespeed);
            }
            if (Input.GetKey(playerdata.backward))
            {
                mVelocity.z = -1.0f;
                angle = new Vector3(0, 180, 0);
                //rb.AddForce(transform.forward * movespeed);
            }
            rb.AddForce(mVelocity.normalized * movespeed);
            transform.eulerAngles = angle;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, originalposition.x - moveArea, originalposition.x + moveArea), transform.position.y, Mathf.Clamp(transform.position.z, originalposition.z - moveArea, originalposition.z + moveArea));

        }
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

    private void Score()
    {
        game04_scoretext.text = game04_caketimer.ToString("F2");
        if (gameObject.transform.Find("Cake") != null)
        {
            cakeOn = true;
            game04_caketimer += Time.deltaTime;
        }
        if (cakeOn == true)
        {
            if (Input.GetKeyDown(playerdata.b))
            {
                gameObject.transform.Find("Cake").transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
                gameObject.transform.Find("Cake").SetParent(null);
                cakeOn = false;
            }
        }
    }
    private void ThrowVolleyBall()
    {
        if (gameObject.transform.Find("ball(Clone)") != null)
        {
            if (Input.GetKeyDown(playerdata.b))
            {
                Transform dir = gameObject.transform.Find("ThrowDirection").GetComponent<Transform>();
                gameObject.transform.Find("ball(Clone)").transform.LookAt(dir);
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().AddForce(gameObject.transform.Find("ball(Clone)").transform.forward * 500);
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().AddForce(0, 500, 0);
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), gameObject.transform.Find("ball(Clone)").GetComponent<Collider>(), false);
                gameObject.transform.Find("ball(Clone)").transform.SetParent(null);
            }
        }
    }
    /// <summary>
    /// 無敵狀態
    /// </summary>
    /// <param name="t"></param>
    private void Invincible()
    {
        if (isInvincible)
        {
            Component[] render = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            timeSpentInvincible += Time.deltaTime;
            if (timeSpentInvincible < 1.5f)
            {
                float remainder = timeSpentInvincible % 0.3f;
                foreach (SkinnedMeshRenderer r in render)
                {
                    r.enabled = remainder > 0.15f;
                }
            }
            else
            {
                gameObject.layer = 8;
                isInvincible = false;
                timeSpentInvincible = 0f;
                foreach (SkinnedMeshRenderer r in render)
                {
                    r.enabled = true;
                }
            }
        }
    }
    private void Game03_Death()
    {
        if (game03_life == 0)
        {
            GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
            gameObject.layer = 15;
            for (int i = 0; i < p.Length; i++)
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), p[i].GetComponent<Collider>());
            }
            ani.SetTrigger("Death");
            GetComponent<PlayerControl>().enabled = false;
        }
    }
    IEnumerator Game03_LeavePlatform()
    {
        yield return new WaitForSeconds(2);
        transform.position = originalposition;
        isInvincible = true;
    }
    private void ThrowBall()
    {
        if (gameObject.transform.Find("ball(Clone)") != null)
        {
            if (Input.GetKey(playerdata.b))
            {
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                gameObject.transform.Find("ball(Clone)").transform.Rotate(Vector3.right * 10 + Vector3.forward * 10);
            }
            if (Input.GetKeyUp(playerdata.b))
            {
                Transform dir = gameObject.transform.Find("ThrowDirection").GetComponent<Transform>();
                gameObject.transform.Find("ball(Clone)").transform.LookAt(dir);
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().AddForce(gameObject.transform.Find("ball(Clone)").transform.forward * 3000);
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), gameObject.transform.Find("ball(Clone)").GetComponent<Collider>(), false);
                gameObject.transform.Find("ball(Clone)").transform.SetParent(null);
            }
        }
    }
    private void JumpAndDash()
    {
        if (Input.GetKeyDown(playerdata.a) && isGround && gameObject.transform.Find("ball(Clone)") == null)
        {
            print("jump");
            rb.AddForce(0, jumpHeight, 0);
        }
        if (Input.GetKeyDown(playerdata.b) && isGround && move)
        {
            move = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * 350);
            dashTime = 0.5f;
            //rb.MovePosition(transform.position + transform.forward );
        }
    }
    private void Serve()
    {
        if (gameObject.transform.Find("ball(Clone)") != null)
        {
            if (Input.GetKeyDown(playerdata.a))
            {
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gameObject.transform.Find("ball(Clone)").GetComponent<Rigidbody>().AddForce(0, 45, 0);
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), gameObject.transform.Find("ball(Clone)").GetComponent<Collider>(), false);
                gameObject.transform.Find("ball(Clone)").transform.SetParent(null);
            }
        }
    }
}
