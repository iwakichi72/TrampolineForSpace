using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    [HideInInspector] public GameObject m_Instance;
    private GameObject m_PlayerUI;
    private PlayerMovement m_Movement;
    private PlayerBehavior m_PlayerState;

    public void SetUp()
    {
        m_Movement = m_Instance.GetComponent<PlayerMovement>();
        m_PlayerState =  m_Instance.GetComponent<PlayerBehavior>();
        m_PlayerUI = m_Instance.transform.Find("PlayerUI").gameObject;
    }

    // この関数は、プレイヤーを制御できないようにする必要がある場合に使用。
    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_PlayerUI.SetActive(false);
    }

    // この関数は、プレイヤーがを制御できるようにする必要がある場合に使用。
    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_PlayerUI.SetActive(true);
    }

    // Playerが死んでしまったらゲームオーバー
    public bool CheckGameOver()
    {
        return m_PlayerState.m_DeadFlg;
    }

    public int CheckTookItemNum()
    {
        return m_PlayerState.m_TookItemNum;
    }
}

