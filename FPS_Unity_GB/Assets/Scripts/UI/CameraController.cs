﻿using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public GameObject CanvasPanel;

    private Vector3 _finalPosition = new Vector3(0.3f, 7.5f, 0);
    private Vector3 _finalRotation = new Vector3(90, 90, 0);


    private void Awake()
    {
        CanvasPanel.SetActive(false);
        DOTween.SetTweensCapacity(1250, 50);
    }

    private void Update()
    {
        transform.DOMove(_finalPosition, 2);
        transform.DORotate(_finalRotation, 2);

        if (transform.position.x >= _finalPosition.x - 0.01f)
        {
            CanvasPanel.SetActive(true);
        }
    }
}
