using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    private float count;
    private AudioSource audioSource;

    private void Start()
    {
        count = 0.0f;
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        count += Time.deltaTime * 0.1f;

        audioSource.volume = Mathf.Lerp(0.0f, 1.0f,count);

    }
}
