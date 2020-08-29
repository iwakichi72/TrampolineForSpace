using System.Collections;
using UnityEngine;

public class PlayerMoveTargrtController : MonoBehaviour
{
    private Transform m_PlayerTfm;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerTfm = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // y軸はPlayerと合わせ、z軸は0固定
        this.transform.position = new Vector3(this.transform.position.x, m_PlayerTfm.position.y, 0f);
    }
}
