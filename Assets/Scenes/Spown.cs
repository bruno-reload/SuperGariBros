using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spown : MonoBehaviour
{
    public Transform[] rubbishPrefab;
    private Transform[] rubbishInstance;
    private Vector3[] avatarBuff;
    public Player player;
    public float timeLerped;
    public float spownRot;
    public float distanceRubbish;
    public float rotationRubbish;

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

            rubbishPrefab[i].localPosition = rubbishInstance[i].localPosition;
            rubbishPrefab[i].localRotation = rubbishInstance[i].localRotation;

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
        for (int i = 0; i < player.GetComponent<Player>().getAvatarsInstances().Length; i++)
        {
            avatarBuff[i] = player.GetComponent<Player>().getAvatarsInstances()[i].position;
        }
    }
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     for (int i = 0; i < rubbishInstance.Length; i++)
        //     {
        //         insertTrash();
        //         if (rubbishInstance[i].gameObject.activeInHierarchy)
        //         {
        //             StartCoroutine(moveToTarget(i));
        //         }
        //         else
        //         {
        //             StopCoroutine(moveToTarget(i));
        //         }

        //     }
        // }

        for (int i = 0; i < rubbishInstance.Length; i++)
        {
            if (rubbishInstance[i].gameObject.activeInHierarchy & rubbishInstance[i].GetComponent<Trash>().able)
            {
                StartCoroutine(moveToTarget(i));
            }
        }

    }

    private void getTrashOnPool()
    {
        int i = Random.Range(0, rubbishInstance.Length);
        rubbishInstance[i].gameObject.SetActive(true);
    }
    IEnumerator moveToTarget(int i)
    {
        rubbishInstance[i].GetComponent<Trash>().able = false;
        while (Vector3.Distance(rubbishInstance[i].transform.position, avatarBuff[i]) > 0.01f)
        {
            float speed = (timeLerped * Time.deltaTime) / Vector3.Distance(rubbishInstance[i].transform.position, avatarBuff[i]);
            rubbishInstance[i].transform.position = Vector3.Lerp(rubbishInstance[i].transform.position,
            avatarBuff[i],
            speed);
            yield return null;
        }
        setStartPos(i);
        rubbishInstance[i].GetComponent<Trash>().able = true;
        getTrashOnPool();
    }
}