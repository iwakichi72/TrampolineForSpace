using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public float m_ItemPower = 10f;
    public ParticleSystem m_ItemPickUpPt;
    public AudioSource m_ItemAudio;
    public AudioClip m_ItemPickUpClip;

    private void OnTriggerEnter(Collider col)
    {
        // Particleを独立させてから、Particleを再生、音も出す
        m_ItemPickUpPt.transform.parent = null;
        m_ItemPickUpPt.Play();
        m_ItemAudio.clip = m_ItemPickUpClip;
        m_ItemAudio.Play();

        // アイテムによって異なる力量で、Playerを上へ打ち上げる
        col.gameObject.GetComponent<Rigidbody>().AddForce(col.gameObject.transform.up * m_ItemPower);

        // Particleは再生が終わってからDestroyする
        Destroy(m_ItemPickUpPt.gameObject, m_ItemPickUpPt.duration);
        Destroy(this.gameObject);
    }
}
