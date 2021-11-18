using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil_lemon : MonoBehaviour
{
    [SerializeField] float LemonSpeed;
    //Animator myAnimator;
    BoxCollider2D myCollider;
    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        //myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HitDet();
        Movement();
    }
    void Movement()
    {

        transform.Translate(new Vector2(Mathf.Sign(transform.localScale.x) * LemonSpeed * Time.deltaTime, 0.0125f));
    }
    void HitDet()
    {
        RaycastHit2D myRaycast = Physics2D.Raycast(myCollider.bounds.center, new Vector2(Mathf.Sign(transform.localScale.x), 0), myCollider.bounds.extents.x, LayerMask.GetMask("Ground"));
        RaycastHit2D myRaycastU = Physics2D.Raycast(myCollider.bounds.center, new Vector2(Mathf.Sign(transform.localScale.x), 1), myCollider.bounds.extents.x, LayerMask.GetMask("Ground"));
        RaycastHit2D myRaycastD = Physics2D.Raycast(myCollider.bounds.center, new Vector2(Mathf.Sign(transform.localScale.x), -1), myCollider.bounds.extents.x, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2((myCollider.bounds.extents.x), 0), Color.red);
        Debug.DrawRay(myCollider.bounds.center, new Vector2((myCollider.bounds.extents.x), 0.125f), Color.red);
        Debug.DrawRay(myCollider.bounds.center, new Vector2((myCollider.bounds.extents.x), -0.125f), Color.red);
        if (myRaycast.collider != null || myRaycastU.collider != null || myRaycastD.collider != null)
        {
            //myAnimator.SetBool("Hit", true);
            myBody.velocity = new Vector2(0, 0);
            Destroy(gameObject);
        }

    }
}
