using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

    [SerializeField]
    private float _speed = 100;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private Transform _followTo;

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, _followTo.position + _offset, Time.deltaTime * _speed);
    }
}
