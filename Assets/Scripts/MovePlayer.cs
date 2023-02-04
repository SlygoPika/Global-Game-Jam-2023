using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{

    Rigidbody2D controller;
    public float speed;
    public float tiltAngle;

    private float horizontalMovement;
    [SerializeField] private float jumpHeight;

    private float jumpPower;
    private bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody2D>();
        jumpPower = Mathf.Sqrt(-2 * Physics2D.gravity.y * controller.gravityScale * jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
            canJump = false;
        }
    }


    void FixedUpdate()
    {

        controller.velocity = new Vector2(horizontalMovement * speed,
            controller.velocity.y);

        controller.rotation = -horizontalMovement * tiltAngle;
    }

    private void Jump()
    {
        controller.velocity = new Vector2(controller.velocity.x, jumpPower);
    }

    public void SetCanJump(bool canJump)
    {
        this.canJump = canJump;
    }

}
