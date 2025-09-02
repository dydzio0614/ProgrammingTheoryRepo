using System.Collections;
using UnityEngine;

public class ChargingEnemy : Enemy //INHERITANCE
{
    [SerializeField]
    private float _chargeSpeed = 12f;
    
    protected override IEnumerator AttackBehaviorLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(8f, 12f));

            _isMoving = false;
            Vector3 playerPosition = PlayerController.Instance.transform.position;
            
            transform.LookAt(playerPosition);

            Vector3 originalPosition = transform.position;
            
            Debug.DrawLine(originalPosition, playerPosition, Color.red, float.MaxValue);
            
            while (Vector3.Distance(transform.position, playerPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, _chargeSpeed * Time.deltaTime);
                yield return null;
            }
            
            while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, _chargeSpeed * Time.deltaTime);
                yield return null;
            }

            transform.rotation = Quaternion.Euler(0, 180, 0);
            _isMoving = true;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(EditorTags.Player))
        {
            other.GetComponent<PlayerController>().DealDamage();
        }
    }
}
