using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] streets;
    public float speed;
    private Renderer back;
    private float vel;
    private string nomeBack;

    private void Start()
    {
        speed = 2;
        back = GetComponent<Renderer>();
        vel = 0.0f;
        nomeBack = tag;
    }
    // Update is called once per frame
    void Update()
    {
        switch (nomeBack)
        {
            case "background":
                vel = speed * 0.02f;
                break;
            case "foreground":
                vel = speed * 0.01f;
                break;
            case "street":
                vel = speed * 0.025f;
                break;
        }
        Vector2 offset = new Vector2(vel * Time.deltaTime, 0);
        back.material.mainTextureOffset += offset;
    }
}
