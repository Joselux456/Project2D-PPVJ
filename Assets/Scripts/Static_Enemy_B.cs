using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static_Enemy_B : MonoBehaviour
{
    [SerializeField] GameObject LemonAmmo;
    [SerializeField] int fireDelay;
    [SerializeField] int range;
    int fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (fireTimer == 0 && Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        {
            GameObject newLemon1 = LemonAmmo;
            GameObject newLemon2 = LemonAmmo;

            //myAnimator.SetLayerWeight(1, 1);
            newLemon1.transform.localScale = new Vector3(2,2,2);
            Instantiate(newLemon1, transform.position + new Vector3(0.5f,0.5f,0), transform.rotation);
            fireTimer = fireDelay;
            newLemon2.transform.localScale = new Vector3(-2, 2, 2);
            Instantiate(newLemon2, transform.position + new Vector3(-0.5f,0.5f,0), transform.rotation);
            fireTimer = fireDelay;
        }
        else if (fireTimer == 0)
        {
            //myAnimator.SetLayerWeight(1, 0);
        }
        fireTimer--;
        if(fireTimer < 0) { fireTimer = 0; }
    }
}
