using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType
{
    yellow, blue, green
}
public class Trash : MonoBehaviour
{
    public bool able = false;
    public TrashType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            able = true;
        }
    }
}
