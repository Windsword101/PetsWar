using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DanceManager : MonoBehaviour
{
    [Header("節點")]
    public Transform node;
    [Header("節點圖片：箭頭，A，B，C")]
    public Sprite[] images = new Sprite[4];
    [Header("節點數量")]
    public Text textCount;
    [Header("節點上限")]
    public int countLimit = 50;
    [Header("節點按鈕")]
    public Button[] btnsNode = new Button[7];
    [Header("開始遊戲按鈕")]
    public GameObject btnStart;
    [Header("開始遊戲畫面")]
    public GameObject panelGame;
    [Header("開始遊戲玩家節點父物件")]
    public Transform[] rootsPlayer;
    [Header("喇叭")]
    public AudioSource aud;
    [Header("節奏背景音樂")]
    public AudioClip[] soundBGMs;
    [Header("產生節點的間隔"), Range(0, 10)]
    public float interval = 0.9f;
    [Header("節點的速度"), Range(0, 1000)]
    public float speed = 280;
    [Header("節點位移：700 左右")]
    public float x = 700;

    [HideInInspector]
    /// <summary>
    /// 玩家選取的節點清單
    /// </summary>
    public List<DanceNote> nodes = new List<DanceNote>();

    public List<DanceNote>[] nodesPlays = { new List<DanceNote>(), new List<DanceNote>(), new List<DanceNote>() };

    public List<Transform>[] nodesPlayObjects = { new List<Transform>(), new List<Transform>(), new List<Transform>() };

    /// <summary>
    /// 節點根物件：選取後的節點區塊
    /// </summary>
    private Transform rootNote;

    private void Awake()
    {
        rootNote = GameObject.Find("選取後的節點區塊").transform;

        for (int i = 0; i < btnsNode.Length; i++)
        {
            int x = i;
            btnsNode[i].onClick.AddListener(() => AddNote(x, rootNote, nodes, true));
        }
    }

    private void Update()
    {
        RandomNote();
    }

    /// <summary>
    /// 增加節點
    /// </summary>
    /// <param name="danceNoteIndex">節點編號：上 ，下 1，左 2，右 3，A 4，B 5，C 6</param>
    /// <param name="parent">父物件</param>
    /// <param name="listNote">要儲存的清單</param>
    /// <param name="updateTip">是否需要更新提示文字</param>
    /// <param name="listObj">儲存實體物件</param>
    public void AddNote(int danceNoteIndex, Transform parent, List<DanceNote> listNote, bool updateTip, List<Transform> listObj = null)
    {
        if (listNote.Count == countLimit) return;                                                   // 如果 節點總數 等於 上限

        DanceNote danceNote = (DanceNote)danceNoteIndex;                                            // 數字轉為節點列舉
        listNote.Add(danceNote);                                                                    // 節點增加到清單內

        if (updateTip) textCount.text = "節點數量：" + listNote.Count + " / " + countLimit;          // 更新文字

        Transform temp = Instantiate(node, parent);                                                 // 生成節點

        if (listObj != null) listObj.Add(temp);

        Image image = temp.GetChild(0).GetComponent<Image>();                                       // 取得節點圖片

        // 如果 上 下 左 右 0 1 2 3 將編號設定為 0 - 箭頭圖片編號
        int index = 0;

        if (danceNoteIndex == 0 || danceNoteIndex == 1 || danceNoteIndex == 2 || danceNoteIndex == 3) index = 0;
        else index = danceNoteIndex - 3;

        image.sprite = images[index];

        // 上下左右 轉 角度
        switch (danceNote)
        {
            case DanceNote.上:
                image.rectTransform.localEulerAngles = Vector3.forward * 0;
                break;
            case DanceNote.下:
                image.rectTransform.localEulerAngles = Vector3.forward * 180;
                break;
            case DanceNote.左:
                image.rectTransform.localEulerAngles = Vector3.forward * 90;
                break;
            case DanceNote.右:
                image.rectTransform.localEulerAngles = Vector3.forward * 270;
                break;
        }

        if (listNote.Count == countLimit) btnStart.SetActive(true);     // 顯示 開始遊戲 按鈕
    }

    /// <summary>
    /// 開始遊戲
    /// </summary>
    public void StartGame()
    {
        panelGame.SetActive(true);
        aud.clip = soundBGMs[1];
        aud.Play();
        StartCoroutine(CreateNote());
    }

    private IEnumerator CreateNote()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = 0; j < rootsPlayer.Length; j++)
            {
                AddNote((int)nodes[i], rootsPlayer[j], nodesPlays[j], false, nodesPlayObjects[j]);
                RectTransform temp = nodesPlayObjects[j][i].GetComponent<RectTransform>();
                temp.localPosition = Vector3.zero;
                temp.anchoredPosition += Vector2.right * x;
                temp.gameObject.AddComponent<DanceNode>().speed = speed;
            }

            yield return new WaitForSecondsRealtime(interval);
        }
    }

    private void RandomNote()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartCoroutine(RandomNoteInterval());
    }

    private IEnumerator RandomNoteInterval()
    {
        for (int i = 0; i < 50; i++)
        {
            int r = Random.Range(0, 7);
            AddNote(r, rootNote, nodes, true);
            yield return null;
        }
    }
}
