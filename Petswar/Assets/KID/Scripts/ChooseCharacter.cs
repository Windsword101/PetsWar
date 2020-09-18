using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KID
{
    public class ChooseCharacter : MonoBehaviour
    {
        [Header("玩家 1 - 4 的資料")]
        /// <summary>
        /// 玩家 1 - 4 的資料
        /// </summary>
        public PlayerData player1, player2, player3, player4;
        [Header("選取介面：圖示 1 - 4")]
        /// <summary>
        /// 選取介面：圖示 1 - 4
        /// </summary>
        public RectTransform[] chooseBox = new RectTransform[4];
        [Header("音效：選取與不能選取")]
        public AudioClip soundChoose;
        public AudioClip soundCantChoose;

        private AudioSource aud;

        /// <summary>
        /// 玩家資料：儲存在陣列方便用迴圈執行
        /// </summary>
        private PlayerData[] players = new PlayerData[4];
        /// <summary>
        /// 選取座標：圖示要移動的四個位置
        /// </summary>
        private Vector3[] chooseBoxPosition = { new Vector3(-450, 180), new Vector3(-150, 180), new Vector3(150, 180), new Vector3(450, 180) };
        /// <summary>
        /// 每個玩家是否選取角色
        /// </summary>
        private bool[] chooseCharacter = { false, false, false, false };
        /// <summary>
        /// 每個角色是否被選取
        /// </summary>
        private bool[] characterWasChoose = { false, false, false, false };

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            // 預設每位玩家選取的角色
            player1.character = Character.狗;
            player2.character = Character.兔子;
            player3.character = Character.烏龜;
            player4.character = Character.貓;

            // 將玩家資料儲存在陣列內
            players[0] = player1;
            players[1] = player2;
            players[2] = player3;
            players[3] = player4;
        }

        private void Update()
        {
            Choose();
        }

        private void Choose()
        {
            if (!Input.anyKey) return;                                                                  // 如果 沒有按任意鍵 跳出 - 效能

            for (int i = 0; i < players.Length; i++)                                                    // 迴圈執行四位玩家
            {
                int index = (int)players[i].character;                                                  // 儲存當前玩家所有角色編號

                if (Input.GetKeyDown(players[i].right) && !chooseCharacter[i])                          // 如果玩家按右 並且 還沒選角色
                {
                    index++;                                                                            // 編號遞增
                    if (index == players.Length) index = 0;                                             // 如果 編號 = 長度 重頭開始 - 避免超出範圍
                    players[i].character = (Character)(index);                                          // 修改玩家選取角色

                    StopAllCoroutines();                                                                // 先停止所有協程
                    StartCoroutine(Move(chooseBox[i], chooseBoxPosition[index], i));                    // 啟動協程
                }
                if (Input.GetKeyDown(players[i].left) && !chooseCharacter[i])                           // 如果玩家按左 並且 還沒選角色
                {
                    index--;                                                                            // 編號遞減
                    if (index == -1) index = players.Length - 1;                                        // 如果 編號 = -1 重尾開始 - 避免超出範圍
                    players[i].character = (Character)(index);                                          // 修改玩家選取角色

                    StopAllCoroutines();                                                                // 先停止所有協程
                    StartCoroutine(Move(chooseBox[i], chooseBoxPosition[index], i));                    // 啟動協程
                }
                if (Input.GetKeyDown(players[i].a) && !chooseCharacter[i])                              // 如果 玩家按下 A 並且 還沒選角色
                {
                    if (characterWasChoose[index])                                                      // 如果 此角色已被選取
                    {
                        aud.PlayOneShot(soundCantChoose);                                               // 播放 不能選取音效
                        return;                                                                         // 跳出
                    }

                    aud.PlayOneShot(soundChoose);                                                       // 播放選取音效
                    chooseCharacter[i] = true;                                                          // 玩家已經選取角色
                    characterWasChoose[index] = true;                                                   // 該角色已經被選取
                    players[i].character = (Character)(index);                                          // 修改玩家選取角色
                    Image img = chooseBox[i].GetChild(0).GetComponent<Image>();                         // 取得圖示
                    img.color = new Color(img.color.r, img.color.g, img.color.b, 0.3f);                 // 改變圖示顏色
                }
            }
        }

        /// <summary>
        /// 移動圖示協程
        /// </summary>
        /// <param name="target">目標圖示</param>
        /// <param name="pos">目標位置</param>
        /// <param name="playerIndex">玩家編號</param>
        /// <returns></returns>
        private IEnumerator Move(RectTransform target, Vector3 pos, int playerIndex)
        {
            pos.x += -50 + 25 * playerIndex;                                                                        // 圖示分開

            while (target.anchoredPosition.x != pos.x)                                                              // 迴圈執行 移動至指定位置
            {
                target.anchoredPosition = Vector3.Slerp(target.anchoredPosition, pos, Time.deltaTime * 80);
                yield return null;
            }
        }
    }
}
