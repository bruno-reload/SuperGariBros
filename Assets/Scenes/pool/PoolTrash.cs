using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolTrash: Pool<ItemPool>
{
    private ItemPool[] t;
    
    public int Size{
        set{t = new ItemPool[value];}
        get{return t.Length;}
    }
    public void desable(int index)
    {
        t[index].Able = false;
    }
    public void enable(int index)
    {
        t[index].Able = true;
    }
    public ItemPool[] allItems()
    {
        return t;
    }
    public ItemPool getItem(int index)
    {
        try
        {
            return t[index];
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            throw new IndexOutOfRangeException("index out of boun in player pool");
        }
    }
    public void addItems(ItemPool[] items)
    {
        for(int i =0; i < items.Length;i++)
        {
            t[i] = items[i];
        }
    }
}
