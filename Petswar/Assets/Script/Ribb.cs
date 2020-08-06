using UnityEngine;
using UnityEngine.UI;

public class Ribb : Dog
{


    private void Awake()
    {
        hp_bar = GameObject.Find("ribbHp");
        str_bar = GameObject.Find("ribbBar");
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
        if(protection == true)
        {
            StartCoroutine("Protection");
        }
        if (scripthp > 0)
            Power();
        if (scripthp > 0 && timer <= 0f)
        {

            AimTturtle();
            AimTdog();
            AimTCat();
        }
    }
    // 按B瞄準狗發射
    private void AimTdog()
    {
        if (Input.GetKey(KeyCode.B))
        {
            hit = GameObject.Find("TDog");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    // 按N瞄準烏龜發射
    private void AimTturtle()
    {
        if (Input.GetKey(KeyCode.N))
        {
            hit = GameObject.Find("Tturtle");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.N))
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
        if (Input.GetKey(KeyCode.M))
        {
            hit = GameObject.Find("Tcat");
            _str += speed;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            _str = 0;
            timer = 0;
            Fire();
            ani.SetTrigger("throw");
        }
    }
    private void Power()
    {
        if (Input.GetKeyDown(KeyCode.H) && HugeTimer <= 0)
        {
            huge = true;
            HugeTimer = 10f;
        }
        if (Input.GetKeyDown(KeyCode.G) && powerTimer <= 0)
        {
            power = true;
            powerTimer = 3f;
        }
        if (Input.GetKeyDown(KeyCode.J) && protectionTimer <= 0)
        {
            protection = true;
            protectionTimer = 15f;
        }
    }
}
