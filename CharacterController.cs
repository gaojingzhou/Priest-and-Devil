using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;
namespace chCtrl
{
    public class CharacterController
    {
        readonly Move move;
        readonly ClickGUI clickGUI;
        readonly GameObject obj;
        readonly int objMark; //0 for p, 1 for d
        LandController landController;
        bool onBoat;

        public CharacterController(string ch_name)
        {
            if (ch_name == "priest")
            {

                obj = Object.Instantiate(Resources.Load("prefabs/priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.transform.Rotate(Vector3.up, -90);
                objMark = 0;
            }
            if (ch_name == "devil")
            {
                obj = Object.Instantiate(Resources.Load("prefabs/devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.transform.Rotate(Vector3.up, -90);
                objMark = 1;
            }
            move = obj.AddComponent(typeof(Move)) as Move;
            clickGUI = obj.AddComponent(typeof(ClickGUI)) as ClickGUI;
            clickGUI.setController(this);
        }
        public void reset()
        {
            move.reset();
            landController = (SSDirector.getInstance().currentSceneController as FirstController).startLand;
            getOnLand(landController);
            setPosition(landController.getEmptyPlace());
            landController.addObj(this);
        }
        public int getMark() { return objMark; }
        public string getName() { return obj.name; }
        public LandController getLandController() { return landController; }
        public bool isOnBoat() { return onBoat; }
        public void setName(string name) { obj.name = name; }
        public void setPosition(Vector3 pos) { obj.transform.position = pos; }
        public void movePos(Vector3 dest) { move.setDest(dest); }
        public void getOnBoat(BoatController bo)
        {
            landController = null;
            obj.transform.parent = bo.getGameobj().transform;
            onBoat = true;
        }
        public void getOnLand(LandController la)
        {
            landController = la;
            obj.transform.parent = null;
            onBoat = false;
        }

    }
}
