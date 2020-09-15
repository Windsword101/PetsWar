using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerControl dog,cat,ribb,turtle;
    public GameObject Winner;
    public Text winnertext;
    public GameObject countdown;
    private bool cdtext = true;
    private float timer = 10;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Application.loadedLevelName == "GameScene")
        {
            if (dog.scripthp <= 0 && cat.scripthp <= 0 && ribb.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                Winner.SetActive(true);
                winnertext.text = "烏龜獲勝！";
            }
            if (dog.scripthp <= 0 && cat.scripthp <= 0 && turtle.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                Winner.SetActive(true);
                winnertext.text = "兔子獲勝！";
            }
            if (dog.scripthp <= 0 && ribb.scripthp <= 0 && turtle.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                Winner.SetActive(true);
                winnertext.text = "貓獲勝！";
            }
            if (cat.scripthp <= 0 && ribb.scripthp <= 0 && turtle.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                Winner.SetActive(true);
                winnertext.text = "狗獲勝！";
            }
            if ((dog.scripthp <= 0 && cat.scripthp <= 0) || (dog.scripthp <= 0 && ribb.scripthp <= 0) || (dog.scripthp <= 0 && turtle.scripthp <= 0) || (cat.scripthp <= 0 && ribb.scripthp <= 0) || (cat.scripthp <= 0 && turtle.scripthp <= 0) || (ribb.scripthp <= 0 && turtle.scripthp <= 0))
            {
                countdown.SetActive(true);
                if (timer > 0 && cdtext == true) timer -= Time.deltaTime;
            }
            if (cdtext == true) countdown.GetComponent<Text>().text = timer.ToString("F0");
            if (timer <= 0) SceneManager.LoadScene("DuelScene");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MenuScene");
                Destroy(gameObject);
            }
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
        Destroy(gameObject);
    }
}
