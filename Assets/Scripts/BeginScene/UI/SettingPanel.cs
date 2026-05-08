using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Toggle toggleMusic;
    public Toggle toggleSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    public Button buttonQuit;
    public override void Init()
    {
        toggleMusic.isOn = GameDataMgr.Instance.msData.musicOpen;
        toggleSound.isOn = GameDataMgr.Instance.msData.soundOpen;
        sliderMusic.value = GameDataMgr.Instance.msData.musicValue;
        sliderSound.value = GameDataMgr.Instance.msData.soundValue;
        toggleMusic.onValueChanged.AddListener((isOn) =>
        {
            GameDataMgr.Instance.msData.musicOpen = isOn;
            BKMusic.Instance.SetMusicOpen(isOn);
        });
        toggleSound.onValueChanged.AddListener((isOn) =>
        {
            GameDataMgr.Instance.msData.soundOpen = isOn;
        });
        sliderMusic.onValueChanged.AddListener((value) =>
        {
            GameDataMgr.Instance.msData.musicValue = value;
            BKMusic.Instance.SetMusicValue(value);
        });
        sliderSound.onValueChanged.AddListener((value) =>
        {
            GameDataMgr.Instance.msData.soundValue = value;
        });
        buttonQuit.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.SaveMSData();
            //UIManager.Instance.ShowPanel<BeginPanel>();
            UIManager.Instance.HidePanel<SettingPanel>();
        });
    }
}
