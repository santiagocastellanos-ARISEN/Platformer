using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ballScript : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 8f;
    public float fallLimit = -10f;

    private InputAction moveAction;

    Rigidbody2D rb;
    bool onGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        moveAction = new InputAction(type: InputActionType.Value, expectedControlType: "Vector2");
        moveAction.AddCompositeBinding("2DVector")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
        moveAction.Enable();
    }

    void FixedUpdate()
    {
        float x = moveAction.ReadValue<Vector2>().x;
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        onGround = true;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && onGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            onGround = false;
        }

        if (transform.position.y < fallLimit)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
