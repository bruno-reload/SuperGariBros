using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LifeControll : MonoBehaviour
{
    public float lerpSpeed;
    public Transform gameOver;
    public ButtonControll bt;
    public GameObject pause;

    public IEnumerator lifeBarNotificationCollider()
    {

        Color RawImagecol = GetComponent<UnityEngine.UI.RawImage>().color;

        GetComponent<UnityEngine.UI.RawImage>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        GetComponent<UnityEngine.UI.RawImage>().color = Color.white;
        yield return new WaitForSeconds(0.05f);
        GetComponent<UnityEngine.UI.RawImage>().color = Color.green;
    }
    public IEnumerator changeSizeBar()
    {
        float x = GetComponent<RectTransform>().localScale.x;
        float newX = x - 0.05f;
        while (newX < x & x > 0)
        {
            float distance = lerpSpeed / x;
            x = Mathf.Lerp(x, x - 0.05f, 0.5f);
            (GetComponent<RectTransform>() as RectTransform).localScale = new Vector2(x, 1.0f);
            yield return null;
        }
        if (x < 0.05f)
        {
            gameOver.DOMove(new Vector3(), 0.5f);
        }
        yield return null;
    }
    void Update()
    {
        if (DOTween.Complete(gameOver) == 1)
        {
            bt.pause();
            pause.SetActive(false);
        }
    }
}
