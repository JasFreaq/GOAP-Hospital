using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    private List<GameObject> _items = new List<GameObject>();

    public IReadOnlyList<GameObject> Items { get { return _items; } }

    public void AddItem(GameObject item)
    {
        _items.Add(item);
    }

    public GameObject FindItemWithTag(string tag)
    {
        GameObject foundItem = null;
        List<GameObject> removableItems = new List<GameObject>();

        foreach (GameObject item in _items)
        {
            if (item == null)
                removableItems.Add(item);
            else
            {
                if (item.CompareTag(tag))
                    foundItem = item;
            }
        }

        foreach (GameObject item in removableItems)
        {
            _items.Remove(item);
        }

        return foundItem;
    }

    public void RemoveItem(GameObject item)
    {
        _items.Remove(item);
    }
}
