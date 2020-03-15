using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] avatarsPrefabs;
    Animator animator;
    private Transform[] avatarsInstances;
    void Start()
    {
        avatarsInstances = new Transform[avatarsPrefabs.Length];
        for(int i = 0; i < avatarsPrefabs.Length; i++){
            avatarsInstances[i] = Instantiate(avatarsPrefabs[i]);
            avatarsInstances[i].parent = transform;
            avatarsInstances[i].position += transform.position;
        }
        animator = avatarsInstances[0].GetComponent<Animator>();
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.DownArrow)){
            updatePosition();
            for (int i = 0; i < avatarsPrefabs.Length; i++){
                tuor(i);
            }
        }
    }
    void updatePosition(){
        Transform last = avatarsPrefabs[0];
        avatarsPrefabs[0] = avatarsPrefabs[1];
        avatarsPrefabs[1] = avatarsPrefabs[2];
        avatarsPrefabs[2] = last;
        
    }

    void tuor(int i){
        avatarsInstances[i].position = avatarsPrefabs[i].position + transform.position; 
    }
}
