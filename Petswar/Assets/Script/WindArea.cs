using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindArea : MonoBehaviour
{
    public float strength;
    public Vector3 direction;
    public Image Windstr;
    private float timer;

    private void Update()
    {
        Windstr.fillAmount = strength / 4f;
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            strength = Random.Range(0, 5);
            timer = 0;
        }
    }
}
