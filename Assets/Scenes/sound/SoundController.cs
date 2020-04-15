using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    private float count;
    private AudioSource audioSource;
    [Range(0f, 1f)]
    public float volume = 1f;

    private void Start()
    {
        count = 0.0f;
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        count += Time.deltaTime * 0.1f;

        audioSource.volume = Mathf.Lerp(0.0f, volume, count);

    }
}
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.5f, 3f)]
    public float pitch = 1f;
    private AudioSource source;

    public void setSource(AudioSource source)
    {
        this.source = source;
        source.clip = clip;
    }
    public float play()
    {
        this.source.pitch = pitch;
        this.source.volume = volume;
        this.source.Play();
        return clip.length;
    }
    public float oneShotPlay()
    {
        this.source.pitch = pitch;
        this.source.volume = volume;
        this.source.Stop();
        this.source.PlayOneShot(clip);
        return clip.length;
    }

}