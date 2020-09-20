using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    void Update()
        {
        float _speed = Random.Range(3.0f, 10.0f);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
}

