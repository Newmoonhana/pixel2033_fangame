using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public DBM db_mng;

    private void Awake()
    {
        if (inst != null)
        {
            return;
        }
        inst = this;
    }

    private void Start()
    {
        inst.db_mng.eventscp_mng.StartEvent();
    }
}
