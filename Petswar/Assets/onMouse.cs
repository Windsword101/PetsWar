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
        this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
    public void OnMouseExit()
    {
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
    }
}
