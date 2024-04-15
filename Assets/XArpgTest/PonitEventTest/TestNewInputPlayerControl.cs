using UnityEngine;
using UnityEngine.InputSystem;

public class TestNewInputPlayerControl : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputAsset;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    
    public CharacterController characterController;
    public Vector3 moveVector = Vector3.zero;
    public float speed = 5f;
    public TestJoyStick joyStick;
    
    // Start is called before the first frame update
    void Start()
    {
        if (inputAsset == null)
        {
            Debug.LogError("InputAsset is null");
            return;
        }   

        var inputActionMap = inputAsset.FindActionMap("MyTestPlayer");
        moveAction = inputActionMap.FindAction("XHWMove");
        moveAction.started += OnXHWMove;
        moveAction.canceled += OnXHWMove;
        moveAction.performed += OnXHWMove;
        moveAction.Enable();
        
        jumpAction = inputActionMap.FindAction("XHWJump");
        jumpAction.started += OnXHWJump;
        jumpAction.canceled += OnXHWJump;
        jumpAction.performed += OnXHWJump;
        jumpAction.Enable();
        
        attackAction = inputActionMap.FindAction("XHWAttack");
        attackAction.performed += OnXHWAttack;
        attackAction.Enable();
            
        joyStick.OnJoystickMove += OnXHWJoyStickMove;
        joyStick.OnJoystickUp += OnXHWJoyStickUp;
    }
    
    private void FixedUpdate()
    {
        if (moveVector != Vector3.zero)
        {
            characterController.Move(moveVector * speed * Time.fixedDeltaTime);
        }
    }
    
    public void OnXHWMove(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove" + context.ToString());
        Vector2 move = context.ReadValue<Vector2>();
        moveVector = new Vector3(move.x, 0, move.y);
    }

    public void OnXHWAttack(InputAction.CallbackContext context)
    {
        Debug.Log("OnAttack" + context.ToString());
    }
    
    public void OnXHWJoyStickMove(Vector2 move)
    {
        moveVector = new Vector3(move.x, 0, move.y);
    }

    public void OnXHWJoyStickUp()
    {
        moveVector = Vector3.zero;
    }
    
    public void OnXHWJump(InputAction.CallbackContext context)
    {
         Debug.Log("OnJump" + context.ToString());
    }
}
