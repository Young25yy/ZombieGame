using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public Image imageTower;
    public TMP_Text textTip;
    public TMP_Text textPrice;
    public void Init(int id, string inputStr)
    {
        TowerInfo info = GameDataMgr.Instance.towerData[id - 1];
        imageTower.sprite = Resources.Load<Sprite>(info.imgRes);
        textTip.text = inputStr;
        textPrice.text = info.money + "$";
        if (info.money > GameLevelMgr.Instance.player.money)
        {
            textTip.text = "金钱不足";
        }
    }
}
