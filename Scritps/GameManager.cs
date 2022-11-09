using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Starting,//0
        Playing,//1
        GameOver//2
    }

    public GameState state;
    public TargetController targetPb;
    int m_score;

    public int Score { 
        get => m_score; 
        set  {
            m_score = value;            
            Prefs.bestScore = value;      // change value m_score = bestScore implement from Prefs.cs
        }
    }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();
        state = GameState.Starting;
        GUIManager.Ins.ShowGameGUI(false);
        GUIManager.Ins.UpdateApple(m_score);

        AudioController.Ins.PlayBackgroundMusic();
    }
    IEnumerator SpawnTargetCo() /// 
    {
        float xPos = Random.Range(0f, 10f);
        float yPos = Random.Range(-2.5f, 5f);

        yield return new WaitForSeconds(0.3f);
        if (targetPb)                                                
            Instantiate(targetPb, new Vector3(xPos, yPos, 0), Quaternion.identity); // if targetPb # null then new toa do x,y,z and ko xoay
    }
    public void SpawnTarget()
    {
        if (state == GameState.GameOver) return;
        StartCoroutine(SpawnTargetCo());

    }
    public void PlayGame()
    {
        state = GameState.Playing;
        GUIManager.Ins.ShowGameGUI(true);
        SpawnTarget();
    }
    public void GameOver()
    {
        if (GUIManager.Ins.gameoverDialog)
            GUIManager.Ins.gameoverDialog.Show(true);
    }
}
