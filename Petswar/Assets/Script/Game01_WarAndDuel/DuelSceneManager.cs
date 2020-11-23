﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DuelSceneManager : MonoBehaviour
{
    public List<GameObject> player = new List<GameObject>();
    public GameManager gamemanager;
    public GameObject GM;
    public GameObject duelwinner;
    public GameObject rules, timesup;
    public Text countdown;
    public Text winnertext;
    public GameObject dog, cat, ribb, turtle, player1, player2;
    public static float timer = 5f;
    public static bool pause, restart = false;
    //用於排列名次
    public List<GameObject> _player = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();

    private void Awake()
    {
        rules = GameObject.Find("規則說明");
        GM = GameObject.Find("GM");
        gamemanager = GameObject.Find("GM").GetComponent<GameManager>();
        if (gamemanager.dog.scripthp > 0 && gamemanager.cat.scripthp > 0)
        {
            dog.SetActive(true);
            cat.SetActive(true);
            dog.GetComponent<PlayerControlDuel>().hit = cat;
            cat.GetComponent<PlayerControlDuel>().hit = dog;
            ribb.GetComponent<PlayerControlDuel>().dead = true;
            turtle.GetComponent<PlayerControlDuel>().dead = true;
            dog.transform.position = player1.transform.position;
            dog.transform.rotation = player1.transform.rotation;
            cat.transform.position = player2.transform.position;
            cat.transform.rotation = player2.transform.rotation;

        }
        if (gamemanager.dog.scripthp > 0 && gamemanager.ribb.scripthp > 0)
        {
            dog.SetActive(true);
            ribb.SetActive(true);
            dog.GetComponent<PlayerControlDuel>().hit = ribb;
            ribb.GetComponent<PlayerControlDuel>().hit = dog;
            cat.GetComponent<PlayerControlDuel>().dead = true;
            turtle.GetComponent<PlayerControlDuel>().dead = true;
            dog.transform.position = player1.transform.position;
            dog.transform.rotation = player1.transform.rotation;
            ribb.transform.position = player2.transform.position;
            ribb.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.dog.scripthp > 0 && gamemanager.turtle.scripthp > 0)
        {
            dog.SetActive(true);
            turtle.SetActive(true);
            dog.GetComponent<PlayerControlDuel>().hit = turtle;
            turtle.GetComponent<PlayerControlDuel>().hit = dog;
            ribb.GetComponent<PlayerControlDuel>().dead = true;
            cat.GetComponent<PlayerControlDuel>().dead = true;
            dog.transform.position = player1.transform.position;
            dog.transform.rotation = player1.transform.rotation;
            turtle.transform.position = player2.transform.position;
            turtle.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.cat.scripthp > 0 && gamemanager.ribb.scripthp > 0)
        {
            ribb.SetActive(true);
            cat.SetActive(true);
            cat.GetComponent<PlayerControlDuel>().hit = ribb;
            ribb.GetComponent<PlayerControlDuel>().hit = cat;
            dog.GetComponent<PlayerControlDuel>().dead = true;
            turtle.GetComponent<PlayerControlDuel>().dead = true;
            ribb.transform.position = player1.transform.position;
            ribb.transform.rotation = player1.transform.rotation;
            cat.transform.position = player2.transform.position;
            cat.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.cat.scripthp > 0 && gamemanager.turtle.scripthp > 0)
        {
            cat.SetActive(true);
            turtle.SetActive(true);
            turtle.GetComponent<PlayerControlDuel>().hit = cat;
            cat.GetComponent<PlayerControlDuel>().hit = turtle;
            ribb.GetComponent<PlayerControlDuel>().dead = true;
            dog.GetComponent<PlayerControlDuel>().dead = true;
            cat.transform.position = player1.transform.position;
            cat.transform.rotation = player1.transform.rotation;
            turtle.transform.position = player2.transform.position;
            turtle.transform.rotation = player2.transform.rotation;
        }
        if (gamemanager.ribb.scripthp > 0 && gamemanager.turtle.scripthp > 0)
        {
            ribb.SetActive(true);
            turtle.SetActive(true);
            ribb.GetComponent<PlayerControlDuel>().hit = turtle;
            turtle.GetComponent<PlayerControlDuel>().hit = ribb;
            dog.GetComponent<PlayerControlDuel>().dead = true;
            cat.GetComponent<PlayerControlDuel>().dead = true;
            ribb.transform.position = player1.transform.position;
            ribb.transform.rotation = player1.transform.rotation;
            turtle.transform.position = player2.transform.position;
            turtle.transform.rotation = player2.transform.rotation;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject p in player)
        {
            if (p.GetComponent<PlayerControlDuel>().dead == false)
            {
                _player.Add(p);
            }
        }
        pause = true;
        StartCoroutine("Delay");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreCalculate();
        if (pause == false && timer > 0 && duelwinner.activeSelf == false) timer -= Time.deltaTime;
        countdown.text = timer.ToString("F2");
        if (timer <= 0) timesup.SetActive(true);
        if (dog.GetComponent<PlayerControlDuel>().dead == false && cat.GetComponent<PlayerControlDuel>().dead == true && ribb.GetComponent<PlayerControlDuel>().dead == true && turtle.GetComponent<PlayerControlDuel>().dead == true)
        {
            duelwinner.SetActive(true);
            winnertext.text = "狗獲勝！";
            Destroy(GM);
        }
        else if (dog.GetComponent<PlayerControlDuel>().dead == true && cat.GetComponent<PlayerControlDuel>().dead == false && ribb.GetComponent<PlayerControlDuel>().dead == true && turtle.GetComponent<PlayerControlDuel>().dead == true)
        {
            duelwinner.SetActive(true);
            winnertext.text = "貓獲勝！";
            Destroy(GM);
        }
        else if (dog.GetComponent<PlayerControlDuel>().dead == true && cat.GetComponent<PlayerControlDuel>().dead == true && ribb.GetComponent<PlayerControlDuel>().dead == false && turtle.GetComponent<PlayerControlDuel>().dead == true)
        {
            duelwinner.SetActive(true);
            winnertext.text = "兔子獲勝！";
            Destroy(GM);
        }
        else if (dog.GetComponent<PlayerControlDuel>().dead == true && cat.GetComponent<PlayerControlDuel>().dead == true && ribb.GetComponent<PlayerControlDuel>().dead == true && turtle.GetComponent<PlayerControlDuel>().dead == false)
        {
            duelwinner.SetActive(true);
            winnertext.text = "烏龜獲勝！";
            Destroy(GM);
        }
    }
    public void Menu()
    {
        Application.LoadLevel("MenuScene");
        Destroy(GM);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1;
        rules.SetActive(false);
        pause = false;
    }
    public void Restart()
    {
        timesup.SetActive(false);
        timer = 5f;
    }

    // 計算分數
    private void ScoreCalculate()
    {
        for (int i = 0; i < _player.Count; i++)
        {
            if (_player[i].GetComponent<Collider>().enabled == false)
            {
                GameObject p = _player[i];
                int index = _player.IndexOf(p);
                _player.RemoveAt(index);
                players.Add(p);
            }
        }
        if (_player.Count == 1)
        {
            players.Insert(0, _player[0]);
            _player.RemoveAt(0);
            players.Add(null);
            players.Add(null);
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] != null)
                {
                    players[i].GetComponent<PlayerControl>().PlayerScore = KID.ScoreSystem.scores[i];
                }
            }
            ScoreBoard.ShowResult = true;
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
