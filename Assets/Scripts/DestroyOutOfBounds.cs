using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    void Update()
    {
        if(transform.position.z < PersistentData.Instance.LowerPlayAreaBound || transform.position.z > PersistentData.Instance.UpperPlayAreaBound)
            Destroy(gameObject);
    }
}
