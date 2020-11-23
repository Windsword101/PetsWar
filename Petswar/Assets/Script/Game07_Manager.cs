using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game07_Manager : MonoBehaviour
{
    [Header("躲避球")]
    public GameObject ball;
    [Header("球的生成範圍")]
    public GameObject createPos;
    [Header("產生躲避球間隔")]
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBall", timer, timer);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnBall()
    {
        Vector3 randomPos;
        Vector3 maxValue = createPos.GetComponent<Collider>().bounds.max;
        Vector3 minValue = createPos.GetComponent<Collider>().bounds.min;
        randomPos = new Vector3(Random.Range(minValue.x + 1, maxValue.x - 1), 0.96f, Random.Range(minValue.z + 1, maxValue.z - 1));
        Instantiate(ball, randomPos, createPos.transform.rotation);
    }
}
