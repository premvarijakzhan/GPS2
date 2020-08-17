using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem SS = null;

    void Awake()
    {
        if (SS == null)
        {
            SS = this;
            DontDestroyOnLoad(this);
        }
        else if (SS != this)
        {
            Destroy(this.gameObject);
        }
    }
}
