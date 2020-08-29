using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON; // Json

public class UserScoreModel : MonoBehaviour
{
    /// <summary>
    /// UserScore型のjsonデータをListで返す
    /// </summary>
    /// <param name="json"></param>
    /// <returns>加工後のjsonデータ</returns>
    public static List<UserScore> DeserializeFromUserScore(string json)
    {
        List<UserScore> retData = new List<UserScore>();

        IList jsonList = (IList)Json.Deserialize(json);

        // リストの内容はオブジェクトなので、辞書型の変数に一つ一つ代入しながら、処理
        foreach (IDictionary jsonOne in jsonList)
        {
            //新レコード解析開始

            var tmp = new UserScore();

            //該当するキー名が jsonOne に存在するか調べ、存在したら取得して変数に格納する。
            if (jsonOne.Contains("userName"))
            {
                tmp.userName = (string)jsonOne["userName"];
            }

            if (jsonOne.Contains("heightRecord"))
            {
                tmp.heightRecord = (int)(long)jsonOne["heightRecord"];
            }
            if (jsonOne.Contains("gameScore"))
            {
                tmp.gameScore = (int)(long)jsonOne["gameScore"];
            }

            //現レコード解析終了
            retData.Add(tmp);
        }

        return retData;
    }
}
