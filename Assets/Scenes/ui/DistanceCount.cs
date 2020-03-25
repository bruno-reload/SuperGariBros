using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class DistanceCount : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = ((SpeedControl.speed  - 5) * 10).ToString("F");
    }
}
