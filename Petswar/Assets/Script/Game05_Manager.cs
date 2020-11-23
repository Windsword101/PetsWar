using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game05_Manager : MonoBehaviour
{
    [Header("照相機")]
    public GameObject camera;
    [Header("抽籤的箱子")]
    public List<GameObject> box = new List<GameObject>();
    [Header("玩家抽籤後分組的位置")]
    public List<Transform> _position = new List<Transform>();
    [Header("儲存角色物件")]
    public List<GameObject> player = new List<GameObject>();
    [Header("排球")]
    public GameObject ball;
    [Header("產生球")]
    public Transform SpwanBall;
    Vector3 cameraOriginalPos;
    Vector3 pos;
    bool gamestart;
    //抽籤
    public int groupA, groupB;
    private void Awake()
    {
        cameraOriginalPos = camera.transform.position;
        pos = camera.transform.position;
        pos.x = 39.6f;
        pos.y = 29;
        camera.transform.position = pos;
        foreach (GameObject p in player)
        {
            p.GetComponent<BoxCollider>().enabled = false;
        }
        Instantiate(ball, SpwanBall);
        /*int i = Random.Range(0, 4);
        Instantiate(ball, player[i].transform);*/
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < box.Count; i++)
        {
            if (box[i].GetComponent<BoxCollider>().enabled == false)
            {
                box.RemoveAt(i);
            }
        }
        if (box.Count == 0 && !gamestart)
        {
            StartCoroutine("GameStart");
        }
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        gamestart = true;
        foreach (GameObject p in player)
        {
            print("Yes");
            p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            p.GetComponent<BoxCollider>().enabled = true;
            p.transform.localPosition = new Vector3(0, 0, 0);
            camera.transform.position = cameraOriginalPos;
        }
        print("no");

    }
}
