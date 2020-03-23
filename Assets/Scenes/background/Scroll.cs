using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scroll : MonoBehaviour
{
    private float speed;
    private Renderer back;
    private string nomeBack;

    public float Speed
    {
        set
        {
            speed = value;
            switch (nomeBack)
            {
                case "background":
                    speed = speed * 0.002f;
                    break;
                case "foreground":
                    speed = speed * 0.001f;
                    break;
                case "street":
                    speed = speed * 0.025f;
                    break;
            }
        }
        get { return speed; }
    }
    private void Start()
    {
        speed = 2;
        back = GetComponent<Renderer>();
        speed = SpeedControl.speed;
        nomeBack = tag;
    }
    // Update is called once per frame
    void Update()
    {
        Speed = SpeedControl.speed;
        Vector2 offset = new Vector2(Speed * Time.deltaTime, 0);
        back.material.mainTextureOffset += offset;
    }
}
