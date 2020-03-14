using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer back;
    private float vel;
    private string nomeBack;

    private void Start() {
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
                vel = 0.04f;
                break;
            case "foreground":
                vel = 0.02f;
                break;
            case "street":
                vel = 0.05f;
                break;
        }   
        Vector2 offset = new Vector2(vel * Time.deltaTime, 0);
        back.material.mainTextureOffset += offset;
    }
}
