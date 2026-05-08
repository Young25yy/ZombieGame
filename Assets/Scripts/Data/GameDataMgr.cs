using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;
    public RoleInfo nowRole;
    public MSData msData;
    public List<RoleInfo> roleData;
    public PlayerData playerData;
    public List<SceneInfo> sceneData;
    private GameDataMgr()
    {
        msData = JsonMgr.Instance.LoadData<MSData>("MSData");
        roleData = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        sceneData = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
    }
    public void SaveMSData()
    {
        JsonMgr.Instance.SaveData(msData, "MSData");
    }
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
}
