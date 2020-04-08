using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ItemPool
{
    public TrashType combine;
    private GameObject lifeBar;
    private GameObject pointsValue;
    private float lerpSpeed;
    private Animator animator;
    public bool bottomLane = false;

    private IEnumerator p;
    private IEnumerator l;
    private IEnumerator c;
    private void Start()
    {
        lerpSpeed = SpeedControl.speed;
        lifeBar = GameObject.Find("Canvas/lifeBar/life");
        pointsValue = GameObject.Find("Canvas/pointBar/value");
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (combine == other.GetComponent<Trash>().type)
        {
            animator.SetInteger("loopState", 2);
            pointsValue.GetComponent<Points>().addPoints();
        }
        else
        {
            //p = PlayerNotificationCollider();
            l = lifeBar.GetComponent<LifeControll>().lifeBarNotificationCollider();
            c = lifeBar.GetComponent<LifeControll>().changeSizeBar();

            animator.SetInteger("loopState", 1);

            //StartCoroutine(p);
            StartCoroutine(l);
            StartCoroutine(c);
        }
        animator.SetInteger("loopState", 0);
    }
    private IEnumerator PlayerNotificationCollider()
    {
        Color col = GetComponentInChildren<Renderer>().material.color;

        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.05f);

        GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);

        GetComponent<Renderer>().material.color = col;

        StopCoroutine(p);
        StopCoroutine(l);
        StopCoroutine(c);

        animator.SetInteger("loopState", 0);
    }
}
