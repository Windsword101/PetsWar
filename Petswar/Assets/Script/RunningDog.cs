using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RunningDog : MonoBehaviour
{
    [Header("加速度")]
    public float _speed;
    private Rigidbody2D rig;
    private bool run = false;
    private float speed;
    private void Awake()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (speed > 0)
        {
            speed -= 0.02f;
        }
        if (Input.GetKeyDown(KeyCode.W) && run == false)
        {
            speed += _speed;
            run = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && run == true)
        {
            rig.AddForce(new Vector2(speed, 0));
            run = false;
        }
    }
}
