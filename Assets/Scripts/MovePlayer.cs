using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{

    Rigidbody2D controller;
    public float speed;
    public float tiltAngle;
    public float footstepDelay;

    private float horizontalMovement;
    [SerializeField] private float jumpHeight;

    private float jumpPower;
    private bool canJump = false;
    private float walkTimeElapsed = 0.0f;

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

        if (walkTimeElapsed > footstepDelay && canJump)
        {
            int rand = (int)Random.Range(0.0f, 100.0f) % 3 + 1;

            FindObjectOfType<AudioManager>().Play("Footstep" + rand);

            walkTimeElapsed = 0;
        }
    }


    void FixedUpdate()
    {

        controller.velocity = new Vector2(horizontalMovement * speed,
            controller.velocity.y);

        controller.rotation = -horizontalMovement * tiltAngle;

        if ((horizontalMovement > 0.05f || horizontalMovement < -0.05f))
        {
            walkTimeElapsed += Time.deltaTime;
        } else
        {
            walkTimeElapsed = 0.0f;
        }
    }

    private void Jump()
    {
        controller.velocity = new Vector2(controller.velocity.x, jumpPower);

        int rand = (int) Random.Range(0.0f, 100.0f) % 2 + 1;

        FindObjectOfType<AudioManager>().Play("Jump" + rand);
    }

    public void SetCanJump(bool canJump)
    {
        this.canJump = canJump;
    }

}
