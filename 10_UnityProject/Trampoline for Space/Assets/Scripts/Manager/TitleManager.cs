using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class TitleManager : MonoBehaviour
{
    public GameObject m_GoalObject;
    public Text m_MessageText;
    public int m_RankViewCount = 3;
    public Text m_1stLabel;
    public Text m_2ndLabel;
    public Text m_3rdLabel;

    private List<UserScore> m_RankDataList;
    private GameObject m_BaseCanvas;
    private GameObject m_RankingCanvas;

    private void Start()
    {
        m_BaseCanvas = GameObject.Find("BaseCanvas");
        m_RankingCanvas = GameObject.Find("RankingCanvas");
        m_RankingCanvas.SetActive(false);
    }

    private void Update()
    {
        // x軸を軸にして0.1度、回転させるQuaternionを作成（変数をrotとする）
        Quaternion rot = Quaternion.Euler(0.1f, 0, 0);
        // 現在の自信の回転の情報を取得する。
        Quaternion q = m_GoalObject.transform.rotation;
        // 合成
        m_GoalObject.transform.rotation = q * rot;
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickRankingButton()
    {
        m_MessageText.text = "Now Loading...";
        StartCoroutine(ViewRanking());
    }


    private IEnumerator ViewRanking()
    {
        // ランキングデータを表示する
        yield return StartCoroutine(GetRanking(GetRankingSuccess, GetRankingFailed));

        SetRankingList();
        m_RankingCanvas.SetActive(true);
        m_BaseCanvas.SetActive(false);
    }

    public void OnClickRankingCloseButton()
    {
        m_MessageText.text = "";
        m_RankingCanvas.SetActive(false);
        m_BaseCanvas.SetActive(true);
    }

    /// <summary>
    /// ランキングデータを取得
    /// </summary>
    /// <returns>The json.</returns>
    /// <param name="cbkSuccess">Cbk success.</param>
    /// <param name="cbkFailed">Cbk failed.</param>
    private IEnumerator GetRanking(Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        // WebRequest.POST通信（※androidで動かしたいならばlocalhostではなく、IPアドレスを入れること）
        UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/tfs/UserScore/getRanking");

        //タイムアウトの指定
        webRequest.timeout = 5;

        yield return webRequest.SendWebRequest();
        if (webRequest.error != null)
        {
            //レスポンスエラーの場合
            Debug.LogError(webRequest.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (webRequest.isDone)
        {
            // リクエスト成功の場合
            Debug.Log($"Success:{webRequest.downloadHandler.text}");
            if (null != cbkSuccess)
            {
                cbkSuccess(webRequest.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// Callbacks the www success.
    /// </summary>
    /// <param name="response">Response.</param>
    private void GetRankingSuccess(string response)
    {
        //Json の内容を UserScoreTable型のリストとしてデコードする。
        m_RankDataList = UserScoreModel.DeserializeFromUserScore(response);

    }

    /// <summary>
    /// Callbacks the www failed.
    /// </summary>
    private void GetRankingFailed()
    {
        // jsonデータ取得に失敗した
        m_MessageText.text = "WebRequest Failed";
    }

    private void SetRankingList()
    {
        // デフォルトメッセージ
        int viewRankCount = 0;
        List<Text> rankingList = new List<Text>();
        rankingList.Add(m_1stLabel);
        rankingList.Add(m_2ndLabel);
        rankingList.Add(m_3rdLabel);

        // データ数が足りなかった場合の対策
        if (m_RankDataList.Count < m_RankViewCount)
            viewRankCount = m_RankDataList.Count;
        else
            viewRankCount = m_RankViewCount;

        // 一旦初期化
        for (int i = 0; i < rankingList.Count; i++)
        {
            rankingList[i].text = "No Data";
        }

        // すべてのタンクを通過し、それぞれのスコアをメッセージに追加
        for (int i = 0; i < viewRankCount; i++)
        {
            rankingList[i].text =  m_RankDataList[i].userName + " : " + m_RankDataList[i].gameScore + "pt";
        }

    }

}
