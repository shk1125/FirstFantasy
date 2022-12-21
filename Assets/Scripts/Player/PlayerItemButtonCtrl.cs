using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemButtonCtrl : MonoBehaviour
{
    public int itemNumber;


    public void UseItem()
    {
        GameManager.instance.UseItem(itemNumber);
    }
}
