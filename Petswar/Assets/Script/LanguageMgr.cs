using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanguageMgr : MonoBehaviour
{
    private static LanguageMgr instance = null;
    public static LanguageMgr Instance
    {
        get { return instance; }
    }
    /// <summary>
    /// 語言
    /// </summary>
    [SerializeField]
    private SystemLanguage language;
    /// <summary>
    /// 相同的key 對應 不同國家的value
    /// </summary>
    private Dictionary<string, string> dict = new Dictionary<string, string>();
    /// <summary>
    /// 加載預翻譯的語言
    /// </summary>
    private void loadLanguage()
    {
        //加載文件
        TextAsset ta = Resources.Load<TextAsset>(language.ToString());
        if (ta == null)
        {
            Debug.LogWarning("沒有這個語言的翻譯文件");
            return;
        }
        //獲取每一行
        string[] lines = ta.text.Split('\n');
        //獲取key value
        for (int i = 0; i < lines.Length; i++)
        {
            //檢測
            if (string.IsNullOrEmpty(lines[i]))
                continue;
            //獲取 key:kv[0] value kv[1]
            string[] kv = lines[i].Split(':');
            //保存到字典
            dict.Add(kv[0], kv[1]);
            Debug.Log(string.Format("key:{0}, value:{1}", kv[0], kv[1]));
        }
    }
    void Awake()
    {
        instance = this;
        loadLanguage();
    }
    /// <summary>
    /// 獲取對應的value
    /// </summary>
    /// <param name="key">鍵</param>
    /// <returns>返回對應的value 如果不存在這個key 就返回空串</returns>
    public string GetText(string key)
    {
        if (dict.ContainsKey(key))
            return dict[key];
        else//沒有這個key
        {
            return string.Empty;
        }
    }
}

