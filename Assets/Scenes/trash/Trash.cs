using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType
{
    yellow, blue, green
}
public class Trash : ItemPool
{
    public TrashType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Active = true;
        }
    }
}
