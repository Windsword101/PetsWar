using UnityEngine;
using UnityEngine.UI;
using System;


public class Cat : PlayerControl
{


    private void Awake()
    {
        hp_bar = GameObject.Find("catHp");
        str_bar = GameObject.Find("catBar");
    }

    private void FixedUpdate()
    {
        if (Application.loadedLevelName == "Game03_Balance" || Application.loadedLevelName == "Game04_Tagyoure it") Move();

    }
    void Update()
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
            Power();
        if (scripthp > 0 && timer <= 0f)
        {
            AimTturtle();
            AimTdog();
            AimTribb();

        }
    }
    // 按1瞄準狗發射
    private void AimTdog()
    {
        if (Input.GetKey(KeyCode.Keypad2))
        {
            hit = GameObject.Find("TDog");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按2瞄準烏龜發射
    private void AimTturtle()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            hit = GameObject.Find("Tturtle");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按3瞄準兔子發射
    private void AimTribb()
    {
        if (Input.GetKey(KeyCode.Keypad3))
        {
            hit = GameObject.Find("Tribb");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    private void Power()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5) && HugeTimer <= 0)
        {
            huge = true;
            HugeTimer = 10f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) && powerTimer <= 0)
        {
            power = true;
            powerTimer = 3f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) && protectionTimer <= 0)
        {
            protection = true;
            protectionTimer = 15f;
        }
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");
        rb.AddForce(transform.forward * Math.Abs(h) * movespeed);
        rb.AddForce(transform.forward * Math.Abs(v) * movespeed);

        if (v == 1) angle = new Vector3(0, 0, 0);               // 前 Y 0
        else if (v == -1) angle = new Vector3(0, 180, 0);       // 後 Y 180
        else if (h == 1) angle = new Vector3(0, 90, 0);         // 右 Y 90
        else if (h == -1) angle = new Vector3(0, 270, 0);       // 左 Y 270
        // 只要類別後面有 : MonoBehaviour
        // 就可以直接使用關鍵字 transform 取得此物件的 Transform 元件
        transform.eulerAngles = angle;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, originalposition.x - moveArea, originalposition.x + moveArea), transform.position.y, Mathf.Clamp(transform.position.z, originalposition.z - moveArea, originalposition.z + moveArea));

    }

}
