using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;
    public Text appleCountingText;
    public GameoverDialog gameoverDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowGameGUI(bool isShow)
    {
        if (gameGUI)
            gameGUI.SetActive(isShow);
        if (homeGUI)
            homeGUI.SetActive(!isShow);
    }
    public void UpdateApple(int apples)
    {
        if (appleCountingText)
            appleCountingText.text = apples.ToString(); /// change apples type int -> String
    }
}
