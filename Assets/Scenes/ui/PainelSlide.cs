﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PainelSlide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        transform.DOMove(new Vector3(0.0f,0.0f,0.0f),5);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
