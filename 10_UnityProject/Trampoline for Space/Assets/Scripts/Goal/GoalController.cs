using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController
{
    [HideInInspector] public GameObject m_Instance;
    
    private GoalBehaviour m_GoalState;

    public void SetUp()
    {
        m_GoalState = m_Instance.GetComponent<GoalBehaviour>();
        m_Instance.GetComponent<Transform>().localScale = new Vector3(3, 3, 3);// 大きさを3倍に
    }

    public bool CheckClear()
    {
        return m_GoalState.m_ClearFlg;
    }

}
