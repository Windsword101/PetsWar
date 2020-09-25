using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game05_Manager : MonoBehaviour
{
    public GameObject[] judgemetnArea;
    public Text[] ballNumber_text;
    public GameObject cube;
    public GameObject ball;
    public Transform instantiateBall;
    public static int[] ballNumber = new int[4];
    private float timer;
    private int a;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        print(a);
        for (int i = 0; i < ballNumber_text.Length; i++)
        {
            ballNumber_text[i].text = ballNumber[i].ToString("F0");
        }
        spawnBall();
        cube.transform.eulerAngles += new Vector3(0, 1, 0);
    }
    private void spawnBall()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            a += 1;
            Instantiate(ball, instantiateBall);
            timer = 0;
        }
    }
}
