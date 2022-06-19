using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPoint : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private Vector3 _startRotaion;
    [SerializeField] private float _radius;
    [SerializeField] private Vector2 _deltaRotationX;
    [SerializeField] private Vector2 _deltaRotationY;

    public Transform Center => _center;
    public Vector3 StartRotaion => _startRotaion;
    public float Radius => _radius;
    public Vector2 DeltaRotationX => _deltaRotationX;
    public Vector2 DeltaRotationY => _deltaRotationY;
}
