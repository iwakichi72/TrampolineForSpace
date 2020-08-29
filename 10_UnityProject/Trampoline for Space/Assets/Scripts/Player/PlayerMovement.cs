using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float m_MoveSpeed = 1f; //移動スピード
    public Slider m_JumpSlider;
    public float m_MinLaunchForce = 10f;
    public float m_MaxLaunchForce = 300f;
    public float m_MaxChargeTime = 2f;
    public ParticleSystem m_JumpParticle;
    public AudioSource m_PlayerAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_JumpClip;
    public AudioClip m_TrampolineClip;
    public AudioClip m_NormalGroundClip;

    private float m_CurrentJumpForce;
    private float m_ChargeSpeed;
    private bool m_Ground;
    private bool m_Jumped;
    private Rigidbody m_Rb;
    private Transform m_MoveTarget;

    private const string m_JumpStr = "Jump";
    private const string m_RunningStr = "Running";
    private const string m_RedStr = "Red";
    private const string m_BlueStr = "Blue";
    private const string m_YellowStr = "Yellow";

    private enum JumpTapStatus
    {
        Default,
        TapOff,
        TapOn,
        Tapping,
    };
    private JumpTapStatus m_JumpTapStatus; // ジャンプボタンのタップ状態を保持

    void Start()
    {
        //Rigidbodyを取得
        m_Rb = GetComponent<Rigidbody>();
        m_MoveTarget = GameObject.FindGameObjectWithTag("PlayerMoveTarget").transform;

        // ジャンプ初期値設定
        m_CurrentJumpForce = m_MinLaunchForce;
        m_JumpSlider.value = m_MinLaunchForce;
        // ジャンプ力がチャージアップする率は、最大チャージ時間による可能な力の範囲
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        m_JumpTapStatus = JumpTapStatus.Default;
    }

    void Update()
    {
        Vector3 velocity = Vector3.zero;

        //現在の位置＋入力した数値の場所に移動する
        transform.position = Vector3.SmoothDamp(transform.position, m_MoveTarget.position, ref velocity, m_MoveSpeed);

        // ジャンプ処理
        JumpController();
    }

    // ジャンプボタンの挙動をコントロールする処理
    private void JumpController()
    {
        // ジャンプスライダーの初期化
        m_JumpSlider.value = m_MinLaunchForce;

        // 最大の力を超えていて、まだジャンプしていないのであれば...
        if (m_CurrentJumpForce >= m_MaxLaunchForce && !m_Jumped)
        {
            // 最大の力でジャンプ
            m_CurrentJumpForce = m_MaxLaunchForce;
            Jump();
        }
        // ジャンプボタンが押され始めたばかりの場合...
        else if (m_JumpTapStatus == JumpTapStatus.TapOn)
        {
            // ジャンプ後フラグをリセットし、ジャンプ力をリセットする
            m_Jumped = false;
            m_CurrentJumpForce = m_MinLaunchForce;

            // m_JumpTapStatusをタップ中状態に
            m_JumpTapStatus = JumpTapStatus.Tapping;

            // 力溜め効果音を設定して再生
            m_PlayerAudio.clip = m_ChargingClip;
            m_PlayerAudio.Play();
        }
        // ジャンプボタンを押していて、まだジャンプしていなければ...
        else if (m_JumpTapStatus == JumpTapStatus.Tapping && !m_Jumped)
        {
            // ジャンプ力を増加させ、スライダーを更新する
            m_CurrentJumpForce += m_ChargeSpeed * Time.deltaTime;
            m_JumpSlider.value = m_CurrentJumpForce;
        }
        // ジャンプボタンを離して、まだジャンプしていなければ...
        else if (m_JumpTapStatus == JumpTapStatus.TapOff && !m_Jumped)
        {
            // 現状のジャンプ力でジャンプする
            Jump();
        }
    }

    // ジャンプ時の処理
    private void Jump()
    {
        //m_CurrentLaunchForce（溜めたJumpPower）の分だけ上方に力がかかる
        m_Rb.AddForce(transform.up * m_CurrentJumpForce);

        // 空中ジャンプのエフェクト＆効果音再生
        if (!m_Ground)
        {
            m_JumpParticle.Play();

            m_PlayerAudio.clip = m_JumpClip;
            m_PlayerAudio.Play();
        }

        // ジャンプ力をリセットする
        m_CurrentJumpForce = m_MinLaunchForce;
        // ジャンプ後フラグを立てる
        m_Jumped = true;
        // m_JumpTapStatusもリセット
        m_JumpTapStatus = JumpTapStatus.TapOff;
    }

    // ジャンプボタンを押した時のイベント
    public void OnTapJumpButton()
    {
        m_JumpTapStatus = JumpTapStatus.TapOn;
    }

    // ジャンプボタンを離した時のイベント
    public void OffTapJumpButton()
    {
        m_JumpTapStatus = JumpTapStatus.TapOff;
    }

    public void OnRedMoveButton()
    {
        ChangeMoveTarget(m_RedStr);
    }

    public void OnBlueMoveButton()
    {
        ChangeMoveTarget(m_BlueStr);
    }

    public void OnYellowMoveButton()
    {
        ChangeMoveTarget(m_YellowStr);
    }

    //PlayerMoveTargetを各色のTrampoline上に持ってくる
    private void ChangeMoveTarget(string colerFlg)
    {
        //移動距離を格納する
        Vector3 movedDstance = Vector3.zero;

        switch (colerFlg)
        {
            case m_RedStr:
                movedDstance = new Vector3(0f,transform.position.y,0f);
                break;
            case m_BlueStr:
                movedDstance = new Vector3(2.5f, transform.position.y, 0f);
                break;
            case m_YellowStr:
                movedDstance = new Vector3(-2.5f, transform.position.y, 0f);
                break;
        }

        m_MoveTarget.transform.localPosition = movedDstance;
    }

    //何かしら触れている間作動
    private void OnCollisionStay(Collision col)
    {
        m_Ground = true;
    }

    //何かしらから離れると作動
    private void OnCollisionExit(Collision col)
    {
        m_Ground = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Trampoline"))
        {
            m_PlayerAudio.clip = m_TrampolineClip;
            m_PlayerAudio.Play();
        }
        else
        {
            m_PlayerAudio.clip = m_NormalGroundClip;
            m_PlayerAudio.Play();
        }
    }
}
