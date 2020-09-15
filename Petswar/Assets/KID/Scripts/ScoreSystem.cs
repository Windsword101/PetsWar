using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace KID
{
    /// <summary>
    /// 分數系統
    /// </summary>
    public class ScoreSystem : MonoBehaviour
    {
        /// <summary>
        /// 儲存有分數的關卡名稱
        /// </summary>
        public static List<string> scenesHaveScore = new List<string>();

        /// <summary>
        /// 儲存分數，需要填角色、關卡與分數
        /// </summary>
        /// <param name="character">角色</param>
        /// <param name="level">關卡</param>
        /// <param name="score">要儲存的分數</param>
        public static void StoreScore(Character character, Level level, int score)
        {
            string name = character.ToString() + level.ToString();                      // 儲存的名稱為：角色場景 - 例如：兔子第一關

            var same = scenesHaveScore.Where(x => x == level.ToString());               // 取得相同關卡是否已經有儲存分數

            if (same.ToList().Count < 1) scenesHaveScore.Add(level.ToString());         // 如果相同數量小於 1 將關卡儲存於有分數的關卡名稱內

            PlayerPrefs.SetInt(name, score);                                            // 儲存整數(角色場景，分數) - 例如：兔子第一關999
        }

        /// <summary>
        /// 透過角色與關卡取得分數
        /// </summary>
        /// <param name="character">要取得分數的角色</param>
        /// <param name="level">要取得分數的關卡</param>
        /// <returns>傳回該角色與關卡的分數</returns>
        public static int GetScoreByCharacterAndLevel(Character character, Level level)
        {
            string name = character.ToString() + level.ToString();                      // 儲存的名稱為：角色場景 - 例如：兔子第一關
            return PlayerPrefs.GetInt(name);
        }

        /// <summary>
        /// 透過角色取得總分
        /// </summary>
        /// <param name="character">角色</param>
        /// <returns>該角色總分</returns>
        public static int GetTotalScoreByCharacter(Character character)
        {
            string nameC = character.ToString();                    // 角色名稱

            int total = 0;                                          // 總分

            for (int i = 0; i < scenesHaveScore.Count; i++)         // 迴圈跑有分數的關卡
            {
                string name = nameC + scenesHaveScore[i];           // 取得儲存的名稱
                total += PlayerPrefs.GetInt(name);                  // 總分
            }

            return total;
        }
    }
}
