using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject hit;
    [Header("集氣速度"), Range(0f, 5f)]
    public float speed = 4f;
    public GameObject str_bar;
    [Header("丟擲物品")]
    public GameObject prop;

    // 力道範圍
    private float str;
    private float _str;
    private float timer;
    

    private void Start()
    {
       
        str_bar = GameObject.Find("集氣條 (1)");
    }
    private void Update()
    {
        str_bar.GetComponent<Image>().fillAmount = _str / 600f;
        str = Mathf.Clamp(_str, 0f, 600f);
        if (Input.GetKey(KeyCode.Space))
        {
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _str = 0;
            timer = 0;
            Fire();
        }


    }
    public void Fire()
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
