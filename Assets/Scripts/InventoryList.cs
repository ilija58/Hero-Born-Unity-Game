using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList<T> where T: class
{
    public InventoryList()
    {
        Debug.Log("Generic list initialized...");
    }

    private T _item;

    public T item 
    { 
        get{ return _item; } 
    }

    public void setItem(T newItem)
    {
        _item = newItem;
        Debug.Log("New item added...");
    }
}
