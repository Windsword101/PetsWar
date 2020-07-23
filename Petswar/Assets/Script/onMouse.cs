using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouse : MonoBehaviour
{
    public Player HitTarget;
    private Vector3 trans;

    private void Start()
    {
        trans = this.transform.localScale;
    }
    private void Update()
    {
        HitTarget = FindObjectOfType<Player>();
    }
    public void OnMouseDown()
    {
        HitTarget.hit = GameObject.Find(name);
    }
    public void OnMouseEnter()
    {
        this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
    public void OnMouseExit()
    {
        this.transform.localScale = trans;
        
    }
}
