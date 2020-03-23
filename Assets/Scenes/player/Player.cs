using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TrashType combine;
    private float lerpSpeed = SpeedControl.speed;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (combine == other.GetComponent<Trash>().type)
        {
            point();
        }
        else
        {
            StartCoroutine("colliderBar");
        }
    }
    IEnumerator colliderBar()
    {
        Color col = GetComponent<Renderer>().material.color;
        Color RawImagecol = GameObject.Find("Canvas/lifeBar/life").GetComponent<UnityEngine.UI.RawImage>().color;

        float x = GameObject.Find("Canvas/lifeBar/life").GetComponent<RectTransform>().localScale.x;
        float newX = x - 0.05f;
        
        GameObject.Find("Canvas/lifeBar/life").GetComponent<UnityEngine.UI.RawImage>().color = Color.red;
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.05f);

        GameObject.Find("Canvas/lifeBar/life").GetComponent<UnityEngine.UI.RawImage>().color = Color.white;
        GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);

        GameObject.Find("Canvas/lifeBar/life").GetComponent<UnityEngine.UI.RawImage>().color = RawImagecol;
        GetComponent<Renderer>().material.color = col;

        while (newX < x & x > 0)
        {
            float distance = lerpSpeed / x;
            x = Mathf.Lerp(x, x - 0.05f, 0.5f);
            (GameObject.Find("Canvas/lifeBar/life").GetComponent<RectTransform>() as RectTransform).localScale = new Vector2(x, 1.0f);
            yield return null;
        }

        if (x < 0.05f)
        {
            Debug.Log("you die");
        }
    }
    void point()
    {
        UnityEngine.UI.Text textArea = GameObject.Find("Canvas/pointBar/value").GetComponent<UnityEngine.UI.Text>();
        int val = int.Parse(textArea.text);
        val++;
        textArea.text = val.ToString();
        StartCoroutine("scaleOut");
    }

    IEnumerator scaleOut()
    {
        GameObject value = GameObject.Find("Canvas/pointBar/value");

        float x = value.GetComponent<RectTransform>().localScale.x;
        float y = value.GetComponent<RectTransform>().localScale.y;

        Vector2 scaleDefault = new Vector2(x, y);
        Vector2 bigScale = new Vector2(1.3f, 1.3f);
        float distance = Vector2.Distance(bigScale, scaleDefault) / x;

        value.GetComponent<RectTransform>().localScale = bigScale;

        while (Vector2.Distance(bigScale, scaleDefault) > 0.01f)
        {
            value.GetComponent<RectTransform>().localScale = Vector2.Lerp(value.GetComponent<RectTransform>().localScale, scaleDefault, Time.deltaTime * distance);
            yield return null;
        }
        StopCoroutine("scaleOut");
    }
}
