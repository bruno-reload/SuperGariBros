using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControll : MonoBehaviour
{
    public ItemPool[] PlayerPoolTarget;
    public ItemPool[] TrashPoolTarget;

    public static Pool<ItemPool> playerPool;
    public static Pool<ItemPool> trashPool;

    private void Start()
    {
        ItemPool[] PPTBuff = new Player[PlayerPoolTarget.Length];
        ItemPool[] TPTBuff = new Trash[TrashPoolTarget.Length];

        playerPool = new PoolPlayer();
        trashPool = new PoolTrash();

        (playerPool as PoolPlayer).Size = PlayerPoolTarget.Length;
        (trashPool as PoolTrash).Size = TrashPoolTarget.Length;

        for (int i = 0; i < PlayerPoolTarget.Length; i++)
        {
            PPTBuff[i] = GameObject.Instantiate(PlayerPoolTarget[i]);
        }
        for (int i = 0; i < PlayerPoolTarget.Length; i++)
        {
            TPTBuff[i] = GameObject.Instantiate(TrashPoolTarget[i]);
        }

        playerPool.addItems(PPTBuff);
        trashPool.addItems(TPTBuff);

        trashPool.getItem(0).Able = true;
    }


}
