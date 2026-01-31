using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference mask1;
    public InputActionReference mask2;
    public InputActionReference mask3;
    public InputActionReference mask4;
    public float laneDistance = 1f;
    public int MaskIndex;


    private int currentLane = 0;
    private Material material;

    public int Health = 3;

    [SerializeField] private GameObject Player;

     void Start()
    {
        material = Player.GetComponent<Renderer>().material;
    }

    private void Update()
    {
    }
    private void OnEnable()
    {
        move.action.performed += OnMove;
        move.action.Enable();

        mask1.action.performed += Mask1;
        mask2.action.performed += Mask2;
        mask3.action.performed += Mask3;
        mask4.action.performed += Mask4;

        mask1.action.Enable();
        mask2.action.Enable();
        mask3.action.Enable();
        mask4.action.Enable();
    }

    private void OnDisable()
    {
        move.action.performed -= OnMove;
        move.action.Disable();

        mask1.action.performed -= Mask1;
        mask2.action.performed -= Mask2;
        mask3.action.performed -= Mask3;
        mask4.action.performed -= Mask4;

        mask1.action.Disable();
        mask2.action.Disable();
        mask3.action.Disable();
        mask4.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        if (input.x > 0)
        {
            currentLane++;
        }
        else if (input.x < 0)
        {
            currentLane--;
        }

       
        currentLane = Mathf.Clamp(currentLane, -2, 2);

       
        transform.position = new Vector3(
            currentLane * laneDistance,
            transform.position.y,
            transform.position.z
        );
    }

    public void Mask1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SetMask(0, Color.red);
    }

    public void Mask2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SetMask(1, Color.green);
    }

    public void Mask3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SetMask(2, Color.blue);
    }

    public void Mask4(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SetMask(3, Color.yellow);
    }

    private void SetMask(int index, Color color)
    {
        MaskIndex = index;
        material.SetColor("_Base_color", color);
    }
}
