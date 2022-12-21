using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<ItemFormat> itemList;


    void Start()
    {
        itemList = new List<ItemFormat>();
    }

    
    public void AddItem(ItemFormat item)
    {
        itemList.Add(item);
        GameManager.instance.AddItem(item);
    }
}
