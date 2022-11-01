using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScriptManager : MonoBehaviour
{
    DBM dbm;
    Image Obj_img;

    private void Awake()
    {
        dbm = GameManager.inst.db_mng;
        Obj_img = dbm.Obj_img;
    }

    public void StartEvent()
    {
        dbm.story_tree.CreateTree(dbm);
        Obj_img.sprite = dbm.objspr_lst[0];
        dbm.button_arr[1].gameObject.SetActive(false);
        dbm.button_arr[2].gameObject.SetActive(false);
        dbm.eventscp_mng.StartCoroutine(dbm.eventscp_mng.Event0000(0.01f));
    }

    //버튼 이벤트 진행 중 index에 따른 이벤트 실행.
    public IEnumerator EventIn0000(float _speed)    //페이드인.
    {
        float alpha = 0;
        dbm.overray_img.color = new Color(0.1607843f, 0.1607843f, 0.1607843f, alpha);
        while (dbm.overray_img.color.a < 1)
        {
            alpha += _speed;
            dbm.overray_img.color = new Color(0.1607843f, 0.1607843f, 0.1607843f, alpha);
            yield return null;
        }
        dbm.overray_img.color = new Color(0.1607843f, 0.1607843f, 0.1607843f, 1);
    }
    public IEnumerator Event0000(float _speed)   //페이드인 아웃.
    {
        float alpha = 1;
        dbm.overray_img.color = new Color(0.1607843f, 0.1607843f, 0.1607843f, alpha);
        while (dbm.overray_img.color.a > 0)
        {
            alpha -= _speed;
            dbm.overray_img.color = new Color(0.1607843f, 0.1607843f, 0.1607843f, alpha);
            yield return null;
        }
        dbm.overray_img.color = new Color(0.1607843f, 0.1607843f, 0.1607843f, 0);
    }
    public IEnumerator Event7112() //코로나2020 로딩창.
    {
        yield return dbm.wait_03_00;
        dbm.overray_img.sprite = dbm.overray_spr1;
        dbm.overray_img.color = new Color(1, 1, 1, 1);
        yield return dbm.wait_03_00;

        dbm.overray_img.sprite = dbm.overray_spr2;
        float alpha = 0;
        dbm.overray_img.color = new Color(1, 1, 1, alpha);
        while (dbm.overray_img.color.a < 1)
        {
            alpha += 0.003f;
            dbm.overray_img.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        dbm.overray_img.color = new Color(1, 1, 1, 1);

        yield return dbm.wait_03_00;
        yield return dbm.wait_03_00;
        alpha = 1;
        dbm.overray_img.color = new Color(1, 1, 1, alpha);
        while (dbm.overray_img.color.a > 0)
        {
            alpha -= 0.003f;
            dbm.overray_img.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        dbm.overray_img.sprite = dbm.overray_spr0;
        dbm.overray_img.color = new Color(0.1607843f, 0.1607843f, 0.1607843f, 0);
        dbm.story_tree.MoveNextNode(1);
    }
}
