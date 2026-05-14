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
            if (!SceneExistsInBuildSettings(sceneInfo.sceneName))
            {
                UIManager.Instance.ShowPanel<TipPanel>().SetContent("敬请期待!");
                return;
            }
            SceneManager.LoadSceneAsync(sceneInfo.sceneName).completed += (ao) =>
            {
                GameLevelMgr.Instance.Init(sceneInfo);
                UIManager.Instance.HidePanel<ChooseScenePanel>();
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
        textInfo.text = "名称:\n" + sceneInfo.name + "\n描述:\n" + sceneInfo.tips;
        imageScene.sprite = Resources.Load<Sprite>(sceneInfo.imgRes);
    }

    private bool SceneExistsInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (System.IO.Path.GetFileNameWithoutExtension(path) == sceneName)
                return true;
        }
        return false;
    }
}
