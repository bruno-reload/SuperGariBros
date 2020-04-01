using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface Pool<ItemPool>
{
    void inside(int index);
    void outside(int index);
    ItemPool[] allItems();
    ItemPool getItem(int index);
    void addItems(ItemPool[] items);
}
