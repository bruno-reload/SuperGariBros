using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public float foward;
    private Animator[] animator;
    private bool inAnimation = false;
    private float speed = SpeedControl.speed;
    public float spawnRot;
    public float rotationAvatars;
    public float distanceAvatars;
    private int bottom = 0;
    private int size = 0;
    private IEnumerator[] moviment;
    private Vector3[] buffPosition;
    void Start()
    {

        size = PoolControll.poolPlayer.allItems().Length;
        animator = new Animator[size];
        buffPosition = new Vector3[size];
        moviment = new IEnumerator[size];

        setStartPos();

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, spawnRot);
    }
    private void setStartPos()
    {

        for (int i = 0; i < size; i++)
        {
            float r = distanceAvatars * i;
            float d = rotationAvatars * Mathf.Deg2Rad;

            PoolControll.poolPlayer.getItem(i).transform.position = transform.position + new Vector3(
                r * Mathf.Cos(d) - r * Mathf.Sin(d),
                r * Mathf.Sin(d) + r * Mathf.Cos(d), 0.0f);
            buffPosition[i] = PoolControll.poolPlayer.getItem(i).transform.position;
            animator[i] = PoolControll.poolPlayer.getItem(i).GetComponentInChildren<Animator>();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !inAnimation)
        {
            foreach (Animator item in animator)
            {
                item.ResetTrigger("collect");
                item.ResetTrigger("collision");
                item.ResetTrigger("idle");
                item.ResetTrigger("run");
                item.SetTrigger("change");
            }
            swapPosition();
            for (int i = 0; i < size; i++)
            {
                moviment[i] = moveToTarget(i);
                StartCoroutine(moviment[i]);
            }
        }
        foreach (Animator item in animator)
        {
            item.SetTrigger("run");
        }
    }
    private void swapPosition()
    {
        Vector3 last = buffPosition[0];
        bottom = (bottom + 2) % size;

        for (int i = 0; i < size - 1; i++)
        {
            buffPosition[i] = buffPosition[i + 1];
            PoolControll.poolPlayer.getItem(i).GetComponent<Player>().bottomLane = false;
        }
        PoolControll.poolPlayer.getItem(size - 1).GetComponent<Player>().bottomLane = false;
        PoolControll.poolPlayer.getItem(bottom).GetComponent<Player>().bottomLane = true;
        buffPosition[size - 1] = last;
    }
    private IEnumerator moveToTarget(int i)
    {
        yield return new WaitForEndOfFrame();
        inAnimation = true;
        if (PoolControll.poolPlayer.getItem(i).GetComponent<Player>().bottomLane)
        {
            float x = buffPosition[i].x;
            float y = buffPosition[i].y;

            Vector3 buff = new Vector3(x * foward, y * foward, 0.0f);


            while (Vector3.Distance(PoolControll.poolPlayer.getItem(i).transform.localPosition, buff) > 0.05f)
            {
                PoolControll.poolPlayer.getItem(i).transform.localPosition = Vector3.Lerp(
                    PoolControll.poolPlayer.getItem(i).transform.localPosition,
                    buff,
                    speed * Time.deltaTime * 2);
                yield return null;
            }
        }
        while (Vector3.Distance(PoolControll.poolPlayer.getItem(i).transform.localPosition, buffPosition[i]) > 0.05f)
        {
            PoolControll.poolPlayer.getItem(i).transform.localPosition = Vector3.Lerp(
                PoolControll.poolPlayer.getItem(i).transform.localPosition,
                buffPosition[i],
                speed * Time.deltaTime);
            yield return null;
        }
        if (i == size - 1)
        {
            inAnimation = false;
        }
        animator[i].SetTrigger("run");
        StopCoroutine(moviment[i]);
    }
}
