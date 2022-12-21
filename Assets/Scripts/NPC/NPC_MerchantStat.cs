using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MerchantStat : NPCStat
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
        _maxHP = 5000;
        _hp = 5000;
        _attackPower = 0;
        _defencePower = 0;
        _money = 100;
    }
}
