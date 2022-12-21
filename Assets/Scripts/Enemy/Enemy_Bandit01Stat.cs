using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bandit01Stat : EnemyStat
{
    private void Awake()
    {
        _maxHP = 100;
        _hp = 100;
        _attackPower = 20;
        _defencePower = 30;
        _enemyNum = 1;
    }
}
