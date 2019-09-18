using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;
public class LandController //control land transactions
{
    readonly GameObject land;
    readonly Vector3 startPos = new Vector3(8.5f, 0, 0); //start position
    readonly Vector3 endPos = new Vector3(-8.5f, 0, 0); //end position
    readonly Vector3[] pos;
    readonly int state; //-1 for to 1 for from

    chCtrl.CharacterController[] passenger;

    public void reset()
    {
        passenger = new chCtrl.CharacterController[6];
    }

    public LandController(string _state)
    {
        passenger = new chCtrl.CharacterController[6];
        pos = new Vector3[] {new Vector3(6.5F,0.8f,0), new Vector3(7.5F,0.8F,0),
                new Vector3(8.5F,0.8F,0),
                new Vector3(9.5F,0.8F,0), new Vector3(10.5F,0.8F,0),
                new Vector3(11.5F,0.8F,0)
        }; //object position

        if (_state == "from") //from
        {
            land = Object.Instantiate(Resources.Load("prefabs/land", typeof(GameObject)), startPos, Quaternion.identity, null) as GameObject;
            land.name = "from";
            state = 1;
        }
        else //to
        {
            land = Object.Instantiate(Resources.Load("prefabs/land", typeof(GameObject)), endPos, Quaternion.identity, null) as GameObject;
            land.name = "to";
            state = -1;
        }
    }
    public int getEmptyNum()
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null) return i;
        }
        return -1;
    }
    public Vector3 getEmptyPlace()
    {
        int index = getEmptyNum();
        Vector3 v = pos[index];
        v.x *= state;
        return v;
    }
    public int getState()
    {
        return state;
    }
    public int[] getObjNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null) continue;
            if (passenger[i].getMark() == 0) count[0]++; //p
            else count[1]++; //d
        }
        return count;
    }
    public void addObj(chCtrl.CharacterController ch) //get on land
    {
        int index = getEmptyNum();
        passenger[index] = ch;
    }
    public chCtrl.CharacterController removeObj(string name) //get off land
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] != null && passenger[i].getName() == name)
            {
                chCtrl.CharacterController ch = passenger[i];
                passenger[i] = null;
                return ch;
            }
        }
        return null;
    }
}
