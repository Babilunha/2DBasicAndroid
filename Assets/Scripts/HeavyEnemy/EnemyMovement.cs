using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float nextWaypointDistance = 3f;

    //Navigation
    public Transform navigationPoint1;
    public Transform navigationPoint2;
    public Transform player;
    public BoxCollider2D playerDetector;

    //Attack
    public HeavyEnemy heavyEnemyScript;



    public new Rigidbody2D rigidbody;
    public float searchRange;
    public LayerMask playerLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(PlayerAroundCheck())
        {
            target = null;
            heavyEnemyScript.Attack();

        } else
        {
            target = ClosetPointCheck();
        }






    }

    

    private Transform ClosetPointCheck()
    {
       if(Mathf.Abs(transform.position.x - navigationPoint1.position.x) > Mathf.Abs(transform.position.x - navigationPoint2.position.x)) {
            
            return navigationPoint2;
        } else
        {
            
            return navigationPoint1;
        }
    }


    private bool PlayerAroundCheck()
    {

        
        if (playerDetector.IsTouchingLayers(playerLayer))
        {
            
            return true;
        } else
        {
            return false;
        }
                       
    }
}
