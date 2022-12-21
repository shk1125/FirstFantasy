using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatFormat : MonoBehaviour
{
    protected float _maxHP;
    protected float _hp;
    protected float _attackPower;
    protected float _defencePower;

    public float MaxHP
    {
        get
        {
            return _maxHP;
        }
        set
        {
            _maxHP = value;
        }
    }
    public float HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    public float AttackPower
    {
        get
        {
            return _attackPower;
        }
        set
        {
            _attackPower = value;
        }
    }
    public float DefencePower
    {
        get
        {
            return _defencePower;
        }
        set
        {
            _defencePower = value;
        }
    }


    private void Awake()
    {
        MaxHP = 100;
        HP = 100;
        AttackPower = 0;
        DefencePower = 0;
    }


}

   
