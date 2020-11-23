using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerControl dog, cat, ribb, turtle;
    public List<GameObject> player = new List<GameObject>();
    /*    public GameObject Scoreboard;
        public Text[] game_result;
        public Text winnertext;*/
    public GameObject countdown;
    //用於排列名次
    public List<GameObject> _player = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
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
        for (int i = 0; i < player.Count; i++)
        {
            _player.Add(player[i]);
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
            ScoreCalculate();
            if (dog.scripthp <= 0 && cat.scripthp <= 0 && ribb.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                /* Scoreboard.SetActive(true);
                 winnertext.text = "烏龜獲勝！";*/
            }
            if (dog.scripthp <= 0 && cat.scripthp <= 0 && turtle.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                /* Scoreboard.SetActive(true);
                 winnertext.text = "兔子獲勝！";*/
            }
            if (dog.scripthp <= 0 && ribb.scripthp <= 0 && turtle.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                /* Scoreboard.SetActive(true);
                 winnertext.text = "貓獲勝！";*/
            }
            if (cat.scripthp <= 0 && ribb.scripthp <= 0 && turtle.scripthp <= 0)
            {
                cdtext = false;
                countdown.GetComponent<Text>().text = "";
                /*Scoreboard.SetActive(true);
                winnertext.text = "狗獲勝！";*/
            }
            if ((dog.scripthp <= 0 && cat.scripthp <= 0) || (dog.scripthp <= 0 && ribb.scripthp <= 0) || (dog.scripthp <= 0 && turtle.scripthp <= 0) || (cat.scripthp <= 0 && ribb.scripthp <= 0) || (cat.scripthp <= 0 && turtle.scripthp <= 0) || (ribb.scripthp <= 0 && turtle.scripthp <= 0))
            {
                countdown.SetActive(true);
                if (timer > 0 && cdtext == true) timer -= Time.deltaTime;
            }
            if (cdtext == true) countdown.GetComponent<Text>().text = timer.ToString("F0");
            if (timer <= 0)
            {
                players.Reverse();
                players.Insert(0, null);
                players.Insert(0, null);
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i] != null)
                    {
                        players[i].GetComponent<PlayerControl>().PlayerScore = KID.ScoreSystem.scores[i];
                        KID.ScoreSystem.PlayerScore[i] += player[i].GetComponent<PlayerControl>().PlayerScore;
                    }
                }
                SceneManager.LoadScene("DuelScene");
            }

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

    // 計算分數
    private void ScoreCalculate()
    {
        for (int i = 0; i < _player.Count; i++)
        {
            if (_player[i].GetComponent<PlayerControl>().scripthp <= 0)
            {
                GameObject p = _player[i];
                int index = _player.IndexOf(p);
                players.Add(p);
                _player.RemoveAt(index);
            }
        }
        if (_player.Count == 1)
        {
            players.Add(_player[0]);
            _player.RemoveAt(0);
            players.Reverse();
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerControl>().PlayerScore = KID.ScoreSystem.scores[i];
                print(players[i].name + players[i].GetComponent<PlayerControl>().PlayerScore);
            }
            ScoreBoard.isEnd = true;
        }
        if (ScoreBoard.isEnd)
        {
            for (int i = 0; i < player.Count; i++)
            {
                KID.ScoreSystem.PlayerScore[i] += player[i].GetComponent<PlayerControl>().PlayerScore;
            }
            ScoreBoard.ShowResult = true;
            ScoreBoard.isEnd = false;
        }
    }
}
