using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRubbish : MonoBehaviour
{
    public Transform[] rubbishPrefab;
    private Transform[] rubbishInstance;
    private Vector3[] avatarBuff;
    public SpawnPlayer spawnPlayer;
    public Scroll globalSpeed;
    public float speed;
    public float spownRot;
    public float rotationRubbish;
    public float distanceRubbish;

    // Start is called before the first frame update
    void Start()
    {
        rubbishInstance = new Transform[rubbishPrefab.Length];
        avatarBuff = new Vector3[rubbishPrefab.Length];

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, spownRot);

        for (int i = 0; i < rubbishPrefab.Length; i++)
        {
            rubbishInstance[i] = Instantiate(rubbishPrefab[i]);

            setStartPos(i);

        }
        StartCoroutine("StartLater");
    }

    void setStartPos(int i)
    {
        float r = distanceRubbish * i;
        float d = rotationRubbish * Mathf.Deg2Rad;

        rubbishInstance[i].position = transform.position + new Vector3(
        r * Mathf.Cos(d) - r * Mathf.Sin(d),
        r * Mathf.Sin(d) + r * Mathf.Cos(d), 0.0f);
    }
    IEnumerator StartLater()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < spawnPlayer.GetComponent<SpawnPlayer>().getAvatarsInstances().Length; i++)
        {
            avatarBuff[i] = spawnPlayer.GetComponent<SpawnPlayer>().getAvatarsInstances()[i].position;
        }
        StopCoroutine("StartLater");
    }
    void Update()
    {
        speed = globalSpeed.speed;
        for (int i = 0; i < rubbishInstance.Length; i++)
        {
            if (rubbishInstance[i].gameObject.activeInHierarchy & rubbishInstance[i].GetComponent<Trash>().able)
            {
                StartCoroutine(moveToTarget(i));
            }
        }
        Debug.Log(speed);
    }

    void radomizeTarget(int old)
    {
        int randIndex = Random.Range(0, rubbishInstance.Length);

        Vector3 rubbishBuffer = rubbishInstance[old].transform.position;
        Vector3 avBuff = avatarBuff[old];

        rubbishInstance[old].transform.position = rubbishInstance[randIndex].transform.position;
        avatarBuff[old] = avatarBuff[randIndex];

        rubbishInstance[randIndex].transform.position = transform.position + rubbishBuffer;
        avatarBuff[randIndex] = avBuff;
    }
    private void activeRubbishPool()
    {
        int i = Random.Range(0, rubbishInstance.Length);
        rubbishInstance[i].gameObject.SetActive(true);
    }
    IEnumerator moveToTarget(int i)
    {
        rubbishInstance[i].GetComponent<Trash>().able = false;
        while (Vector3.Distance(rubbishInstance[i].transform.position, avatarBuff[i]) > 0.01f)
        {
            float t = (this.speed * Time.deltaTime *0.25f) / Vector3.Distance(rubbishInstance[i].transform.position, avatarBuff[i]);
            rubbishInstance[i].transform.position = Vector3.Lerp(rubbishInstance[i].transform.position,
            avatarBuff[i],
            t);
            yield return null;
        }
        rubbishInstance[i].GetComponent<Trash>().able = true;

        activeRubbishPool();
        radomizeTarget(i);
    }
}