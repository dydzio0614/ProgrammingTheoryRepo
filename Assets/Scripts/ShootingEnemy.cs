using System.Collections;
using UnityEngine;

public class ShootingEnemy : Enemy // INHERITANCE
{
    [SerializeField]
    private GameObject _projectilePrefab;
    
    private const float ProjectileForwardOffset = 0.5f;
    
    protected override IEnumerator AttackBehaviorLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            Instantiate(_projectilePrefab, transform.position + transform.forward * ProjectileForwardOffset, transform.rotation);
        }
    }
}
