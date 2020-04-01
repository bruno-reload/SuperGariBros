using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ItemPool
{
    public TrashType combine;
    private GameObject lifeBar;
    private GameObject pointsValue;
    private float lerpSpeed;
    public bool bottomLane = false;

    private IEnumerator p;
    private IEnumerator l;
    private IEnumerator c;
    private void Start()
    {
        lerpSpeed = SpeedControl.speed;
        lifeBar = GameObject.Find("Canvas/lifeBar/life");
        pointsValue = GameObject.Find("Canvas/pointBar/value");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (combine == other.GetComponent<Trash>().type)
        {
            pointsValue.GetComponent<Points>().addPoints();
        }
        else
        {
            p = PlayerNotificationCollider();
            l = lifeBar.GetComponent<LifeControll>().lifeBarNotificationCollider();
            c = lifeBar.GetComponent<LifeControll>().changeSizeBar();

            StartCoroutine(p);
            StartCoroutine(l);
            StartCoroutine(c);
        }
    }
    private IEnumerator PlayerNotificationCollider()
    {
        Color col = GetComponent<Renderer>().material.color;

        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.05f);

        GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);

        GetComponent<Renderer>().material.color = col;
        
        StopCoroutine(p);
        StopCoroutine(l);
        StopCoroutine(c);
    }
}
