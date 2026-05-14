using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;
    public Player player;
    private List<ZombieBirthPoint> zombieBirthPoints = new List<ZombieBirthPoint>();
    private int nowCount = 0;
    private int maxCount = 0;
    private List<Zombie> zombies = new List<Zombie>();
    private GameLevelMgr()
    {
        
    }
    public void Clear()
    {
        player = null;
        zombieBirthPoints.Clear();
        nowCount = 0;
        maxCount = 0;
        zombies.Clear();
    }
    public void Init(SceneInfo sceneInfo)
    {
        UIManager.Instance.ShowPanel<GamePanel>();
        RoleInfo roleInfo = GameDataMgr.Instance.nowRole;
        Transform roleBirthPoint = GameObject.Find("RoleBirthPoint").transform;
        GameObject roleObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), roleBirthPoint.position, roleBirthPoint.rotation);
        player = roleObj.GetComponent<Player>();
        player.InitPlayer(roleInfo.atk, sceneInfo.money);
        Camera.main.GetComponent<CameraMove>().SetTarget(roleObj.transform);
        Shelter.Instance.UpdateHP(sceneInfo.shelterHP, sceneInfo.shelterHP);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void AddZombieBirthPoint(ZombieBirthPoint point)
    {
        zombieBirthPoints.Add(point);
    }
    public void UpdateMaxCount(int count)
    {
        maxCount += count;
        nowCount += count;
        UIManager.Instance.GetPanel<GamePanel>().UpdateCount(nowCount, maxCount);
    }
    public void UpdateNowCount(int count)
    {
        nowCount += count;
        UIManager.Instance.GetPanel<GamePanel>().UpdateCount(nowCount, maxCount);
    }
    public bool CheckOver()
    {
        for (int i = 0; i < zombieBirthPoints.Count; i++)
        {
            if (!zombieBirthPoints[i].CheckOver())  return false;
        }
        if (zombies.Count > 0) return false;
        return true;
    }
    public void AddZombie(Zombie zombie)
    {
        zombies.Add(zombie);
    }
    public void RemoveZombie(Zombie zombie)
    {
        zombies.Remove(zombie);
    }
    public Zombie FindZombie(Vector3 pos, float range)
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            if (!zombies[i].isDead && Vector3.Distance(pos, zombies[i].transform.position) <= range)
            {
                return zombies[i];
            }
        }
        return null;
    }
    public List<Zombie> FindZombies(Vector3 pos, float range)
    {
        List<Zombie> list = new List<Zombie>();
        for (int i = 0; i < zombies.Count; i++)
        {
            if (!zombies[i].isDead && Vector3.Distance(pos, zombies[i].transform.position) <= range)
            {
                list.Add(zombies[i]);
            }
        }
        return list;
    }
}
