using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBM : MonoBehaviour
{
    public ButtonManager button_mng;
    public EventScriptManager eventscp_mng;

    public StoryTree story_tree = new StoryTree();

    public List<Sprite> objspr_lst = new List<Sprite>();
    public Sprite overray_spr0;
    public Sprite overray_spr1;
    public Sprite overray_spr2;

    public Image Obj_img;
    public Button []button_arr;
    public Image overray_img;

    public WaitForSeconds wait_03_00 = new WaitForSeconds(3);
}
