using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStoreFormat : MonoBehaviour
{
    public List<ItemFormat> itemList;


    private void Awake()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            ItemFormat item = Instantiate(itemList[i]);
            itemList[i] = item;
            item.gameObject.SetActive(false);
        }
    }
}


