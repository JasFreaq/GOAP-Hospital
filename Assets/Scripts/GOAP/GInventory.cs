using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    private List<GameObject> _items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        _items.Add(item);
    }

    public GameObject FindItemWithTag(string tag)
    {
        foreach (GameObject item in _items)
        {
            if (item.CompareTag(tag))
                return item;
        }

        return null;
    }

    public void RemoveItem(GameObject item)
    {
        _items.Remove(item);
    }
}
