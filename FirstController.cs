using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;
public class FirstController : MonoBehaviour, ISceneController, UserAction
{
    public LandController startLand, endLand;
    public BoatController boat;
    private chCtrl.CharacterController[] ch;
    UserGUI userGUI;

    void Awake()
    {
        SSDirector dir = SSDirector.getInstance();
        dir.currentSceneController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        ch = new chCtrl.CharacterController[6];
        loadResources();
    }
    Vector3 riverPos = new Vector3(0, -0.5f, 0);
    public void loadResources()
    {
        GameObject river = Instantiate(Resources.Load("prefabs/river", typeof(GameObject)), riverPos, Quaternion.identity, null) as GameObject;
        river.name = "river";
        startLand = new LandController("from");
        endLand = new LandController("to");
        boat = new BoatController();

        for (int i = 0; i < 3; i ++)
        {
            chCtrl.CharacterController _ch = new chCtrl.CharacterController("priest");
            _ch.setName("priest[" + i + "]");
            _ch.setPosition(startLand.getEmptyPlace());
            _ch.getOnLand(startLand);
            startLand.addObj(_ch);
            ch[i] = _ch;
        }
        for (int i = 0; i < 3; i++)
        {
            chCtrl.CharacterController _ch = new chCtrl.CharacterController("devil");
            _ch.setName("devil[" + i + "]");
            _ch.setPosition(startLand.getEmptyPlace());
            _ch.getOnLand(startLand);
            startLand.addObj(_ch);
            ch[i + 3] = _ch;
        }
    }
    public void moveBoat()
    {
        if (boat.isEmpty()) return;
        boat.Move();
        userGUI.status = check();
    }
    public void objectClicked(chCtrl.CharacterController _ch)
    {
        if (_ch.isOnBoat()) //obj is on boat
        {
            LandController land;
            if (boat.getState() == -1) land = endLand;
            else land = startLand;
            boat.getOffBoat(_ch.getName());
            _ch.movePos(land.getEmptyPlace());
            _ch.getOnLand(land);
            land.addObj(_ch);
        }
        else //obj is on land
        {
            LandController land = _ch.getLandController();
            if (boat.getEmptyNum() == -1) return; //full boat
            if (land.getState() != boat.getState()) return; //different side
            land.removeObj(_ch.getName());
            _ch.movePos(boat.getEmptyPlace());
            _ch.getOnBoat(boat);
            boat.getOnBoat(_ch);
        }
        userGUI.status = check();
    }
    int check()
    {
        int start_p = 0, start_d = 0, end_p = 0, end_d = 0;
        int[] start_count = startLand.getObjNum();
        start_p += start_count[0];
        start_d += start_count[1];
        int[] end_count = endLand.getObjNum();
        end_p += end_count[0];
        end_d += end_count[1];
        if (end_p + end_d == 6) return 2;

        int[] boatCount = boat.getObjNum();
        if (boat.getState() == -1) //to
        {
            end_p += boatCount[0];
            end_d += boatCount[1];
        }
        if (boat.getState() == 1) //from
        {
            start_p += boatCount[0];
            start_d += boatCount[1];
        }
        if (start_p < start_d && start_p > 0)
        {       
            return 1;
        }
        if (end_p < end_d && end_p > 0)
        {
            return 1;
        }
        return 0;
    }
    public void restart()
    {
        boat.reset();
        startLand.reset();
        endLand.reset();
        for (int i = 0; i < ch.Length; i++) ch[i].reset();
    }
}
