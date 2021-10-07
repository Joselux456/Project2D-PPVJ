using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon_Pellet : MonoBehaviour
{
    [SerializeField] float LemonSpeed;
    Animator myAnimator;
    BoxCollider2D myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        transform.Translate(new Vector3(0, 0, -1 * (Mathf.Sign(transform.position.z)) * 0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        HitDet();
        Movement();
    }
    void Movement()
    {
        
        transform.Translate(new Vector2(Mathf.Sign(transform.position.z)*LemonSpeed * Time.deltaTime,0));
    }
    void HitDet()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Hit", true);
            Destroy(gameObject);
        }
    }
}
