using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
    private AudioSource audioSource;
    public AudioClip audioCollider;
    public AudioClip audioCollect;
    private void Start()
    {
        lerpSpeed = SpeedControl.speed;
        lifeBar = GameObject.Find("Canvas/lifeBar/life");
        pointsValue = GameObject.Find("Canvas/pointBar/value");
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(audioCollect,5);
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
            
            audioSource.PlayOneShot(audioCollider,5);
        }
    }
}
