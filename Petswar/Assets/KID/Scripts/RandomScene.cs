using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace KID
{
    public class RandomScene : MonoBehaviour
    {
        private static List<string> _allScene = new List<string>();

        /// <summary>
        /// 所有場景：除了編號為 0 的第一個場景 - 排除選單
        /// ※ 必須檢查 Build Settings 內是否還有多餘的場景
        /// </summary>
        public static List<string> allScene
        {
            get
            {
                for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
                    _allScene.Add(SceneManager.GetSceneAt(i + 1).name);

                return _allScene;
            }
        }

        /// <summary>
        /// 載入隨機場景：不重複
        /// </summary>
        /// <returns>隨機場景名稱</returns>
        public static string LoadRandomScene()
        {
            int r = Random.Range(0, allScene.Count);    // 取得隨機整數 0 至所有場景數量

            string scene = allScene[r];                 // 取得隨機場景名稱
            allScene.RemoveAt(r);                       // 刪除取得的場景名稱

            return scene;                               // 傳回隨機場景名稱
        }
    }
}
