﻿using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public delegate void ElapsedEventHandler(object sender, ElapsedEventArgs e);
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
    private Timer aTimer;
    private int randSelect = 0;
    private string nameAnim = "runing";
    void Start()
    {
        aTimer = new Timer(32000);

        aTimer.Elapsed += OnTimedEvent;

        aTimer.AutoReset = true;

        aTimer.Enabled = true;

        size = PoolControll.poolPlayer.allItems().Length;
        animator = new Animator[size];
        buffPosition = new Vector3[size];
        moviment = new IEnumerator[size];

        setStartPos();

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, spawnRot);
    }
    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        try
        {
            foreach (Animator item in animator)
            {
                if (nameAnim == "runing")
                    (PoolControll.poolPlayer.getItem(randSelect) as Player).playIdle();
            }
        }
        catch (System.Exception error)
        {
            Debug.Log(error);
            throw;
        }
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
                nameAnim = "";
                item.SetBool("runing", false);
            }
            swapPosition();
            for (int i = 0; i < size; i++)
            {
                moviment[i] = moveToTarget(i);
                StartCoroutine(moviment[i]);
            }
        }
        randSelect = Random.Range(0, size);
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
        animator[i].SetBool("runing", true);
        nameAnim = "runing";
        StopCoroutine(moviment[i]);
    }
}
