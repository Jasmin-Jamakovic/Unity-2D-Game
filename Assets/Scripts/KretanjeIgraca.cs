using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KretanjeIgraca : MonoBehaviour
{
     private Rigidbody2D RB;
    private BoxCollider2D BC;
     private Animator anim;
    private float dirX=0;
    private SpriteRenderer sprite;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField]private float move = 7f;
    [SerializeField]private float jumpf = 10f;


    private enum MovementState {idle,running,jumping,falling };

    [SerializeField] private AudioSource jumpSoundEffect;
    
    // Start is called before the first frame update
    private void Start()
    {
        RB= GetComponent<Rigidbody2D>();     
        anim=GetComponent<Animator>();
        sprite=GetComponent<SpriteRenderer>();
        BC=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        RB.velocity = new Vector2(dirX * move,RB.velocity.y);

        if (Input.GetButtonDown("Jump")&&Grounded())
        {
            jumpSoundEffect.Play();
            RB.velocity = new Vector2(RB.velocity.x, jumpf);
        
        }
        UpdateAnimation();
       
        
    }
    private void UpdateAnimation()
    {
        MovementState state;
        
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
           state= MovementState.idle;
        }
        if(RB.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(RB.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("stanje", (int)state);
    }

    private bool Grounded()
    {
        return Physics2D.BoxCast(BC.bounds.center, BC.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

        
    }
}
