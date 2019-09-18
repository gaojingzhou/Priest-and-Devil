using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;
namespace myGame
{
    public class SSDirector : System.Object
    {
        private static SSDirector _instance;
        public ISceneController currentSceneController { get; set; }
        public static SSDirector getInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }
    }

    public interface ISceneController
    {
        void loadResources();
    }

    public interface UserAction
    {
        void moveBoat(); //move boat
        void restart(); //restart game
        void objectClicked(chCtrl.CharacterController ch);
    }


}
