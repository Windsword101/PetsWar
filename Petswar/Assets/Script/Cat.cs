using UnityEngine;
using UnityEngine.UI;

public class Cat : Dog
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
        AimTribb();
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
}
