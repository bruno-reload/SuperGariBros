using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] character;
    Animator animator;
    private float time;
    private Transform[] characterInstances;
    void Start()
    {
        time = 0.0f;
        characterInstances = new Transform[character.Length];

        for(int i = 0; i < character.Length; i++){
            characterInstances[i] = Instantiate(character[i]);
            characterInstances[i].parent = transform;
            characterInstances[i].position += transform.position;
        }
        animator = characterInstances[0].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.DownArrow)){
            changeCharacter();
        }
    }
    void changeCharacter(){
        Transform last = character[0];
        time += Time.deltaTime;

        character[0] = character[1];
        character[1] = character[2];
        character[2] = last;


        for (int i = 0; i < character.Length; i++){
            characterInstances[i].position = character[i].position + transform.position; 
        }
        animator.SetTrigger("go_to_change");
    }
}
