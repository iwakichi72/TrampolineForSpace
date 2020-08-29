using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController
{
    [HideInInspector] public GameObject m_Instance;

    private ItemBehavior m_ItemState;

    public void SetUp()
    {
        m_ItemState = m_Instance.GetComponent<ItemBehavior>();
        m_Instance.GetComponent<Transform>().rotation = Quaternion.Euler(-90f, 0, 0);
    }

}
