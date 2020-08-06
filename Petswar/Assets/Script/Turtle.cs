﻿using UnityEngine.UI;
using UnityEngine;

public class Turtle : Dog
{


    private void Awake()
    {
        hp_bar = GameObject.Find("turtleHp");
        str_bar = GameObject.Find("turtleBar");
    }
    // Update is called once per frame
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

            AimTribb();
            AimTdog();
            AimTCat();
        }
    }
    // 按B瞄準狗發射
    private void AimTdog()
    {
        if (Input.GetKey(KeyCode.Delete))
        {
            hit = GameObject.Find("TDog");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Delete))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按N瞄準烏龜發射
    private void AimTribb()
    {
        if (Input.GetKey(KeyCode.End))
        {
            hit = GameObject.Find("Tribb");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.End))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按M瞄準貓發射
    private void AimTCat()
    {
        if (Input.GetKey(KeyCode.PageDown))
        {
            hit = GameObject.Find("Tcat");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.PageDown))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    private void Power()
    {
        if (Input.GetKeyDown(KeyCode.Home) && HugeTimer <= 0)
        {
            huge = true;
            HugeTimer = 10f;
        }
        if (Input.GetKeyDown(KeyCode.Insert) && powerTimer <= 0)
        {
            power = true;
            powerTimer = 3f;
        }
        if (Input.GetKeyDown(KeyCode.PageUp) && protectionTimer <= 0)
        {
            protection = true;
            protectionTimer = 15f;
        }
    }
}

