using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    [HideInInspector] public bool m_ClearFlg;

    private void Start()
    {
        m_ClearFlg = false;
    }

    // ゴールするときは通り抜ける前提
    private void OnTriggerExit(Collider col)
    {
        Goal();
    }

    private void Goal()
    {
        BoxCollider[] m_BoxCols = gameObject.GetComponents<BoxCollider>();
        foreach (var col in m_BoxCols)
        {
            col.isTrigger = false;
        }

        m_ClearFlg = true;
    }

}
