using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface Pool<ItemPool>
{
    void desable(int index);
    void enable(int index);
    ItemPool[] allItems();
    ItemPool getItem(int index);
    void addItems(ItemPool[] items);
}
