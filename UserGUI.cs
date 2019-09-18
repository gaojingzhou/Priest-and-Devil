using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
public class UserGUI : MonoBehaviour
{
    public int status = 0;
    private UserAction userAction;
    GUIStyle style;
    GUIStyle buttonStyle;

    void Start()
    {
        userAction = SSDirector.getInstance().currentSceneController as UserAction;
        
       
    }

    void OnGUI()
    {
        GUIStyle fontstyle = new GUIStyle();
        fontstyle.normal.background = null;
        fontstyle.normal.textColor = new Color(255, 192, 203);
        fontstyle.fontSize = 50;

        style = new GUIStyle()
        {
            fontSize = 50
        };
        style.normal.textColor = new Color(0, 0, 0);
        buttonStyle = new GUIStyle("button")
        {
            fontSize = 10
        };
        GUI.Label(new Rect(250, 15, 100, 100), "Priest & Devil", fontstyle); //title
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                userAction.restart();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                userAction.restart();
            }

        }
    }
}
