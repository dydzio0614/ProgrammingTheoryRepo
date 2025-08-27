using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoosterController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-10f, 10f), Vector3.up);
        
        GetComponent<Rigidbody>().AddForce(rotation * Vector3.forward * 10f, ForceMode.Impulse);
    }
}
