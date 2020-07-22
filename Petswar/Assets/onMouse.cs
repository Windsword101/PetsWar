using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouse : MonoBehaviour
{
    public Player HitTarget;
    
    public void OnMouseDown()
    {
        HitTarget.hit = GameObject.Find(name);
    }
    public void OnMouseEnter()
    {
        this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void OnMouseExit()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
}
