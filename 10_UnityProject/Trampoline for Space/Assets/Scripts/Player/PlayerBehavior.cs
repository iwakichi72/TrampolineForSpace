using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [HideInInspector] public bool m_DeadFlg;
    [HideInInspector] public int m_TookItemNum;
    private Rigidbody m_Rb;
    private float m_FallSpeed;

    private void Start()
    {
        //Rigidbodyを取得
        m_Rb = GetComponent<Rigidbody>();
        m_DeadFlg = false;
        m_TookItemNum = 0;
    }

    private void FixedUpdate()
    {
        // 地面衝突時に良い感じに下降スピードが残ってますように
        m_FallSpeed = m_Rb.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision col)
    {
        // 下降速度が20km?くらいを越えてから地面に着地すると死
        if (m_FallSpeed > 20f && col.gameObject.CompareTag("Ground"))
        {
            Dead();
        }
    }

    private void Dead()
    {
        m_DeadFlg = true;
        m_Rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
    }

    private void OnTriggerEnter(Collider col)
    {
        //アイテムに当たったら取得カウント
        if (col.gameObject.CompareTag("Item"))
        {
            m_TookItemNum++;
        }
    }
}
