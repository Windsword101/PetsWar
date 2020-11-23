using UnityEngine;
using System.Collections.Generic;

namespace KID
{
    public class RandomScene : MonoBehaviour
    {
        /// <summary>
        /// 所有場景名稱
        /// </summary>
        private static List<string> allSceneName { get; set; }

        /// <summary>
        /// 建議在選單執行一次此方法：設定所有場景
        /// </summary>
        /// <param name="scenes">請輸入要隨機載入的場景名稱，必須與 Build Settings 內的相同，大小寫也要相同</param>
        public static void SetAllScene(params string[] scenes)
        {
            allSceneName = new List<string>();                                              // 實例化清單物件

            for (int i = 0; i < scenes.Length; i++) allSceneName.Add(scenes[i]);            // 添加輸入的場景名稱到清單內
        }

        /// <summary>
        /// 取得隨機場景名稱
        /// </summary>
        /// <returns>隨機場景名稱</returns>
        public static string GetRandomScene()
        {
            if (allSceneName.Count == 0) return "";

            int r = Random.Range(0, allSceneName.Count);            // 取得隨機場景編號
            string scene = allSceneName[r];                         // 取得隨機場景名稱
            allSceneName.RemoveAt(r);                               // 刪除隨機場景

            return scene;                                           // 傳回隨機場景名稱
        }
    }
}
