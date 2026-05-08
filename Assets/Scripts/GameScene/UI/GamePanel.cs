using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imageHP;
    public TMP_Text textHP;
    public TMP_Text textCount;
    public TMP_Text textMoney;
    public Button buttonQuit;
    public float hpWid;
    public float hpHei;
    public RectTransform botTrans;
    public List<TowerButton> buttonListTowers;
    public override void Init()
    {
        hpWid = imageHP.rectTransform.sizeDelta.x;
        hpHei = imageHP.rectTransform.sizeDelta.y;
        buttonQuit.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("BeginScene").completed += (ao) =>
            {
                UIManager.Instance.HidePanel<GamePanel>();
            };
        });
        botTrans.gameObject.SetActive(false);
    }
    public void UpdateShelterHP(int hp, int maxHP)
    {
        textHP.text = hp + "/" + maxHP;
        imageHP.rectTransform.sizeDelta = new Vector2((float)(hp / maxHP) * hpWid, hpHei);
    }
    public void UpdateMoney(int money)
    {
        textMoney.text = money.ToString();
    }
    public void UpdateCount(int nowCount, int maxCount)
    {
        textCount.text = nowCount + "/" + maxCount;
    }
}
