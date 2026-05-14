using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public TMP_Text textState;
    public TMP_Text textReward; 
    public Button buttonSure;
    public override void Init()
    {
        buttonSure.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("BeginScene").completed += (ao) =>
            {
                GameLevelMgr.Instance.Clear();
                UIManager.Instance.HidePanel<GameOverPanel>();
                UIManager.Instance.HidePanel<GamePanel>();
            };
        });
    }
    public void SetInfo(bool isWin, int reward)
    {
        textState.text = isWin ? "胜利" : "失败";
        textReward.text = reward + "$";
        GameDataMgr.Instance.playerData.money += reward;
        GameDataMgr.Instance.SavePlayerData();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        GameLevelMgr.Instance.player.SetCursorVisible(true);
    }
}
