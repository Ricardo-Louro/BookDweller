using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform anchor;

    private Vector3 _newPos;

    private void Start()
    {
        
        _newPos = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (anchor is null) return;
     
        Vector3 anchorPosition = anchor.position;
        _newPos.x = anchorPosition.x;
        _newPos.y = anchorPosition.y;
        
        transform.position = _newPos;
    }
}
