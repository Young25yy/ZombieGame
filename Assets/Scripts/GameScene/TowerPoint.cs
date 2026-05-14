using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    private Tower tower = null;
    public TowerInfo info;
    public List<int> towerIds;
    public void CreateTower(int id)
    {
        TowerInfo towerInfo = GameDataMgr.Instance.towerData[id - 1];
        if (towerInfo.money > GameLevelMgr.Instance.player.money) return;
        GameLevelMgr.Instance.player.AddMoney(-towerInfo.money);
        if (tower != null)
        {
            Destroy(tower.gameObject);
            tower = null;
        }
        tower = Instantiate(Resources.Load<Tower>(towerInfo.res), transform.position, Quaternion.identity);
        tower.Init(towerInfo);
        info = towerInfo;
        if (info.nextLev != 0)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateTower(this);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateTower(null);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (info != null && info.nextLev == 0) return;
        UIManager.Instance.GetPanel<GamePanel>().UpdateTower(this);
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateTower(null);
    }
}
