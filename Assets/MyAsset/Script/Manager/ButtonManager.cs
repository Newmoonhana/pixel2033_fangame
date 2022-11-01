using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    DBM dbm;
    Image Obj_img;
    private void Awake()
    {
        dbm = GameManager.inst.db_mng;
        Obj_img = dbm.Obj_img;
    }

    public void ButtonEventDefault(int _eventID)
    {
        StartCoroutine(ButtonEventDefault_Cor(_eventID));
    }
    public IEnumerator ButtonEventDefault_Cor(int _eventID)
    {
        dbm.button_arr[0].gameObject.transform.parent.gameObject.SetActive(false);
        if (dbm.story_tree.GetNextEventIndex(_eventID) == 0)
            yield return dbm.eventscp_mng.StartCoroutine(dbm.eventscp_mng.EventIn0000(0.005f));
        dbm.story_tree.MoveNextNode(_eventID);
        switch (dbm.story_tree.GetEventIndex())
        {
            case 0:
                yield return dbm.eventscp_mng.StartCoroutine(dbm.eventscp_mng.Event0000(0.005f));
                break;
            case 7112:
                yield return dbm.eventscp_mng.StartCoroutine(dbm.eventscp_mng.Event7112());
                break;
        }
        dbm.button_arr[0].gameObject.transform.parent.gameObject.SetActive(true);
    }
}
