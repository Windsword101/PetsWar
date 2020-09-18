using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KID;

public class NewBehaviourScript : MonoBehaviour
{
    public Button a;
    IEnumerator Start()
    {
        //string b = RandomScene.LoadRandomScene();
        if (GameObject.FindGameObjectsWithTag("Button").Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        yield return null;

        a.onClick.AddListener(() =>
        {
           // string a = RandomScene.LoadRandomScene();
            print(a);
        }
        );
    }
}
