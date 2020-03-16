using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spown : MonoBehaviour{
    public Transform[] rubbishPrefab;
    private Transform[] rubbishInstance;
    private Transform[] avatarTransform;
    private Transform[] avatarBuff;
    public Player player;
    public float timeLerped;
    public float spownRot;
    public float distanceRubbish;
    public float rotationRubbish;

    // Start is called before the first frame update
    void Start(){
        rubbishInstance = new Transform[rubbishPrefab.Length];
        avatarBuff = new Transform[rubbishPrefab.Length];

        for(int i = 0; i < rubbishPrefab.Length; i++){
            rubbishInstance[i] = Instantiate(rubbishPrefab[i]);
            rubbishInstance[i].parent = transform;

            rubbishInstance[i].localPosition = new Vector3( i * distanceRubbish, 0.0f,0.0f);
            rubbishInstance[i].localRotation = Quaternion.Euler(0.0f,0.0f,rotationRubbish);

            rubbishPrefab[i].localPosition = rubbishInstance[i].localPosition;
            rubbishPrefab[i].localRotation = rubbishInstance[i].localRotation;
            avatarBuff[i] = new GameObject().transform;
        }
        transform.localRotation = Quaternion.Euler(0.0f,0.0f,spownRot);
        StartCoroutine("StartLater");
    }

    // Update is called once per frame
    IEnumerator StartLater(){
        yield return new WaitForEndOfFrame();
        avatarTransform = new Transform[player.transform.childCount];
        for(int i = 0; i < player.transform.childCount; i++){
            avatarTransform[i] = player.transform.GetChild(i).GetComponent<Transform>();
            avatarBuff[i].transform.SetPositionAndRotation(avatarTransform[i].position,avatarTransform[i].rotation);
         }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            for (int i = 0; i < rubbishInstance.Length; i++){
                StartCoroutine(moveToTarget(i));
            }
        }
    }

    IEnumerator moveToTarget(int i){ 
        while(Vector3.Distance(rubbishInstance[i].transform.position,avatarBuff[i].transform.position) > 0.0f){
            float speed = timeLerped/Vector3.Distance(rubbishInstance[i].transform.position,avatarBuff[i].transform.position);
            Debug.Log(speed);
            rubbishInstance[i].transform.position = Vector3.Lerp(rubbishInstance[i].transform.position,
            avatarBuff[i].transform.position,
            speed * Time.deltaTime);
            yield return null;
        }
    }
}