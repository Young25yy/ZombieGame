using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBirthPoint : MonoBehaviour
{
    public int maxCount;
    public int numOfOneCount;
    private int nowNum;
    public List<int> zombieIds;
    private int nowID;
    public float birthOffset;
    public float delayTime;
    public float firstDelayTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateOneCount", firstDelayTime);
        GameLevelMgr.Instance.AddZombieBirthPoint(this);
        GameLevelMgr.Instance.UpdateMaxCount(maxCount);
    }
    public void CreateOneCount()
    {
        nowID = zombieIds[Random.Range(0, zombieIds.Count)];
        nowNum = numOfOneCount;
        --maxCount;
        GameLevelMgr.Instance.UpdateNowCount(-1);
        CreateZombie();
    }
    public void CreateZombie()
    {
        ZombieInfo info = GameDataMgr.Instance.zombieData[nowID - 1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), transform.position, Quaternion.identity);
        obj.GetComponent<Zombie>().InitZombie(info);
        --nowNum;
        GameLevelMgr.Instance.AddZombie(obj.GetComponent<Zombie>());
        if (nowNum == 0)
        {
            if (maxCount == 0) return;
            Invoke("CreateOneCount", delayTime);
        }
        else
        {
            Invoke("CreateZombie", birthOffset);
        }
    }
    public bool CheckOver()
    {
        return nowNum == 0 && maxCount == 0;
    }
}
