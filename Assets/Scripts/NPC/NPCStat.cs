using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStat : StatFormat
{
    public string _name;
   
    
    
    
    private void Awake()
    {
        MaxHP = 5000;
        HP = 5000;
        AttackPower = 0;
        DefencePower = 0;
    }
}
