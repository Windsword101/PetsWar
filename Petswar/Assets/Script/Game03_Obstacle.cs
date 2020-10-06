using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game03_Obstacle : MonoBehaviour
{
    [Header("障礙物移動速度")]
    public float speed;
    private float floatX, floatZ;
    // 產生後等待數秒再開始移動
    private float timer;
    private void Awake()
    {
        Physics.IgnoreLayerCollision(13, 14, false);
        Destroy(gameObject, 15);
    }
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1) transform.Translate(new Vector3(floatX, 0, floatZ) * Time.deltaTime * speed);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Bottom")
        {
            floatZ = 1;
        }
        if (other.name == "Top")
        {
            floatZ = -1;
        }
        if (other.name == "Right")
        {
            floatX = -1;
        }
        if (other.name == "Left")
        {
            floatX = 1;
        }
        Physics.IgnoreLayerCollision(13, 14);
    }

}
