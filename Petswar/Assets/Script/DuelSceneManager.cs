using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelSceneManager : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject dog, cat, ribb, turtle , player1, player2;
    private void Awake()
    {
        gamemanager = GameObject.Find("GM").GetComponent<GameManager>();
        if (gamemanager.dog.scripthp > 0 && gamemanager.cat.scripthp > 0)
        {
            dog.SetActive(true);
            cat.SetActive(true);
            dog.GetComponent<DuelDog>().hit = cat;
            cat.GetComponent<DuelCat>().hit = dog;
            dog.transform.position = player1.transform.position;
            dog.transform.rotation = player1.transform.rotation;
            cat.transform.position = player2.transform.position;
            cat.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.dog.scripthp > 0 && gamemanager.ribb.scripthp > 0)
        {
            dog.SetActive(true);
            ribb.SetActive(true);
            dog.GetComponent<DuelDog>().hit = ribb;
            ribb.GetComponent<DuelRibb>().hit = dog;
            dog.transform.position = player1.transform.position;
            dog.transform.rotation = player1.transform.rotation;
            ribb.transform.position = player2.transform.position;
            ribb.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.dog.scripthp > 0 && gamemanager.turtle.scripthp > 0)
        {
            dog.SetActive(true);
            turtle.SetActive(true);
            dog.GetComponent<DuelDog>().hit = turtle;
            turtle.GetComponent<DuelTurtle>().hit = dog;
            dog.transform.position = player1.transform.position;
            dog.transform.rotation = player1.transform.rotation;
            turtle.transform.position = player2.transform.position;
            turtle.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.cat.scripthp > 0 && gamemanager.ribb.scripthp > 0)
        {
            ribb.SetActive(true);
            cat.SetActive(true);
            cat.GetComponent<DuelCat>().hit = ribb;
            ribb.GetComponent<DuelRibb>().hit = cat;
            ribb.transform.position = player1.transform.position;
            ribb.transform.rotation = player1.transform.rotation;
            cat.transform.position = player2.transform.position;
            cat.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.cat.scripthp > 0 && gamemanager.turtle.scripthp > 0)
        {
            cat.SetActive(true);
            turtle.SetActive(true);
            turtle.GetComponent<DuelTurtle>().hit = cat;
            cat.GetComponent<DuelCat>().hit = turtle;
            cat.transform.position = player1.transform.position;
            cat.transform.rotation = player1.transform.rotation;
            turtle.transform.position = player2.transform.position;
            turtle.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.ribb.scripthp > 0 && gamemanager.turtle.scripthp > 0)
        {
            ribb.SetActive(true);
            turtle.SetActive(true);
            ribb.GetComponent<DuelRibb>().hit = turtle;
            turtle.GetComponent<DuelTurtle>().hit = ribb;
            ribb.transform.position = player1.transform.position;
            ribb.transform.rotation = player1.transform.rotation;
            turtle.transform.position = player2.transform.position;
            turtle.transform.rotation = player2.transform.rotation;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
