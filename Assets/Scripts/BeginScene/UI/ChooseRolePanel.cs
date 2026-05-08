using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRolePanel : BasePanel
{
    public TMP_Text textMoney;
    public TMP_Text textName;
    public TMP_Text textPrice;
    public Button buttonBuy;
    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonNext;
    public Button buttonBack;
    private Transform heroPos;
    private GameObject roleObj;
    private RoleInfo roleInfo;
    private int index;
    public override void Init()
    {
        heroPos = GameObject.Find("HeroPos").transform;
        if (GameDataMgr.Instance.nowRole == null)
        {
            index = 0;
        }
        else
        {
            index = GameDataMgr.Instance.roleData.IndexOf(GameDataMgr.Instance.nowRole);
        }
        UpdatePanel();
        buttonBuy.onClick.AddListener(() =>
        {
            if (GameDataMgr.Instance.playerData.money >= roleInfo.price)
            {
                GameDataMgr.Instance.playerData.money -= roleInfo.price;
                GameDataMgr.Instance.playerData.heroes.Add(roleInfo.id);
                GameDataMgr.Instance.SavePlayerData();
                UpdatePanel();
                UIManager.Instance.ShowPanel<TipPanel>().SetContent("购买成功");
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().SetContent("购买失败，资金不足");
            }
        });
        buttonLeft.onClick.AddListener(() =>
        {
            index--;
            if (index < 0)
            {
                index = GameDataMgr.Instance.roleData.Count - 1;
            }
            UpdatePanel();
        });
        buttonRight.onClick.AddListener(() =>
        {
            index++;
            if (index > GameDataMgr.Instance.roleData.Count - 1)
            {
                index = 0;
            }
            UpdatePanel();
        });
        buttonNext.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.nowRole = roleInfo;
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
            UIManager.Instance.HidePanel<ChooseRolePanel>();
        });
        buttonBack.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
            UIManager.Instance.HidePanel<ChooseRolePanel>();
        });
    }
    public void UpdatePanel()
    {
        textMoney.text = "$:" + GameDataMgr.Instance.playerData.money.ToString();
        roleInfo = GameDataMgr.Instance.roleData[index];
        textName.text = roleInfo.name;
        if (roleObj != null)
        {
            Destroy(roleObj);
            roleObj = null;
        }
        roleObj = Instantiate(Resources.Load<GameObject>(roleInfo.res), heroPos.position, heroPos.rotation);
        //roleObj.transform.SetParent(heroPos, false);
        if (roleInfo.price > 0 && !GameDataMgr.Instance.playerData.heroes.Contains(roleInfo.id))
        {
            buttonBuy.gameObject.SetActive(true);
            textPrice.text = "$:" + roleInfo.price.ToString();
            buttonNext.gameObject.SetActive(false);
        }
        else
        {
            buttonBuy.gameObject.SetActive(false);
            buttonNext.gameObject.SetActive(true);
        }
    }
    public override void HideMe(Action callBack)
    {
        base.HideMe(callBack);
        if (roleObj != null)
        {
            Destroy(roleObj);
            roleObj = null;
        }
    }
}
