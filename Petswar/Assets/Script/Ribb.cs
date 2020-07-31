using UnityEngine;
using UnityEngine.UI;

public class Ribb : Dog
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        str = Mathf.Clamp(_str, 0f, 600f);
        AimTturtle();
        AimTdog();
        AimTCat();
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
}
