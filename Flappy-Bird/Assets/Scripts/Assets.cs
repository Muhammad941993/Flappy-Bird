using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assets : MonoBehaviour
{
    static private Assets instance;

    private void Awake()
    {
        instance = this;
    }
    static public Assets GetInstance()
    {
        return instance;
    }

    public Transform PfPipeHead;
    public Transform PfPipeBody;
}
