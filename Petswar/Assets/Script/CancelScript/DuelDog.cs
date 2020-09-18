using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using KID;

public class DuelPlayerControl : MonoBehaviour
{
    public PlayerData playerdata;
    [Header("丟擲對象")]
    public GameObject hit;
    [Header("護盾")]
    public GameObject Protectionsphere;
    [Header("丟擲物品")]
    public GameObject prop;
    public Animator ani;
    public AudioSource throwsound, deadsound, hitsound;
    public bool dead = false, fire = true, fake = true, protection = true;

    private void Start()
    {
        deadsound = GameObject.Find("Dead").GetComponent<AudioSource>();
        throwsound = GameObject.Find("Throw").GetComponent<AudioSource>();
        hitsound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        Physics.IgnoreLayerCollision(8, 9);
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        if (DuelSceneManager.timer >= 4.5f)
        {
            fire = true;
            fake = true;
            protection = true;
        }
        if (DuelSceneManager.pause == false && dead == false) Power();
    }
    private void Power()
    {
        if (Input.GetKeyDown(playerdata.left))
        {
            if (fire == true) Fire(1);
            fire = false;
        }
        // 假動作
        if (Input.GetKeyDown(playerdata.backward))
        {
            if (fake == true) Fire(0);
            fake = false;
        }

        if (Input.GetKeyDown(playerdata.right))
        {
            if (protection == true) StartCoroutine("Protection");
            protection = false;
        }
    }
    public void Fire(int i)
    {
        if (i == 0)
        {
            ani.SetTrigger("throw");
            throwsound.Play();
        }

        else
        {
            GameObject temp = Instantiate(prop, transform.position + transform.up * 1.5f, transform.rotation);
            temp.transform.LookAt(hit.transform.position + transform.up * 1.5f);
            temp.GetComponent<Rigidbody>().useGravity = false;
            temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            temp.GetComponent<Rigidbody>().AddForce(temp.transform.forward * 1500);
            temp.GetComponent<ThrowObject>().turn = 999f;
            Destroy(temp, 20f);
            throwsound.Play();

            ani.SetTrigger("throw");
        }
    }
    public IEnumerator Protection()
    {
        Protectionsphere.SetActive(true);
        yield return new WaitForSeconds(1f);
        Protectionsphere.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prop" && other.gameObject.name != prop.name + "(Clone)")
        {
            hitsound.Play();
            ani.SetTrigger("GetHit");
            Destroy(other.gameObject);
            Dead();
        }
    }
    public void Dead()
    {
        dead = true;
        deadsound.Play();
        ani.SetTrigger("Death");
        GetComponent<Collider>().enabled = false;
    }
}

