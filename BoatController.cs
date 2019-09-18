using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;
public class BoatController
{
    readonly GameObject boat;
    readonly Move move;
    readonly Vector3 startPos = new Vector3(4.5f, 0, 0);
    readonly Vector3 endPos = new Vector3(-4.5f, 0, 0);
    readonly Vector3[] startPoss;
    readonly Vector3[] endPoss;
    int state;  //-1 for to 1 for from

    chCtrl.CharacterController[] passenger = new chCtrl.CharacterController[2];

    public BoatController()
    {
        state = 1;
        startPoss = new Vector3[] { new Vector3(4.5F, 0.8f, 0), new Vector3(5.5F, 0.8f, 0) };
        endPoss = new Vector3[] { new Vector3(-5.5F, 0.8f, 0), new Vector3(-4.5F, 0.8f, 0) };
        boat = Object.Instantiate(Resources.Load("prefabs/boat", typeof(GameObject)), startPos, Quaternion.identity, null) as GameObject;
        boat.name = "boat";
        move = boat.AddComponent(typeof(Move)) as Move;
        boat.AddComponent(typeof(ClickGUI));
    }
    public void reset()
    {
        move.reset();
        if (state == -1) Move();
        passenger = new chCtrl.CharacterController[2];
    }
    public GameObject getGameobj() { return boat; }
    public int getState() { return state; }
    public int[] getObjNum() //return obj index
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null)
                continue;
            if (passenger[i].getMark() == 0)
            {
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }
    public void Move() //boat move
    {
        if (state == -1) //to
        {
            move.setDest(startPos);
            state = 1;
        }
        else //from
        {
            move.setDest(endPos);
            state = -1;
        }
    }
    public int getEmptyNum()
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public Vector3 getEmptyPlace()
    {
        Vector3 pos = new Vector3();
        int index = getEmptyNum();
        if (state == -1) //to
        {
            pos = endPoss[index];
        }
        if (state == 1) //from
        {
            pos = startPoss[index];
        }
        return pos;
    }
    public bool isEmpty()
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] != null)
            {
                return false;
            }
        }
        return true;
    }
    public void getOnBoat(chCtrl.CharacterController ch)
    {
        int index = getEmptyNum();
        passenger[index] = ch;
    }
    public chCtrl.CharacterController getOffBoat(string name)
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
