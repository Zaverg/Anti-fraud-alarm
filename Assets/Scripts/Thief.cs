using System;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private Transform _startPoint;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    [SerializeField] private float _distance;

    private Transform _currentTarget;
    private bool _isDiscovered;

    private void Start()
    {
        _currentTarget = _targets[0];
    }

    private void Update()
    {
        Move();
        Rotate();

        if ((transform.position - _currentTarget.position).sqrMagnitude <= _distance)
            UpdateTarget();
    }

    public void UpdateState() =>
       _isDiscovered = !_isDiscovered;

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);   
    }

    private void Rotate()
    {
        Vector3 direction = _currentTarget.position - transform.position;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speedRotation);
    }

    private void UpdateTarget()
    {
        int direction = 1;

        if (_isDiscovered)
        {
            direction = -1;
        }

        int index = _targets.IndexOf(_currentTarget) + direction;

        if (index < _targets.Count && index >= 0)
            _currentTarget = _targets[index];
    }
}
