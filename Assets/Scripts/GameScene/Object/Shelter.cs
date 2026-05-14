using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour
{
    private int hp;
    private int maxHP;
    private bool isDead = false;
    private static Shelter instance;
    public static Shelter Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateHP(int hp, int maxHP)
    {
        this.hp = hp;
        this.maxHP = maxHP;
        UIManager.Instance.GetPanel<GamePanel>().UpdateShelterHP(hp, maxHP);
    }
    public void Wound(int damage)
    {
        if (isDead) return;
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            UIManager.Instance.ShowPanel<GameOverPanel>()
            .SetInfo(false, (int)(GameLevelMgr.Instance.player.money * 0.25f));
        }
        UpdateHP(hp, maxHP);
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
