using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLine : MonoBehaviour
{
    // скрипт отвечающий за линии у прицела
    private LineRenderer _lineRenderer;
    public Vector3 dotOne;
    public Transform dotTwo;

    private void Start() 
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2; // сколько точек у линиии
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, dotOne); // первая точка и вторая
        _lineRenderer.SetPosition(1, dotTwo.position);
    }
}
