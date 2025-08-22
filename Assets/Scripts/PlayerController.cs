using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 10f;
    
    private Rigidbody _rigidbody;
    private InputSystemActions _inputSystemActions;
    
    private Vector2 _movementInput;
    private float _xLimit;
    private float _zLimit;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputSystemActions = new InputSystemActions();
        _inputSystemActions.Enable();
    }
    
    private void Start()
    {
        const float targetAspectRatio = 16f / 9f;
        Vector3 size = GetComponent<Renderer>().bounds.size;
        float cameraOrthographicSize = Camera.main.orthographicSize;
        
        _xLimit = cameraOrthographicSize * targetAspectRatio - size.x / 2;
        _zLimit = cameraOrthographicSize - size.z / 2;
    }

    private void Update() => _movementInput = _inputSystemActions.Player.Move.ReadValue<Vector2>();
    
    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero)
        {
            Vector3 newPosition = _rigidbody.position + new Vector3(_movementInput.x, 0, _movementInput.y) * (Time.fixedDeltaTime * _movementSpeed);
            newPosition.x = Mathf.Clamp(newPosition.x, -_xLimit, _xLimit);
            newPosition.z = Mathf.Clamp(newPosition.z, -_zLimit, _zLimit);
            _rigidbody.MovePosition(newPosition);
        }
    }

    private void OnDestroy()
    {
        _inputSystemActions.Dispose();
    }
}
