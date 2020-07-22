
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player2;
    [Header("丟擲速度")]
    public float speed;
    [Header("丟擲物品")]
    public GameObject prop;
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Fire();
        }
    }
    public void Fire()
    {
        GameObject temp = Instantiate(prop,transform.position,Quaternion.identity);
        Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        temp.transform.LookAt(vec);
        temp.GetComponent<Rigidbody>().AddForce(temp.transform.forward*1000);
        //temp.transform.position = Vector3.MoveTowards(temp.transform.position, player2.transform.position, speed);
        Destroy(temp, 5f);

    }
}
