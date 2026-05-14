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
    public List<ZombieInfo> zombieData;
    public List<TowerInfo> towerData;
    private GameDataMgr()
    {
        msData = JsonMgr.Instance.LoadData<MSData>("MSData");
        roleData = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        sceneData = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        zombieData = JsonMgr.Instance.LoadData<List<ZombieInfo>>("ZombieInfo");
        towerData = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }
    public void SaveMSData()
    {
        JsonMgr.Instance.SaveData(msData, "MSData");
    }
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
    public void PlaySound(string res)
    {
        GameObject soundObj = new GameObject();
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = Resources.Load<AudioClip>(res);
        audioSource.volume = msData.soundValue;
        audioSource.mute = !msData.soundOpen;
        audioSource.Play();
        GameObject.Destroy(soundObj, audioSource.clip.length);
    }
}
