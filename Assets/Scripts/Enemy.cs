using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    public event Action OnDeath;
    
    [SerializeField]
    protected GameObject _boosterPrefab;
    [SerializeField]
    protected float _movementSpeed = 2f;
    [SerializeField]
    protected int _health = 1;

    protected bool _isMoving = true;
    
    private Vector3 _movementDirection;
    
    
    void Start()
    {
        _movementDirection = transform.right;
        StartCoroutine(MovementDirectionSwapCoroutine());
        StartCoroutine(AttackBehaviorLoop());
    }

    protected abstract IEnumerator AttackBehaviorLoop(); // POLYMORPHISM

    protected virtual void Update()
    {
        if(_isMoving)
            transform.Translate(_movementDirection * (_movementSpeed * Time.deltaTime));
    }

    protected IEnumerator MovementDirectionSwapCoroutine()
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

    protected virtual void OnTriggerEnter(Collider other)
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
