using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform m_Target;
    [SerializeField] private Material m_skybox;      // Skyboxのマテリアル
    [SerializeField] private float m_bgChangeHeight;
    private Vector3 m_offset;      //相対距離取得用

    // Use this for initialization
    public void SetUp()
    {
        // MainCamera(自分自身)とplayerとの相対距離を求める
        m_offset = transform.position - m_Target.position;
        RenderSettings.skybox = m_skybox;
    }

    // Update is called once per frame
    void Update()
    {
        //新しいトランスフォームの値を代入する
        transform.position = m_Target.position + m_offset;

        // 一定の高さで背景を変える
        if (transform.position.y > m_bgChangeHeight)
        {
            RenderSettings.skybox = (Material)Resources.Load("Materials/Space_AnotherPlanet/Space");
        }
        else
        {
            RenderSettings.skybox = m_skybox;
        }
    }
}
