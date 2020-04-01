using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControll : MonoBehaviour
{
    public ItemPool[] PlayerPoolTarget;
    public ItemPool[] TrashPoolTarget;

    public static Pool<ItemPool> poolPlayer;
    public static Pool<ItemPool> poolTrash;

    private void Awake()
    {
        ItemPool[] PPTBuff = new Player[PlayerPoolTarget.Length];
        ItemPool[] TPTBuff = new Trash[TrashPoolTarget.Length];

        poolPlayer = new PoolPlayer();
        poolTrash = new PoolTrash();

        (poolPlayer as PoolPlayer).Size = PlayerPoolTarget.Length;
        (poolTrash as PoolTrash).Size = TrashPoolTarget.Length;

        for (int i = 0; i < PlayerPoolTarget.Length; i++)
        {
            PPTBuff[i] = GameObject.Instantiate(PlayerPoolTarget[i]);
        }
        for (int i = 0; i < PlayerPoolTarget.Length; i++)
        {
            TPTBuff[i] = GameObject.Instantiate(TrashPoolTarget[i]);
        }

        poolPlayer.addItems(PPTBuff);
        poolTrash.addItems(TPTBuff);

        for (int i = 0; i < poolTrash.allItems().Length; i++)
        {
            poolTrash.outside(i);
        }
        poolTrash.inside(0);
    }
}
