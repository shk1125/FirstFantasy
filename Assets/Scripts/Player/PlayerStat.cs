using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : StatFormat
{
    protected int _money;

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
        }
    }

    private void Awake()
    {
        MaxHP = 100;
        HP = 100;
        AttackPower = 20;
        DefencePower = 10;
        Money = 100;
    }
}
