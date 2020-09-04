using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance_Column : MonoBehaviour
{
    private float timer;
    private Vector3 scaleChange;
    void Start()
    {
        scaleChange = new Vector3(-1f, 0f, -1f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (transform.localScale.x >= 1 && timer >= 5)
        {
            transform.localScale += scaleChange;
            timer = 0;
        }
    }
}
