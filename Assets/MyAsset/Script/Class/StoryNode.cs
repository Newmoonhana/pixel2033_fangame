using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class StoryNode
{
    Sprite data_img;
    int degree; //선택지(자식) 개수(최대 3).
    StoryNode[]child = new StoryNode[3];   //선택지.
    int eventOn = -1;   //-1 == 이벤트 X, n == 이벤트 번호.

    public StoryNode()
    {
        data_img = null;
        child[0] = null;
        child[1] = null;
        child[2] = null;
    }
    public StoryNode(StoryNode _s)
    {
        data_img = _s.data_img;
        degree = _s.degree;
        child = _s.child;
        eventOn = _s.eventOn;
    }

    public Sprite GetData()
    {
        return data_img;
    }
    public void SetData(Sprite _data_img)
    {
        data_img = _data_img;
    }
    public int GetDegree()
    {
        return degree;
    }
    public void SetDegree(int _degree)
    {
        degree = _degree;
    }
    public StoryNode GetNextNode(int _index)
    {
        if (_index > degree)
        {
            Debug.Log("GetNextNode Index Error. index = " + _index + ", degree = " + degree);
            return null;
        }

        return child[_index - 1];
    }
    public void SetNextNode(int _index, StoryNode _s)
    {
        if (child[_index - 1] == null)
            child[_index - 1] = new StoryNode();
        child[_index - 1] = _s;
    }
    public void SetNode(Sprite _data_img, int _degree, StoryNode _child1, StoryNode _child2, StoryNode _child3)
    {
        SetData(_data_img);
        SetDegree(_degree);
        SetNextNode(1, _child1);
        SetNextNode(2, _child2);
        SetNextNode(3, _child3);
    }
    public int GetEventOn()
    {
        return eventOn;
    }
    public void SetEventOn(int _index)
    {
        eventOn = _index;
    }
}

public class StoryTree
{
    DBM dbm;
    StoryNode firstnode;
    StoryNode thisnode = new StoryNode();

    public int GetEventIndex()
    {
        return thisnode.GetEventOn();
    }
    public int GetNextEventIndex(int _val)
    {
        return thisnode.GetNextNode(_val).GetEventOn();
    }

    public bool MoveNextNode(int _choice)
    {
        if (thisnode.GetNextNode(_choice) == null)
        {
            return false;
        }
        thisnode = thisnode.GetNextNode(_choice);
        if (thisnode.GetDegree() == 1)
        {
            dbm.button_arr[1].gameObject.SetActive(false);
            dbm.button_arr[2].gameObject.SetActive(false);
        }
        else if (thisnode.GetDegree() == 2)
        {
            dbm.button_arr[1].gameObject.SetActive(true);
            dbm.button_arr[2].gameObject.SetActive(false);
        }
        else if (thisnode.GetDegree() == 3)
        {
            dbm.button_arr[1].gameObject.SetActive(true);
            dbm.button_arr[2].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("MoveNextNode() Error. thisnode.GetDegreed() = " + thisnode.GetDegree());
            return false;
        }

        dbm.Obj_img.sprite = thisnode.GetData();
        return true;
    }

    public void CreateTree(DBM _dbm)
    {
        dbm = _dbm;
        //시작점(1컷).
        StoryNode temp = thisnode;
        temp.SetData(dbm.objspr_lst[0]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //2컷 이동.
        temp.SetEventOn(0);
        thisnode = temp;
        firstnode = new StoryNode(thisnode);

        //2컷.
        temp = thisnode.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[1]);
        temp.SetDegree(3);
        temp.SetNextNode(1, new StoryNode()); //3-1컷 이동.
        temp.SetNextNode(2, new StoryNode()); //3-2컷 이동.
        temp.SetNextNode(3, new StoryNode()); //3-3컷 이동.
        StoryNode temp02 = temp;

        //3-1컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[2]);
        temp.SetDegree(2);
        temp.SetNextNode(1, new StoryNode()); //4-1-1컷 이동.
        temp.SetNextNode(2, new StoryNode()); //4-2컷 이동.
        //3-2컷(Ending).
        temp = temp02.GetNextNode(2);
        temp.SetData(dbm.objspr_lst[3]);
        temp.SetDegree(1);
        temp.SetNextNode(1, firstnode);
        //3-3컷.
        temp = temp02.GetNextNode(3);
        temp.SetData(dbm.objspr_lst[4]);
        temp.SetDegree(2);
        temp.SetNextNode(1, temp02.GetNextNode(1).GetNextNode(1)); //4-1-1컷 이동.
        temp.SetNextNode(2, temp02.GetNextNode(1).GetNextNode(2)); //4-2컷 이동.

        //4-1-1컷.
        temp = temp02.GetNextNode(1).GetNextNode(1);
        temp.SetData(dbm.objspr_lst[5]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //4-1-2컷 이동.
        //4-1-2컷(Ending).
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[6]);
        temp.SetDegree(1);
        temp.SetNextNode(1, firstnode);

        //4-2컷.
        temp = temp02.GetNextNode(1).GetNextNode(2);
        temp.SetData(dbm.objspr_lst[7]);
        temp.SetDegree(2);
        temp.SetNextNode(2, new StoryNode()); //5컷 이동(5-1 가젯 미보유로 null).

        //5컷.
        temp = temp.GetNextNode(2);
        temp.SetData(dbm.objspr_lst[8]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //6컷 이동.

        //6컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[9]);
        temp.SetDegree(3);
        temp.SetNextNode(1, new StoryNode()); //7-1컷 이동.
        temp.SetNextNode(2, new StoryNode()); //7-2컷 이동.
        StoryNode temp07_03 = new StoryNode();
        temp.SetNextNode(3, temp07_03); //7-3컷 이동.
        StoryNode temp06 = temp;

        //7-1컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[10]);
        temp.SetDegree(2);
        temp.SetNextNode(1, new StoryNode()); //7-1-1-1컷 이동.
        temp.SetNextNode(2, new StoryNode()); //7-1-2컷 이동.

        //7-1-1-1컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[11]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //7-1-1-2컷 이동.
        //7-1-1-2컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[12]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //7-1-1-loading컷 이동.
        //7-1-1-loading.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[13]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //7-1-1-3컷 이동.
        temp.SetEventOn(7112);
        //7-1-1-3컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[16]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //7-1-1-4컷 이동.
        //7-1-1-4컷(Ending).
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[17]);
        temp.SetDegree(1);
        temp.SetNextNode(1, firstnode);

        //7-1-2컷(Ending).
        temp = temp06.GetNextNode(1).GetNextNode(2);
        temp.SetData(dbm.objspr_lst[18]);
        temp.SetDegree(1);
        temp.SetNextNode(1, firstnode);

        //7-2컷.
        temp = temp06.GetNextNode(2);
        temp.SetData(dbm.objspr_lst[19]);
        temp.SetDegree(2);
        temp.SetNextNode(1, temp); //7-2컷 이동.
        temp.SetNextNode(2, temp07_03); //7-3컷 이동.

        //7-3컷.
        temp = temp.GetNextNode(2);
        temp.SetData(dbm.objspr_lst[20]);
        temp.SetDegree(1);
        temp.SetNextNode(1, temp); //8컷 이동.

        //8컷.
        temp = temp.GetNextNode(1);
        StoryNode temp08 = temp;
        temp.SetData(dbm.objspr_lst[21]);
        temp.SetDegree(3);
        temp.SetNextNode(1, temp); //8-2-1컷 이동.
        temp.SetNextNode(2, new StoryNode()); //8-2-1컷 이동.
        temp.SetNextNode(3, new StoryNode()); //8-3컷 이동.

        //8-2-1컷.
        temp = temp.GetNextNode(2);
        temp.SetData(dbm.objspr_lst[22]);
        temp.SetDegree(3);
        StoryNode temp2 = new StoryNode();
        temp.SetNextNode(1, temp2); //8-2-2컷 이동.
        temp.SetNextNode(2, temp2); //8-2-2컷 이동.
        temp.SetNextNode(3, temp2); //8-2-2컷 이동.
        //8-2-2컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[23]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //8-2-3컷 이동.
        //8-2-3컷.
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[24]);
        temp.SetDegree(1);
        temp.SetNextNode(1, new StoryNode()); //8-2-4컷 이동.
        //8-2-4컷(Ending).
        temp = temp.GetNextNode(1);
        temp.SetData(dbm.objspr_lst[25]);
        temp.SetDegree(1);
        temp.SetNextNode(1, firstnode);

        //8-3컷(Ending).
        temp = temp08.GetNextNode(3);
        temp.SetData(dbm.objspr_lst[26]);
        temp.SetDegree(1);
        temp.SetNextNode(1, firstnode);
    }
}