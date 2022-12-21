using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFormat : ItemFormat
{
    protected float _attackPower;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
    
    void Awake()
    {
        Name = "¹«±â";
        Price = 10;
        AttackPower = 10;
    }



    public void playWeaponSound()
    {
        audioSource.Play();
    }


}
