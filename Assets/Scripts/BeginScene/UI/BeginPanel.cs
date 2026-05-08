using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button buttonStart;
    public Button buttonSetting;
    public Button buttonAbout;
    public Button buttonQuit;
    public override void Init()
    {
        buttonStart.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanel<ChooseRolePanel>();
            });
            UIManager.Instance.HidePanel<BeginPanel>();
        });
        buttonSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
            //UIManager.Instance.HidePanel<BeginPanel>();
        });
        buttonAbout.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<AboutPanel>();
            //UIManager.Instance.HidePanel<BeginPanel>();
        });
        buttonQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
