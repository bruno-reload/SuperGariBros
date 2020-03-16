using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    private Transform[] avatarsInstances;
    public Transform[] avatarsPrefabs;
    private Vector3 highlightPosition;
    private Animator animator;
    public float speed;
    public float rot;
    public float rotationAvatars;
    public float distanceAvatars;
    private float time;
    void Start(){   
        time = 0.0f;

        avatarsInstances = new Transform[avatarsPrefabs.Length];

        for(int i = 0; i < avatarsPrefabs.Length; i++){
            avatarsInstances[i] = Instantiate(avatarsPrefabs[i]);
            avatarsInstances[i].parent = transform;

            avatarsInstances[i].localPosition = new Vector3( i * distanceAvatars, 0.0f,0.0f);
            avatarsInstances[i].localRotation = Quaternion.Euler(0.0f,0.0f,rotationAvatars);

            avatarsPrefabs[i].localPosition = avatarsInstances[i].localPosition;
             avatarsPrefabs[i].localRotation = avatarsInstances[i].localRotation;
        }
        animator = avatarsInstances[0].GetComponent<Animator>();
        transform.localRotation = Quaternion.Euler(0.0f,0.0f,rot);
    }

    void Update(){ 
        if( Input.GetKeyDown(KeyCode.DownArrow)){
            updatePosition(); 
        
        for (int i = 0; i < avatarsPrefabs.Length; i++){
            StartCoroutine(tuor(i));
        }
            // animator.SetTrigger("go_to_change");
        } 
    }
    void updatePosition(){
        Transform last = avatarsPrefabs[0];
        avatarsPrefabs[0] = avatarsPrefabs[1];
        avatarsPrefabs[1] = avatarsPrefabs[2];
        avatarsPrefabs[2] = last;
    }
    IEnumerator tuor(int i){
        float x = avatarsInstances[i].localPosition.x;
        float y = avatarsInstances[i].localPosition.y;
        while(Vector3.Distance(avatarsInstances[i].localPosition, avatarsPrefabs[i].localPosition) > 0.01f){
            // avatarsInstances[i].localPosition = new Vector3(x,y,0.0f);
            avatarsInstances[i].localPosition = Vector3.Lerp(avatarsInstances[i].localPosition,
            avatarsPrefabs[i].localPosition, speed*Time.deltaTime);
            yield return null;
        }
    }
}
