using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    
    public static PersistentData Instance { get; private set; }
    
    public float UpperPlayAreaBound { get; private set; }
    public float LowerPlayAreaBound { get; private set; }
    public float LeftPlayAreaBound { get; private set; }
    public float RightPlayAreaBound { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeData();
    }

    private void InitializeData()
    {
        const float TargetAspectRatio = 16f / 9f;
        
        float xCameraOffset = _mainCamera.transform.position.x;
        float zCameraOffset = _mainCamera.transform.position.z;
        
        float xLimit = _mainCamera.orthographicSize * TargetAspectRatio;
        float zLimit = _mainCamera.orthographicSize;
        
        UpperPlayAreaBound = zLimit + zCameraOffset;
        LowerPlayAreaBound = -zLimit + zCameraOffset;
        LeftPlayAreaBound = -xLimit + xCameraOffset;
        RightPlayAreaBound = xLimit + xCameraOffset;
    }
}