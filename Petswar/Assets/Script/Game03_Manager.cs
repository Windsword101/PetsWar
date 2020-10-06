using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game03_Manager : MonoBehaviour
{
    [Header("障礙物物件")]
    public GameObject obstacle;
    [Header("障礙物生成點")]
    public GameObject[] CreatePos;
    [Header("障礙物產出間隔")]
    public float WaitTime;
    [Header("超過規定時間障礙物數量增加")]
    public float timer;
    private float _timer;
    //每個方向產生的障礙物數量
    int num = 1;
    void Start()
    {
        InvokeRepeating("CreateObstacle", WaitTime, WaitTime);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= timer * 3) num = 4;
        else if (_timer >= timer * 2) num = 3;
        else if (_timer >= timer) num = 2;
    }

    void CreateObstacle()
    {
        Vector3 randomPos, randomPos1 = Vector3.zero, randomPos2 = Vector3.zero, randomPos3 = Vector3.zero;
        for (int j = 0; j < num;)
        {
            for (int i = 0; i < CreatePos.Length; i++)
            {
                Vector3 MaxValue = CreatePos[i].GetComponent<Collider>().bounds.max;
                Vector3 MinValue = CreatePos[i].GetComponent<Collider>().bounds.min;
                if (CreatePos[i].name == "Bottom" || CreatePos[i].name == "Top")
                {
                    randomPos = new Vector3(Random.Range(MinValue.x, MaxValue.x), MaxValue.y, MaxValue.z);
                }
                else
                {
                    randomPos = new Vector3(MaxValue.x, MaxValue.y, Random.Range(MinValue.z, MaxValue.z));

                }
                Collider[] Hitcolliders = Physics.OverlapSphere(randomPos, 0.6f, 1 << 14);
                if (Hitcolliders.Length <= 0) Instantiate(obstacle, randomPos, CreatePos[i].transform.rotation);
            }
            j++;
        }
    }
}
