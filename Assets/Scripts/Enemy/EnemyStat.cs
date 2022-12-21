using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : StatFormat
{
    protected int _enemyNum;

    public int EnemyNum
    {
        get
        {
            return _enemyNum;
        }
        set
        {
            _enemyNum = value;
        }
    }


    private void Awake()
    {
        MaxHP = 100;
        HP = 100;
        AttackPower = 20;
        DefencePower = 30;
    }
}
