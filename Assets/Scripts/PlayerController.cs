using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _movementSpeed = 10f;
    
    private const float ProjectileForwardOffset = 0.5f;
    
    private Rigidbody _rigidbody;
    private InputSystemActions _inputSystemActions;
    
    private Vector2 _movementInput;
    private Vector3 _displaySize;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _displaySize = GetComponent<Renderer>().bounds.size;
        _inputSystemActions = new InputSystemActions();
        _inputSystemActions.Enable();
    }

    private void Update()
    {
        if(_inputSystemActions.Player.Attack.WasPressedThisFrame())
            Instantiate(_projectilePrefab, transform.position + transform.forward * ProjectileForwardOffset, transform.rotation);
        
        _movementInput = _inputSystemActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero)
        {
            Vector3 newPosition = _rigidbody.position + new Vector3(_movementInput.x, 0, _movementInput.y) * (Time.fixedDeltaTime * _movementSpeed);
            newPosition.x = Mathf.Clamp(newPosition.x, PersistentData.Instance.LeftPlayAreaBound + _displaySize.x / 2, PersistentData.Instance.RightPlayAreaBound - _displaySize.x / 2);
            newPosition.z = Mathf.Clamp(newPosition.z, PersistentData.Instance.LowerPlayAreaBound + _displaySize.z / 2, PersistentData.Instance.UpperPlayAreaBound - _displaySize.z / 2);
            
            _rigidbody.MovePosition(newPosition);
        }
    }

    private void OnDestroy()
    {
        _inputSystemActions.Dispose();
    }
}
