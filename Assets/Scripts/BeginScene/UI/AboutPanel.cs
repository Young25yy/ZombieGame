using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AboutPanel : BasePanel
{
    public TMP_Text text;
    public Button buttonBack;
    public override void Init()
    {
        buttonBack.onClick.AddListener(() =>
        {
            //UIManager.Instance.ShowPanel<BeginPanel>();
            UIManager.Instance.HidePanel<AboutPanel>();
        });
        text.text = File.ReadAllText(Application.streamingAssetsPath + "/About.txt");
    }
}
