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

    void Start()
    {
        size = (PoolControll.trashPool as PoolTrash).Size;
        avatarBuff = new Vector3[size];

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, spownRot);

        for (int i = 0; i < size; i++)
        {
            setStartPos(i);
        }
        StartCoroutine("StartLater");
    }
    IEnumerator StartLater()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < size; i++)
        {
            avatarBuff[i] = PoolControll.playerPool.getItem(i).transform.position;
        }
        StopCoroutine("StartLater");
    }
    private void setStartPos(int i)
    {
        float r = distanceRubbish * i;
        float d = rotationRubbish * Mathf.Deg2Rad;

        PoolControll.trashPool.getItem(i).transform.position = transform.position + new Vector3(
        r * Mathf.Cos(d) - r * Mathf.Sin(d),
        r * Mathf.Sin(d) + r * Mathf.Cos(d), 0.0f);
    }
    void Update()
    {
        speed = SpeedControl.speed;
        for (int i = 0; i < size; i++)
        {
            if (PoolControll.trashPool.getItem(i).Able)
            {
                PoolControll.trashPool.desable(i);
                StartCoroutine(moveToTarget(i));
            }
        }
    }
    IEnumerator moveToTarget(int i)
    {
        while (!PoolControll.trashPool.getItem(i).Able)
        {
            float t = (this.speed * Time.deltaTime * 0.25f) / Vector3.Distance(PoolControll.trashPool.getItem(i).transform.position, avatarBuff[i]);
            PoolControll.trashPool.getItem(i).transform.position = Vector3.Lerp(PoolControll.trashPool.getItem(i).transform.position, avatarBuff[i], t);
            if(Vector3.Distance(PoolControll.trashPool.getItem(i).transform.position, avatarBuff[i]) > 0.01f){

            }
            yield return null;
        }
        //nunca chama essa parte
        radomizeTarget(i);
        PoolControll.trashPool.enable(Random.Range(0, size));

    }
    void radomizeTarget(int old)
    {
        int randIndex = Random.Range(0, size);

        Vector3 rubbishBuffer = PoolControll.trashPool.getItem(old).transform.position;
        Vector3 avBuff = avatarBuff[old];

        PoolControll.trashPool.getItem(old).transform.position = PoolControll.trashPool.getItem(randIndex).transform.position;
        avatarBuff[old] = avatarBuff[randIndex];

        Debug.Log(transform.position);
        Debug.Log(rubbishBuffer);
        PoolControll.trashPool.getItem(randIndex).transform.position = transform.position + rubbishBuffer;
        avatarBuff[randIndex] = avBuff;
    }
}