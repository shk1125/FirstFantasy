using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemFormat : MonoBehaviour
{
    public Sprite itemIcon;

    protected string _name;
    protected int _price;

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public int Price
    {
        get
        {
            return _price;
        }
        set
        {
            _price = value;
        }
    }

    void Awake()
    {
        Name = "æ∆¿Ã≈€";
        Price = 10;
    }

  
}
