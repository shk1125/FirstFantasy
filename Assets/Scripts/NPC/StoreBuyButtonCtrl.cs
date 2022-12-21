using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBuyButtonCtrl : MonoBehaviour
{
    public int itemNumber, itemPrice;
    public Sprite itemIcon;
    
    public void NPCBuyItem()
    {
        GameManager.instance.NPCBuyItem(itemNumber, itemPrice);
    }
}
