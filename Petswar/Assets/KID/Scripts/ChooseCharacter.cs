using System.Xml.Schema;
using UnityEngine;

namespace KID
{
    public class ChooseCharacter : MonoBehaviour
    {
        /// <summary>
        /// 玩家 1 - 4 的資料
        /// </summary>
        public PlayerData player1, player2, player3, player4;

        public RectTransform[] chooseBox = new RectTransform[4];

        private PlayerData[] players = new PlayerData[4];

        private Vector3[] chooseBoxPosition = { new Vector3(-450, 0), new Vector3(-150, 0), new Vector3(150, 0), new Vector3(450, 0) };

        private void Awake()
        {
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
            for (int i = 0; i < players.Length; i++)
            {
                int index = (int)players[i].character;

                if (Input.GetKeyDown(players[i].right))
                {
                    index++;
                    if (index == 4) index = 0;
                    players[i].character = (Character)(index);
                }
                if (Input.GetKeyDown(players[i].left))
                {
                    index--;
                    if (index == -1) index = 3;
                    players[i].character = (Character)(index);
                }

                chooseBox[i].anchoredPosition = chooseBoxPosition[index];
            }
        }
    }
}
