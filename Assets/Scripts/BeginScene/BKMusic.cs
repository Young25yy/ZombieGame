using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        MSData msData = GameDataMgr.Instance.msData;
        SetMusicOpen(msData.musicOpen);
        SetMusicValue(msData.musicValue);
    }
    public void SetMusicOpen(bool b)
    {
        audioSource.mute = !b;
    }
    public void SetMusicValue(float v)
    {
        audioSource.volume = v;
    }
}
