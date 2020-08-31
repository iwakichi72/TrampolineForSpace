using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerObj;
    [SerializeField] private GameObject m_GoalObj;
    [SerializeField] private GameObject m_ItemObj;
    public float m_TimeLimit = 100f;
    public int m_RankViewCount = 3;
    public int m_ItemSpawnNum = 12;// アイテム生成個数
    public float m_ItemYMinPosition = 10;// アイテム生成ポジション最低
    public float m_ItemYMaxPosition = 1000;// アイテム生成ポジション最大
    public Text m_HeightText;
    public Text m_TimeCountText;
    public Text m_MessageText;
    public Text m_UserNameText;
    public Transform m_PlayerSpawnPoint;
    public Transform m_GoalSpawnPoint;
    public AudioSource m_BackMusic;
    public AudioClip m_TimeUpAudio;
    public AudioClip m_GameOverAudio;
    public CameraController m_CameraCtrl;

    private PlayerController m_PlayerCtrl = new PlayerController();
    private GoalController m_GoalCtrl = new GoalController();
    private ItemController m_ItemCtrl = new ItemController();
    private float m_Seconds;
    private float m_RemainingTime;
    private int m_MaxHeightRecord;
    private int m_GameScore;
    private bool m_PlayingFlag;
    private WaitForSeconds m_2SecWait;
    private GameObject m_TitleButton;
    private GameObject m_UserNameInputField;
    private GameObject m_EnterButton;
    private bool m_UserNameInputFlag;
    private List<UserScore> m_RankDataList;

    // トランポリンの色を判別するenum定数
    private enum TrampolineColor
    {
        Red,
        Blue,
        Yellow
    }

    private void Awake()
    {
        // プレイヤーオブジェクトの生成
        m_PlayerCtrl.m_Instance = Instantiate(m_PlayerObj, m_PlayerSpawnPoint.position, m_PlayerSpawnPoint.rotation) as GameObject;
        m_PlayerCtrl.SetUp();
        // ゴールオブジェクトの生成
        m_GoalCtrl.m_Instance = Instantiate(m_GoalObj, m_GoalSpawnPoint.position, m_GoalSpawnPoint.rotation) as GameObject;
        m_GoalCtrl.SetUp();

        // 指定されたアイテム個数分回す
        for (int i = 0; i < m_ItemSpawnNum; i++)
        {
            Vector3 spawnPosition = new Vector3();
            // アイテム生成するトランポリンの抽選
            int tcRandomNum = UnityEngine.Random.Range((int)TrampolineColor.Red, (int)TrampolineColor.Yellow + 1);// 切り捨て対策でYellowは+1
            // アイテム生成する座標を取得する
            spawnPosition = GetRandomPosition(tcRandomNum);

            // アイテムオブジェクトの生成
            m_ItemCtrl.m_Instance = Instantiate(m_ItemObj, spawnPosition, Quaternion.identity) as GameObject;
            m_ItemCtrl.SetUp();
        }

        // カメラのターゲットを、プレイヤーに設定
        m_CameraCtrl.m_Target = m_PlayerCtrl.m_Instance.transform;
        m_CameraCtrl.SetUp();
    }

    void Start()
    {
        m_2SecWait = new WaitForSeconds(2f);
        m_PlayingFlag = false;

        m_TitleButton = GameObject.Find("TitleButton");
        m_TitleButton.SetActive(false);
        m_UserNameInputField = GameObject.Find("UserNameInputField");
        m_UserNameInputField.SetActive(false);
        m_EnterButton = GameObject.Find("EnterButton");
        m_EnterButton.SetActive(false);

        // Main処理をここから始めよう
        StartCoroutine(GameLoop());
    }

    void Update()
    {
        if (m_PlayingFlag)
        {
            //時間
            m_Seconds += Time.deltaTime;

            m_RemainingTime = m_TimeLimit - m_Seconds;
            m_TimeCountText.text = m_RemainingTime.ToString("000");

            //高さ
            float currentHeight = (m_PlayerCtrl.m_Instance.transform.position.y - 0.05f) * 10f; // 地面にいる時微妙に高さが出るので微調整
            m_HeightText.text = currentHeight.ToString("0");

            //最高到達点を更新
            if(m_MaxHeightRecord < currentHeight)
            {
                m_MaxHeightRecord = (int)currentHeight;
            }
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStart());

        yield return StartCoroutine(GamePlaying());

        yield return StartCoroutine(GameEnd());
    }

    private IEnumerator GameStart()
    {
        DisablePlayerControl();

        m_MessageText.text = "Start!";

        yield return m_2SecWait;
    }
    private IEnumerator GamePlaying()
    {
        EnablePlayerControl();

        m_MessageText.text = string.Empty;

        m_PlayingFlag = true;

        // ゴールかタイムアップかゲームオーバーになるまでループ
        while (!(m_RemainingTime < 0)&& !m_GoalCtrl.CheckClear() && !m_PlayerCtrl.CheckGameOver())
        {
            yield return null;
        }

    }

    private IEnumerator GameEnd()
    {
        //ゲーム終了時は入力、パラメータ更新を止める
        DisablePlayerControl();
        m_PlayingFlag = false;

        if (m_RemainingTime < 0 || m_GoalCtrl.CheckClear())
        {
            m_BackMusic.clip = m_TimeUpAudio;
            m_BackMusic.loop = false;
            m_BackMusic.Play();
            if (m_GoalCtrl.CheckClear())
                m_MessageText.text = "Goal!";
            else
                m_MessageText.text = "TimeUp!";

            yield return m_2SecWait;

            // スコア計算を行う
            yield return StartCoroutine(CalculateGameScore());

            //ランキングデータを取得し、m_RankViewCount以内だったら名前を聞く
            bool rankInFlg = false;
            int viewRankCount = 0;

            yield return StartCoroutine(GetRanking(GetRankingSuccess, GetRankingFailed));

            // ランキングデータ数が足りなかった場合の対策
            if (m_RankDataList.Count < m_RankViewCount)
            {
                viewRankCount = m_RankDataList.Count;
                if (m_RankDataList.Count <= 0)
                    rankInFlg = true;
            }
            else
                viewRankCount = m_RankViewCount;

            for (int i = 0; i < viewRankCount; i++)
            {
                UserScore rec = m_RankDataList[i];
                if (m_RankDataList.Count < m_RankViewCount)
                    rankInFlg = true;
                else if (rec.gameScore < m_GameScore)
                    rankInFlg = true;
            }

            if (rankInFlg && !m_TitleButton.activeSelf)// タイトルボタンが出てるということはGetRanking出来なかったということなので、この処理には入らない
            {
                m_MessageText.text = m_MessageText.text + "\n It's NewRecord!!!";

                yield return StartCoroutine(WateUserNameInput());

                yield return StartCoroutine(SetRanking(m_UserNameText.text, m_MaxHeightRecord, m_GameScore, SetRankingSuccess, SetRankingFailed));
            }
        }
        else if(m_PlayerCtrl.CheckGameOver())
        {
            m_MessageText.text = "GameOver...";

            m_BackMusic.clip = m_GameOverAudio;
            m_BackMusic.Play();
        }

        m_TitleButton.SetActive(true);

        yield return null;
    }

    // ゲームスコアの計算を行う
    private IEnumerator CalculateGameScore()
    {
        // m_MaxHeightRecord表示
        m_MessageText.text = "YourMaxJump\n" + m_MaxHeightRecord.ToString() + " M";
        yield return m_2SecWait;

        // 取得したアイテム個数表示
        m_MessageText.text = "ItemPickUpNum\n" + m_PlayerCtrl.CheckTookItemNum() + " item\n" + "item × 100 pt!";
        yield return m_2SecWait;

        // m_GameScore表示
        m_GameScore = m_MaxHeightRecord + m_PlayerCtrl.CheckTookItemNum() * 100;
        m_MessageText.text = "YourScore\n" + m_GameScore.ToString() + " pt";
        yield return m_2SecWait;

        // ゴールしていればボーナスポイント追加
        if (m_GoalCtrl.CheckClear())
        {
            m_MessageText.text = "and GoalBonus!!! +5000pt\n";
            m_GameScore = m_GameScore + 5000;
            m_MessageText.text = m_MessageText.text + m_GameScore.ToString() + " pt";
            yield return m_2SecWait;
        }
    }

    // 名前項目を表示させる
    private IEnumerator WateUserNameInput()
    {
        m_UserNameInputFlag = false;

        m_UserNameInputField.SetActive(true);
        m_EnterButton.SetActive(true);

        while (!m_UserNameInputFlag)
        {
            yield return null;
        }

        m_UserNameInputField.SetActive(false);
        m_EnterButton.SetActive(false);
    }

    private void EnablePlayerControl()
    {
        m_PlayerCtrl.EnableControl();
    }


    private void DisablePlayerControl()
    {
        m_PlayerCtrl.DisableControl();
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void OnClickEnterButton()
    {
        m_UserNameInputFlag = true;
        m_MessageText.text = "Now Loading...";
    }

    /// <summary>
    /// ランキングデータを登録する
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="maxHeight"></param>
    /// <param name="score"></param>
    /// <param name="cbkSuccess"></param>
    /// <param name="cbkFailed"></param>
    /// <returns></returns>
    private IEnumerator SetRanking(string userName, int maxHeight, int score, Action cbkSuccess = null, Action cbkFailed = null)
    {
        // form にフィールドを追加する

        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("heightRecord", maxHeight);
        form.AddField("gameScore", score);

        // WebRequest.POST通信（※androidで動かしたいならばlocalhostではなく、IPアドレスを入れること）
        UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.11.13/tfs/UserScore/setRanking", form);

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
                cbkSuccess();
            }
        }
    }

    /// <summary>
    /// Callbacks the www success.
    /// </summary>
    /// <param name="response">Response.</param>
    private void SetRankingSuccess()
    {
        m_MessageText.text = "Your name is add in the Ranking!";
    }

    /// <summary>
    /// Callbacks the www failed.
    /// </summary>
    private void SetRankingFailed()
    {
        // jsonデータ取得に失敗した
        m_MessageText.text = "WebRequest Failed";
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
        UnityWebRequest webRequest = UnityWebRequest.Get("http://192.168.11.13/tfs/UserScore/getRanking");

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
        m_TitleButton.SetActive(true);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition(int trampolineColerNum)
    {
        float x = 0; //x軸は引数によって変える
        float y = 0; //y軸はランダム
        float z = 0; //z軸は0固定

        //x軸だけランダムに生成する
        switch (trampolineColerNum)
        {
            case (int)TrampolineColor.Red:
                x = 0;
                break;
            case (int)TrampolineColor.Blue:
                x = 2.5f;
                break;
            case (int)TrampolineColor.Yellow:
                x = -2.5f;
                break;
        }

        // y軸（高さ）はランダム
        y = UnityEngine.Random.Range(m_ItemYMinPosition, m_ItemYMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, y, z);
    }
}
