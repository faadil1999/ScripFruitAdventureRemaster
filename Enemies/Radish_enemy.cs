using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radish_enemy : Enemy
{
    private bool groundBelowDetected;
    private bool groundAboveDetected;
    [Header("Radish Specific")]
    [SerializeField] private float distanceFlyFromGround;
    [SerializeField] private float distanceGroundabove;
    [SerializeField] private float aggresiveTime;
                     private float aggresiveCounter;

    [Header("Radish Specific audio")]
    [SerializeField] private AudioSource m_audioSource;
    private bool isPlayingSFX;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isAggresive = true;
        m_audioSource.Play();
        isPlayingSFX = true;
    }

    // Update is called once per frame
    void Update()
    {
        aggresiveCounter -= Time.deltaTime;
        if ( aggresiveCounter < 0 && !groundAboveDetected)
        {
            rb.gravityScale = 1;
            isAggresive = false;
        }

        if (!isAggresive)
        {
            if( !isPlayingSFX )
            {
                m_audioSource.Play();
                isPlayingSFX = true;
            }
            if (groundBelowDetected && !groundAboveDetected)
            {
                rb.velocity = new Vector2(0,1);
            }
            
        }
        else
        {
            if (isGrounded)
            {
                WalkAround();
                isPlayingSFX = false;
                m_audioSource.Stop();
            } 
        }
        CollisionCheck();
        AnimationController();
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - distanceFlyFromGround));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + distanceGroundabove));
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        groundBelowDetected = Physics2D.Raycast(transform.position, Vector2.down, distanceFlyFromGround, whatIsGround);
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, distanceGroundabove, whatIsGround);
    }
    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetBool("isAggresive",isAggresive);
    }

    public override void Damage()
    {
        if(!isAggresive)
        {
            aggresiveCounter = aggresiveTime;
            rb.gravityScale = 12;
            isAggresive = true;
        }
        else
        {
            base.Damage();
        }
    }
}
