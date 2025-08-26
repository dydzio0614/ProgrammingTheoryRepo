using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 10f;
    
    private void Update()
    {
        transform.position += transform.forward * (_movementSpeed * Time.deltaTime);
    }
}
