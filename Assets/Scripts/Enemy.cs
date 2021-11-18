using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detector();
            
    }
    void Detector()
    {
        /*if (Vector2.Distance(transform.position, player.transform.position) <= range)
        {
            Debug.Log("Persigalo");
        }
        */
        if(Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        {
            Debug.Log("Persigalo");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(100,0,0,0.2f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
