using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public event Action OnDeath;
    
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private GameObject _boosterPrefab;
    [SerializeField]
    private float _movementSpeed = 2f;
    [SerializeField]
    private int _health = 1;
    
    private const float ProjectileForwardOffset = 0.5f;
    
    private Vector3 _movementDirection;
    
    void Start()
    {
        _movementDirection = transform.right;
        StartCoroutine(MovementDirectionSwapCoroutine());
        StartCoroutine(SpawnProjectileCoroutine());
    }
    
    void Update() => transform.Translate(_movementDirection * (_movementSpeed * Time.deltaTime));
    
    IEnumerator SpawnProjectileCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            Instantiate(_projectilePrefab, transform.position + transform.forward * ProjectileForwardOffset, transform.rotation);
        }
    }

    IEnumerator MovementDirectionSwapCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            _movementDirection *= -1;
        }
    }

    private void OnDestroy()
    {
        Instantiate(_boosterPrefab, transform.position, _boosterPrefab.transform.rotation);
        OnDeath?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(EditorTags.PlayerProjectile))
        {
            _health--;
            Destroy(other.gameObject);
            
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
