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
            animator.ResetTrigger("run");
            animator.ResetTrigger("change");
            animator.ResetTrigger("idle");
            animator.ResetTrigger("collision");
            animator.SetTrigger("collect");

            pointsValue.GetComponent<Points>().addPoints();
        }
        else
        {
            animator.ResetTrigger("collect");
            animator.ResetTrigger("run");
            animator.ResetTrigger("change");
            animator.ResetTrigger("idle");
            animator.SetTrigger("collision");
            
            l = lifeBar.GetComponent<LifeControll>().lifeBarNotificationCollider();
            c = lifeBar.GetComponent<LifeControll>().changeSizeBar();

            StartCoroutine(l);
            StartCoroutine(c);
        }
    }
}
