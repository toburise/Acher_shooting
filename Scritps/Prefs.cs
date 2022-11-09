using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs 
{
   public static int bestScore
    {
        set
        {
            if (PlayerPrefs.GetInt(Const.BEST_SCORE_KEY) < value)
            {
                PlayerPrefs.SetInt(Const.BEST_SCORE_KEY, value);
            }
        }
        get => PlayerPrefs.GetInt(Const.BEST_SCORE_KEY);
    }
}
