using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float movX = 0f;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
    
        movX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movX * moveSpeed, rb.velocity.y);

       if (Input.GetButtonDown("Jump"))
       {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
       }
       
        UpdateAnimationState();
      
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (movX > 0f)
       {
            state = MovementState.running;
            sprite.flipX = false;
       }
       else if (movX < 0f)
       {
            state = MovementState.running;
            sprite.flipX = true;
       }
       else
       {
            state = MovementState.idle;
       }

       if (rb.velocity.y > .1f)
       {
            state = MovementState.jumping;
       }
       else if (rb.velocity.y < -1f)
       {
            state = MovementState.falling;
       }

       anim.SetInteger("state", (int) state);
    }
}