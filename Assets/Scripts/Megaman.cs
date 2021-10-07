using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float Speedo;
    [SerializeField] BoxCollider2D LeFoot;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dashPower;
    [SerializeField] int fireDelay;
    [SerializeField] GameObject LemonAmmo;
    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    float FoHspeed, FoJspeed,enabler;
    int dashTime,dashTimer,fireTimer;
    int jumpCapacity = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        FoHspeed = Speedo;
        FoJspeed = jumpSpeed;
        dashTime = (int)(10 * dashPower);
        dashTimer = dashTime;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Falling();
        Fire();
        Dash();
        Djump();
    }
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            myAnimator.SetLayerWeight(1, 1);
            Instantiate(LemonAmmo, transform.position + new Vector3(Mathf.Sign(transform.localScale.x)*(myCollider.bounds.extents.x+1f),0, Mathf.Sign(transform.localScale.x) * 0.1f), transform.rotation);
            fireTimer = fireDelay;
        }
        else if(fireTimer == 0)
        {
            myAnimator.SetLayerWeight(1, 0);
        }   
        fireTimer--;
    }
    void Movement() 
    {
        float mov = Input.GetAxis("Horizontal");
            if (mov != 0)
            {
                transform.localScale = new Vector2(Mathf.Sign(mov), 1);
                myAnimator.SetBool("running", true);

            }
            else
            {
                myAnimator.SetBool("running", false);
            }
            transform.Translate(new Vector2(mov * Speedo * Time.deltaTime, 0));
    }
    void Dash()
    {
        
        if (Input.GetKeyDown(KeyCode.X) && dashTimer != 0 && IsGrounded())
        {
            myAnimator.SetBool("FastBoi", true);
            Speedo = Speedo + 1.5F * dashPower;
            jumpSpeed = jumpSpeed + dashPower;
        }
        else if (Input.GetKeyUp(KeyCode.X) || dashTimer == 0)
        {
            myAnimator.SetBool("FastBoi", false);
            if (IsGrounded())
            {
                Speedo = FoHspeed;
            }
            jumpSpeed = FoJspeed;
            dashTimer = dashTime;
        }
        dashTimer--;

    }
    void Jump()
    {

        if (IsGrounded() && !myAnimator.GetBool("Jumping"))
        {
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Falling", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("Liftoff");
                myAnimator.SetBool("Jumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                enabler = Time.time + 100f;
            }
            
        }
            
    }
    void Djump()
    {
        if (!IsGrounded() && jumpCapacity > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            myBody.AddForce(new Vector2(0, jumpSpeed / 1.5f), ForceMode2D.Impulse);
            jumpCapacity--;
        }
        if (IsGrounded() && jumpCapacity < 1)
        {
            jumpCapacity = 1;
        }
    }

        void Falling()
        {
            if(myBody.velocity.y < 0 && !myAnimator.GetBool("Jumping") && enabler == Time.time)
            {
                myAnimator.SetBool("Falling", true);
            }
        }

        bool IsGrounded()
        {
            //return LeFoot.IsTouchingLayers(LayerMask.GetMask("Ground"));
            RaycastHit2D myRaycast = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(myCollider.bounds.center,new Vector2(0, (myCollider.bounds.extents.y + 0.2f)*-1),Color.red);
            return myRaycast.collider != null;
        }
        void AfterTakeoffEvent()
        {
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Falling", true);
        }
}
