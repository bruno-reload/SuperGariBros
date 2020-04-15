using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ItemPool
{

    [SerializeField]
    public Sound[] sounds;
    public TrashType combine;
    private GameObject lifeBar;
    private GameObject pointsValue;
    private float lerpSpeed;
    private Animator animator;
    public bool bottomLane = false;
    private AudioSource audioSource;
    private IEnumerator l;
    private IEnumerator c;
    private IEnumerator w;

    private float startTime = 0;
    private float clipTime = 0;
    private void Start()
    {

        lerpSpeed = SpeedControl.speed;
        lifeBar = GameObject.Find("Canvas/lifeBar/life");
        pointsValue = GameObject.Find("Canvas/pointBar/value");
        animator = GetComponentInChildren<Animator>();

        gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].setSource(audioSource);
            if (sounds[i].name == "walk") sounds[i].play();
        }
    }
    private void Update()
    {
        if ((Time.time - startTime) >= clipTime && !audioSource.isPlaying)
        {
            foreach (Sound item in sounds)
            {
                if (item.name == "walk") item.play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startTime = Time.time;
        if (combine == other.GetComponent<Trash>().type)
        {
            foreach (Sound item in sounds)
            {
                if (item.name == "collect") clipTime = item.oneShotPlay();
            }
            animator.SetTrigger("collect");

            pointsValue.GetComponent<Points>().addPoints();

        }
        else
        {
            foreach (Sound item in sounds)
            {
                if (item.name == "collision") clipTime = item.oneShotPlay();
            }
            animator.SetTrigger("collision");


            l = lifeBar.GetComponent<LifeControll>().lifeBarNotificationCollider();
            c = lifeBar.GetComponent<LifeControll>().changeSizeBar();

            StartCoroutine(l);
            StartCoroutine(c);
        }

    }
}

