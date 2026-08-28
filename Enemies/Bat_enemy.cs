using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_enemy : Enemy
{
    [Header("Bat specific")]
    [SerializeField] private Transform[] idlePoints;

    private Vector2 destination;
    private bool canBeAggresive;
    private bool playerDetected;
    private float defaultSpeed;

    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float checkRadius;

  
    protected override void Start()
    {
        base.Start();
        defaultSpeed = speed;
        destination = idlePoints[0].position;
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);

        anim.SetBool("canBeAggresive", canBeAggresive);
        anim.SetFloat("speed", speed);
        idleTimeCounter -= Time.deltaTime;
        FlipController();

        if (idleTimeCounter > 0)
        {
            return;
        }

        if (playerDetected && !isAggresive && canBeAggresive)
        {
            isAggresive = true;
            canBeAggresive = false;
            if (player != null)
            {
                destination = player.transform.position;
            }
            else
            {
                isAggresive = false;
                canBeAggresive = true;
            }
        }

        if (isAggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < 0.1f)
            {
                isAggresive = false;

                int i = Random.Range(0, idlePoints.Length);
                destination = idlePoints[i].position;
                speed *= 0.5f;
            }

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < 0.1f)
            {
                if (!canBeAggresive)
                {
                    canBeAggresive = true;
                    idleTimeCounter = idleTime;
                    speed = defaultSpeed;
                }
            }
        }

    }

    private void FlipController()
    {
        if (player == null)
            return; 

        if (destination.x > transform.position.x && facedirection == -1)
        {
            Flip();
            facedirection = 1;
        }
        else if (destination.x < transform.position.x && facedirection == 1)
        {
            Flip();
            facedirection = -1;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

}
