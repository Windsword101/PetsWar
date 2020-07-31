using UnityEngine.UI;
using UnityEngine;

public class Turtle : Dog
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        str = Mathf.Clamp(_str, 0f, 600f);
        AimTribb();
        AimTdog();
        AimTCat();
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
}

