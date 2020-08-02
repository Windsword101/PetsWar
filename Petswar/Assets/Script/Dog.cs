using UnityEngine;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    public GameObject hit;
    [Header("集氣速度"), Range(0f, 5f)]
    public float speed = 4f;
    public float hp = 100f;
    protected GameObject str_bar;
    protected GameObject hp_bar;
    [Header("丟擲物品")]
    public GameObject prop;

    // 力道範圍
    protected float scripthp;
    protected float str;
    protected float _str;
    protected float timer;
    protected Animator ani;

    private void Awake()
    {
        hp_bar = GameObject.Find("dogHp");
        str_bar = GameObject.Find("dogBar");
    }
    private void Start()
    {
        scripthp = hp;
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        hp_bar.GetComponent<Image>().fillAmount = scripthp / hp;
        str_bar.GetComponent<Image>().fillAmount = _str / 600f;
        str = Mathf.Clamp(_str, 0f, 600f);
        AimTturtle();
        AimTcat();
        AimTribb();


    }
    // 按Z瞄準烏龜發射
    private void AimTturtle()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            hit = GameObject.Find("Tturtle");
            _str += speed;
            timer += Time.deltaTime;
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
            timer += Time.deltaTime;
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
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    public void Fire()
    {
        {
            GameObject temp = Instantiate(prop, transform.position, transform.rotation);
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            temp.transform.LookAt(hit.transform.position);
            temp.GetComponent<Rigidbody>().AddForce(0, 500, 0);
            temp.GetComponent<Rigidbody>().AddForce(temp.transform.forward * str);
            //temp.transform.position = Vector3.MoveTowards(temp.transform.position, player2.transform.position, speed);
            Destroy(temp, 5f);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "" && gameObject.name != this.name)
        {

        }
    }
}
