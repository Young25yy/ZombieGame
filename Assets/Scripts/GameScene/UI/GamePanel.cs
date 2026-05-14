using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imageHP;
    public Image imageHPBK;
    public TMP_Text textHP;
    public TMP_Text textCount;
    public TMP_Text textMoney;
    public Button buttonQuit;
    public float hpWid;
    public float hpHei;
    public RectTransform botTrans;
    public List<TowerButton> buttonTowers;
    private TowerPoint nowPoint = null;
    private bool checkInput;
    protected override void Awake()
    {
        base.Awake();
        hpWid = imageHPBK.rectTransform.sizeDelta.x;
        hpHei = imageHPBK.rectTransform.sizeDelta.y;
    }

    public override void Init()
    {
        buttonQuit.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("BeginScene").completed += (ao) =>
            {
                GameLevelMgr.Instance.Clear();
                UIManager.Instance.HidePanel<GamePanel>();
            };
        });
        botTrans.gameObject.SetActive(false);
    }
    public void UpdateShelterHP(int hp, int maxHP)
    {
        textHP.text = hp + "/" + maxHP;
        imageHP.rectTransform.sizeDelta = new Vector2((float)hp / maxHP * hpWid, hpHei);
    }
    public void UpdateMoney(int money)
    {
        textMoney.text = money.ToString();
    }
    public void UpdateCount(int nowCount, int maxCount)
    {
        textCount.text = nowCount + "/" + maxCount;
    }
    public void UpdateTower(TowerPoint point)
    {
        nowPoint = point;
        if (nowPoint == null)
        {
            checkInput = false;
            botTrans.gameObject.SetActive(false);
            return;
        }
        else
        {
            checkInput = true;
            botTrans.gameObject.SetActive(true);
            if (nowPoint.info == null)
            {
                for (int i = 0; i < buttonTowers.Count; i++)
                {
                    buttonTowers[i].gameObject.SetActive(true);
                    buttonTowers[i].Init(nowPoint.towerIds[i], "数字键" + (i + 1));
                }
            }
            else
            {
                for (int i = 0; i < buttonTowers.Count; i++)
                {
                    buttonTowers[i].gameObject.SetActive(false);
                }
                buttonTowers[1].gameObject.SetActive(true);
                buttonTowers[1].Init(nowPoint.info.nextLev, "空格键");
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        if (!checkInput) return;
        if (nowPoint.info == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowPoint.CreateTower(nowPoint.towerIds[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowPoint.CreateTower(nowPoint.towerIds[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowPoint.CreateTower(nowPoint.towerIds[2]);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowPoint.CreateTower(nowPoint.info.nextLev);
            }
        }
    }
}
