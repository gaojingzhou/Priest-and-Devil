using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;
public class ClickGUI : MonoBehaviour
{
    UserAction userAction;
    chCtrl.CharacterController ch;
    public void setController(chCtrl.CharacterController _ch)
    {
        ch = _ch;

    }
    
    // Start is called before the first frame update
    void Start()
    {
        userAction = SSDirector.getInstance().currentSceneController as UserAction;
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (gameObject.name == "boat") userAction.moveBoat();
        else userAction.objectClicked(ch);
    }
}
