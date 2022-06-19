using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private float _sharpness;
    [SerializeField] private Vector2 _screenFormat = new Vector2(1080, 1920);
    [SerializeField] private CenterPoint _startCenterPoint;

    private Camera _camera;
    private Vector2 _deltaRotationY;
    private Vector2 _deltaRotationX;
    private float _rotationY = 0;
    private float _rotationX = 0;
    private float _radius;
    private float _scaleDifference = 1;

    private void Update()
    {
        if (_camera.transform.rotation != Quaternion.Euler(_rotationX, _rotationY + 180, 0))
        {
            _camera.transform.position = new Vector3(Mathf.Cos(_rotationX * Mathf.Deg2Rad) * Mathf.Sin(_rotationY * Mathf.Deg2Rad) * _radius, Mathf.Sin(_rotationX * Mathf.Deg2Rad) * _radius, Mathf.Cos(_rotationX * Mathf.Deg2Rad) * Mathf.Cos(_rotationY * Mathf.Deg2Rad) * _radius) + _center.transform.position;
            _camera.transform.rotation = Quaternion.Euler(_rotationX, _rotationY + 180, 0);
        }
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _scaleDifference = ((float)Screen.height / Screen.width) / ((float)_screenFormat.y / _screenFormat.x);
        ChangeCenterPoint(_startCenterPoint);
    }

    public void OnSwipe(Vector2 deltaSwipe)
    {
        _rotationY = Mathf.Clamp(_rotationY += deltaSwipe.x * _sharpness, _deltaRotationY.x, _deltaRotationY.y);
        _rotationX = Mathf.Clamp(_rotationX -= deltaSwipe.y * _sharpness, _deltaRotationX.x, _deltaRotationX.y);
    }

    public void ChangeCenterPoint(CenterPoint centerPoint)
    {
        _center = centerPoint.Center;
        _radius = centerPoint.Radius * _scaleDifference;
        _rotationX = centerPoint.StartRotaion.x;
        _rotationY = centerPoint.StartRotaion.y;
        _deltaRotationX = centerPoint.DeltaRotationX;
        _deltaRotationY = centerPoint.DeltaRotationY;
    }
}
