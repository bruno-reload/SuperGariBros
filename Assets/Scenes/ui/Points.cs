using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public void addPoints()
    {
        UnityEngine.UI.Text textArea = GameObject.Find("Canvas/pointBar/value").GetComponent<UnityEngine.UI.Text>();
        int val = int.Parse(textArea.text);
        val++;
        textArea.text = val.ToString();
        StartCoroutine("pointsVisualNotification");
    }

    private IEnumerator pointsVisualNotification()
    {
        float x = GetComponent<RectTransform>().localScale.x;
        float y = GetComponent<RectTransform>().localScale.y;

        Vector2 scaleDefault = new Vector2(x, y);
        Vector2 bigScale = new Vector2(1.3f, 1.3f);
        float distance = Vector2.Distance(bigScale, scaleDefault) / x;

        GetComponent<RectTransform>().localScale = bigScale;

        while (Vector2.Distance(bigScale, scaleDefault) > 0.01f)
        {
            GetComponent<RectTransform>().localScale = Vector2.Lerp(GetComponent<RectTransform>().localScale, scaleDefault, Time.deltaTime * distance);
            yield return null;
        }
        StopCoroutine("pointsVisualNotification");
    }
}
