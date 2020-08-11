using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DuelSceneManager : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject GM;
    public GameObject duelwinner;
    public Text winnertext;
    public GameObject dog, cat, ribb, turtle, player1, player2;
    private void Awake()
    {
        GM = GameObject.Find("GM");
        gamemanager = GameObject.Find("GM").GetComponent<GameManager>();
        if (gamemanager.dog.scripthp > 0 && gamemanager.cat.scripthp > 0)
        {
            dog.SetActive(true);
            cat.SetActive(true);
            dog.GetComponent<DuelDog>().hit = cat;
            cat.GetComponent<DuelCat>().hit = dog;
            ribb.GetComponent<DuelRibb>().dead = true;
            turtle.GetComponent<DuelTurtle>().dead = true;
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
            cat.GetComponent<DuelCat>().dead = true;
            turtle.GetComponent<DuelTurtle>().dead = true;
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
            ribb.GetComponent<DuelRibb>().dead = true;
            cat.GetComponent<DuelCat>().dead = true;
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
            dog.GetComponent<DuelDog>().dead = true;
            turtle.GetComponent<DuelTurtle>().dead = true;
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
            ribb.GetComponent<DuelRibb>().dead = true;
            dog.GetComponent<DuelDog>().dead = true;
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
            dog.GetComponent<DuelDog>().dead = true;
            cat.GetComponent<DuelCat>().dead = true;
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
        if (dog.GetComponent<DuelDog>().dead == false && cat.GetComponent<DuelCat>().dead == true && ribb.GetComponent<DuelRibb>().dead == true && turtle.GetComponent<DuelTurtle>().dead == true)
        {
            duelwinner.SetActive(true);
            winnertext.text = "狗獲勝！";
            Destroy(GM);
        }
        else if (dog.GetComponent<DuelDog>().dead == true && cat.GetComponent<DuelCat>().dead == false && ribb.GetComponent<DuelRibb>().dead == true && turtle.GetComponent<DuelTurtle>().dead == true)
        {
            duelwinner.SetActive(true);
            winnertext.text = "貓獲勝！";
            Destroy(GM);
        }
        else if (dog.GetComponent<DuelDog>().dead == true && cat.GetComponent<DuelCat>().dead == true && ribb.GetComponent<DuelRibb>().dead == false && turtle.GetComponent<DuelTurtle>().dead == true)
            {
            duelwinner.SetActive(true);
            winnertext.text = "兔子獲勝！";
            Destroy(GM);
        }
        else if (dog.GetComponent<DuelDog>().dead == true && cat.GetComponent<DuelCat>().dead == true && ribb.GetComponent<DuelRibb>().dead == true && turtle.GetComponent<DuelTurtle>().dead == false)
        {
            duelwinner.SetActive(true);
            winnertext.text = "烏龜獲勝！";
            Destroy(GM);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetKeyDown(KeyCode.W)) SceneManager.LoadScene("DuelScene");
    }
    public void Menu()
    {
        Application.LoadLevel("MenuScene");
        Destroy(GM);
        
    }
}
