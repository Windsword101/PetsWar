using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace KID
{
    public class ChooseCharacterBox : MonoBehaviour
    {
        [Header("玩家資料：玩家 1 - 玩家 4")]
        /// <summary>
        /// 玩家資料：玩家 1 - 玩家 4
        /// </summary>
        public PlayerData[] players;
        [Header("選取角色盒子")]
        public Image[] imgBoxes;
        [Header("角色")]
        public Transform[] characters;
        [Header("每個角色選取盒子的座標")]
        /// <summary>
        /// 選取角色方塊的四個座標
        /// </summary>
        public List<float> posCharacters = new List<float> { -450, -150, 150, 450 };

        public List<int> indexCharacters = new List<int> { 0, 1, 2, 3 };
        [Header("角色：確認後的角色")]
        public RectTransform[] rectCharactersChoose;

        /// <summary>
        /// 玩家是否選
        /// </summary>
        public bool[] playersChoose = { false, false, false, false };

        /// <summary>
        /// 角色是否被選取
        /// </summary>
        public bool[] charactersChoose = { false, false, false, false };

        [Header("確認音效")]
        public AudioClip soundChoose;

        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        private void Update()
        {
            MoveBox();
        }

        /// <summary>
        /// 移動選取角色盒子
        /// </summary>
        private void MoveBox()
        {
            if (Input.anyKeyDown)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if (!playersChoose[i] && Input.GetKeyDown(players[i].right))
                    {
                        int index = indexCharacters[i];
                        index++;

                        if (index >= posCharacters.Count) index = 0;

                        indexCharacters[i] = index;

                        CheckCharacter(i, 1);

                        MoveImgBox(i, indexCharacters[i]);
                    }

                    if (!playersChoose[i] && Input.GetKeyDown(players[i].left))
                    {
                        int index = indexCharacters[i];
                        index--;

                        if (index <= -1) index = posCharacters.Count - 1;

                        indexCharacters[i] = index;

                        CheckCharacter(i, -1);

                        MoveImgBox(i, indexCharacters[i]);
                    }

                    if (!playersChoose[i] && Input.GetKeyDown(players[i].a))
                    {
                        StartCoroutine(ChooseCharacterEffect(i));
                        aud.PlayOneShot(soundChoose);
                    }
                }
            }
        }

        /// <summary>
        /// 移動圖片盒子：玩家選取區塊
        /// </summary>
        /// <param name="indexPlayer">玩家編號</param>
        /// <param name="index">玩家選取的編號</param>
        private void MoveImgBox(int indexPlayer, int index)
        {
            imgBoxes[indexPlayer].rectTransform.anchoredPosition = new Vector2(posCharacters[index], imgBoxes[indexPlayer].rectTransform.anchoredPosition.y);
            imgBoxes[indexPlayer].transform.SetParent(characters[index]);
            imgBoxes[indexPlayer].transform.SetAsFirstSibling();
        }

        /// <summary>
        /// 檢查下一隻角色是否被選取
        /// </summary>
        /// <param name="indexPlayer">玩家編號</param>
        /// <param name="checkDirection">檢查方向：1 右邊，-1 左邊</param>
        private void CheckCharacter(int indexPlayer, int checkDirection)
        {
            for (int i = 0; i < charactersChoose.Length; i++)
            {
                if (i == indexPlayer) continue;

                if (charactersChoose[indexCharacters[indexPlayer]])
                {
                    indexCharacters[indexPlayer] += checkDirection;
                    if (indexCharacters[indexPlayer] == indexCharacters.Count) indexCharacters[indexPlayer] = 0;
                    if (indexCharacters[indexPlayer] == -1) indexCharacters[indexPlayer] = indexCharacters.Count - 1;
                }
            }
        }

        /// <summary>
        /// 選取角色的特效：2D 圖片滑動下來
        /// </summary>
        /// <param name="index">玩家編號</param>
        private IEnumerator ChooseCharacterEffect(int index)
        {
            /* 將其他選取相同角色玩家往右移 */
            for (int i = 0; i < indexCharacters.Count; i++)
            {
                if (i == index) continue;

                if (indexCharacters[i] == indexCharacters[index])
                {
                    indexCharacters[i] += 1;
                    if (indexCharacters[i] == indexCharacters.Count) indexCharacters[i] = 0;

                    /* 往右判斷是否該角色被選取，如果有就繼續檢查下一隻角色 */
                    CheckCharacter(i, 1);

                    MoveImgBox(i, indexCharacters[i]);
                }
            }

            playersChoose[index] = true;
            charactersChoose[indexCharacters[index]] = true;
            int chooseIndex = indexCharacters[index];

            /* 儲存玩家所選的角色 */
            players[index].character = (Character)indexCharacters[index];

            while (rectCharactersChoose[chooseIndex].anchoredPosition.y > 0)
            {
                rectCharactersChoose[chooseIndex].anchoredPosition -= Vector2.up * 1000 * Time.deltaTime;
                yield return null;
            }

            rectCharactersChoose[chooseIndex].anchoredPosition = Vector2.zero;

            /* 玩家編號調整順序與位置 */
            Transform playerIndex = imgBoxes[index].transform.GetChild(0);
            playerIndex.SetParent(imgBoxes[index].transform.parent);
            playerIndex.GetComponent<RectTransform>().anchoredPosition = new Vector2(playerIndex.GetComponent<RectTransform>().anchoredPosition.x, 270);
        }
    }
}

