using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRubbish : MonoBehaviour
{
    private Vector3[] avatarBuff;
    public SpawnPlayer spawnPlayer;
    private float speed = SpeedControl.speed;
    public float spownRot;
    public float rotationRubbish;
    public float distanceRubbish;
    private int size;
    private int old = 0;
    private IEnumerator co;

    void Start()
    {
        size = (PoolControll.poolTrash as PoolTrash).Size;
        avatarBuff = new Vector3[size];

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, spownRot);

        StartCoroutine("StartLater");
    }
    IEnumerator StartLater()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < size; i++)
        {
            avatarBuff[i] = PoolControll.poolPlayer.getItem(i).transform.position;
        }
        StopCoroutine("StartLater");
    }
    private void setStartPos(int i)
    {
        float r = distanceRubbish * i;
        float d = rotationRubbish * Mathf.Deg2Rad;

        PoolControll.poolTrash.getItem(i).transform.position = transform.position + new Vector3(
        r * Mathf.Cos(d) - r * Mathf.Sin(d),
        r * Mathf.Sin(d) + r * Mathf.Cos(d), 0.0f);
    }
    void Update()
    {

        speed = SpeedControl.speed;

        for (int i = 0; i < size; i++)
        {
            if (PoolControll.poolTrash.getItem(i).Able)
            {
                PoolControll.poolTrash.outside(i);
                co = moveToTarget(i);
                StartCoroutine(co);
                old = i;
            }
        }
        if (PoolControll.poolTrash.getItem(old).Active)
        {
            PoolControll.poolTrash.getItem(old).Active = false;
            int neo = Random.Range(0, size);
            StopCoroutine(co);

            PoolControll.poolTrash.inside(neo);
            radomizeTarget(old);
        }
    }
    IEnumerator moveToTarget(int i)
    {
        ItemPool obj = PoolControll.poolTrash.getItem(i);
        while (!obj.Able)
        {
            float t = (this.speed * Time.deltaTime * 0.25f) / Vector3.Distance(obj.transform.position, avatarBuff[i]);
            obj.transform.position = Vector3.Lerp(PoolControll.poolTrash.getItem(i).transform.position, avatarBuff[i], t);
            yield return null;
        }

    }
    void radomizeTarget(int old)
    {
        int randIndex = Random.Range(0, size);

        Vector3 rubbishBuffer = PoolControll.poolTrash.getItem(old).transform.position;
        Vector3 avBuff = avatarBuff[old];

        PoolControll.poolTrash.getItem(old).gameObject.SetActive(false);

        PoolControll.poolTrash.getItem(old).transform.position = PoolControll.poolTrash.getItem(randIndex).transform.position;
        avatarBuff[old] = avatarBuff[randIndex];

        PoolControll.poolTrash.getItem(randIndex).transform.position = transform.position + rubbishBuffer;
        avatarBuff[randIndex] = avBuff;
    }
}