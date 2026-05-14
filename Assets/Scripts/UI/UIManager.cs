using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;
    public Transform canvasTrans;
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();//字典里面存储的是当前显示出来的面板
    private UIManager()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        GameObject.DontDestroyOnLoad(canvas);
    }
    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName)) 
        {
            return panelDic[panelName] as T;
        }
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        T panel = panelObj.GetComponent<T>();
        panel.ShowMe();
        panelDic.Add(panelName, panel);
        return panel;
    }
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            T panel = panelDic[panelName] as T;
            if (isFade)
            {
                panel.HideMe(() =>
                {
                    GameObject.Destroy(panel.gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panel.gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
}
