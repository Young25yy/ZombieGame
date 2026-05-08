using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public TMP_Text textContent;
    public Button buttonSure;
    public override void Init()
    {
        buttonSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }
    public void SetContent(string content)
    {
        textContent.text = content;
    }
}
