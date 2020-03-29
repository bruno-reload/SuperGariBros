using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
    private Transform[] avatarsInstances;
    public Transform[] avatarsPrefabs;
    private Vector3 highlightPosition;
    public float foward;
    private Animator animator;
    private bool inAnimation = false;
    private float speed = SpeedControl.speed;
    public float spawnRot;
    public float rotationAvatars;
    public float distanceAvatars;
    void Start () {
        avatarsInstances = new Transform[avatarsPrefabs.Length];

        for (int i = 0; i < avatarsPrefabs.Length; i++) {
            avatarsInstances[i] = Instantiate (avatarsPrefabs[i]);

            setStartPos (i);

            avatarsPrefabs[i].localPosition = avatarsInstances[i].localPosition;
            avatarsPrefabs[i].localRotation = avatarsInstances[i].localRotation;
        }
        animator = avatarsInstances[0].GetComponent<Animator> ();
        transform.localRotation = Quaternion.Euler (0.0f, 0.0f, spawnRot);
    }
    private void setStartPos (int i) {
        float r = distanceAvatars * i;
        float d = rotationAvatars * Mathf.Deg2Rad;

        avatarsInstances[i].position = transform.position + new Vector3 (
            r * Mathf.Cos (d) - r * Mathf.Sin (d),
            r * Mathf.Sin (d) + r * Mathf.Cos (d), 0.0f);
    }
    public Transform[] getAvatarsInstances () {
        return this.avatarsInstances;
    }
    void Update () {
        if (Input.GetKeyDown (KeyCode.DownArrow) && !inAnimation) {
            swapPosition ();
            for (int i = 0; i < avatarsPrefabs.Length; i++) {
                StartCoroutine (moveToTarget (i));
            }
            // animator.SetTrigger("go_to_change");

        }
    }
    private void swapPosition () {
        Transform last = avatarsPrefabs[0];
        for (int i = 0; i < avatarsPrefabs.Length - 1; i++) {
            avatarsPrefabs[i] = avatarsPrefabs[i + 1];
            avatarsInstances[i].GetComponent<Player> ().bottomLane = avatarsPrefabs[i].GetComponent<Player> ().bottomLane;
        }
        avatarsPrefabs[avatarsPrefabs.Length - 1] = last;
        avatarsInstances[avatarsInstances.Length - 1].GetComponent<Player> ().bottomLane = avatarsPrefabs[avatarsInstances.Length - 1].GetComponent<Player> ().bottomLane;
    }
    private IEnumerator moveToTarget (int i) {
        inAnimation = true;
        if (avatarsInstances[i].GetComponent<Player> ().bottomLane) {
            float x = avatarsPrefabs[i].localPosition.x;
            float y = avatarsPrefabs[i].localPosition.y;

            Vector3 buff = new Vector3 (x * foward, y * foward, 0.0f);

            while (Vector3.Distance (avatarsInstances[i].localPosition, buff) > 0.05f) {
                avatarsInstances[i].localPosition = Vector3.Lerp (
                    avatarsInstances[i].localPosition,
                    buff,
                    speed * Time.deltaTime * 2);
                yield return null;
            }
        }
        while (Vector3.Distance (avatarsInstances[i].localPosition, avatarsPrefabs[i].localPosition) > 0.05f) {
            avatarsInstances[i].localPosition = Vector3.Lerp (
                avatarsInstances[i].localPosition,
                avatarsPrefabs[i].localPosition,
                speed * Time.deltaTime);
            yield return null;
        }
        if (i == avatarsInstances.Length - 1) {

            inAnimation = false;
        }
        StopCoroutine (moveToTarget (i));
    }
}