using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform[] avatarsInstances;
    public Transform[] avatarsPrefabs;
    private Vector3 highlightPosition;
    private Animator animator;
    public float speed;
    public float rot;
    public float rotationAvatars;
    public float distanceAvatars;
    void Start()
    {
        avatarsInstances = new Transform[avatarsPrefabs.Length];

        for (int i = 0; i < avatarsPrefabs.Length; i++)
        {
            avatarsInstances[i] = Instantiate(avatarsPrefabs[i]);

            float r = distanceAvatars * i;
            float d = rotationAvatars * Mathf.Deg2Rad;

            avatarsInstances[i].position = transform.position + new Vector3(
            r * Mathf.Cos(d) - r * Mathf.Sin(d),
            r * Mathf.Sin(d) + r * Mathf.Cos(d), 0.0f);

            avatarsPrefabs[i].localPosition = avatarsInstances[i].localPosition;
            avatarsPrefabs[i].localRotation = avatarsInstances[i].localRotation;
        }
        animator = avatarsInstances[0].GetComponent<Animator>();
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rot);
    }
    public Transform[] getAvatarsInstances()
    {
        return this.avatarsInstances;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            updatePosition();
            for (int i = 0; i < avatarsPrefabs.Length; i++)
            {
                StartCoroutine(tuor(i));
            }
            // animator.SetTrigger("go_to_change");
        }
    }
    void updatePosition()
    {
        Transform last = avatarsPrefabs[0];
        avatarsPrefabs[0] = avatarsPrefabs[1];
        avatarsPrefabs[1] = avatarsPrefabs[2];
        avatarsPrefabs[2] = last;
    }
    IEnumerator tuor(int i)
    {
        while (Vector3.Distance(avatarsInstances[i].localPosition, avatarsPrefabs[i].localPosition) > 0.01f)
        {
            // if(i == avatarsPrefabs.Length - 1){
            //     avatarsInstances[i].localPosition = Vector3.Lerp(avatarsInstances[i].localPosition,
            //     avatarsPrefabs[i].localPosition + new Vector3(10.0f,10.0f,0.0f), speed*Time.deltaTime);
            // }
            avatarsInstances[i].localPosition = Vector3.Lerp(avatarsInstances[i].localPosition,
            avatarsPrefabs[i].localPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
