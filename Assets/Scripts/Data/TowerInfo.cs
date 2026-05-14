using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo
{
    // 塔ID
    public int id;

    // 塔名称
    public string name;

    // 价格/花费
    public int money;

    // 攻击力
    public int atk;

    // 攻击范围
    public int atkRange;

    // 攻击偏移时间/攻击间隔
    public float offSetTime;

    // 下一级塔的ID，0表示已满级
    public int nextLev;

    // 图片资源路径
    public string imgRes;

    // 塔预制体资源路径
    public string res;

    // 攻击类型（1: 普通攻击, 2: 其他类型）
    public int atkType;

    // 攻击特效资源路径
    public string eff;
}
