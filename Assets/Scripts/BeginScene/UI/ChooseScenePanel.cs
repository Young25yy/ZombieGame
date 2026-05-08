using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonStart;
    public Button buttonBack;
    public Image imageScene;
    public SceneInfo sceneInfo;
    public TMP_Text textInfo;
    private int index = 0;
    public override void Init()
    {
        UpdatePanel();
        buttonLeft.onClick.AddListener(() =>
        {
            index--;
            if (index < 0)
            {
                index = GameDataMgr.Instance.sceneData.Count - 1;
            }
            UpdatePanel();
        });
        buttonRight.onClick.AddListener(() =>
        {
            index++;
            if (index > GameDataMgr.Instance.sceneData.Count - 1)
            {
                index = 0;
            }
            UpdatePanel();
        });
        buttonStart.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(sceneInfo.sceneName).completed += (ao) =>
            {
                UIManager.Instance.HidePanel<ChooseScenePanel>();
                UIManager.Instance.ShowPanel<GamePanel>();
            };
        });
        buttonBack.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<ChooseRolePanel>();
            UIManager.Instance.HidePanel<ChooseScenePanel>();
        });
    }
    public void UpdatePanel()
    {
        sceneInfo = GameDataMgr.Instance.sceneData[index];
        textInfo.text = "√˚≥∆£∫\n" + sceneInfo.name + "\nÃ· æ£∫\n" + sceneInfo.tips;
        imageScene.sprite = Resources.Load<Sprite>(sceneInfo.imgRes);
    }
}
