using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_enemy : Enemy
{
    [Header("Ghost specifics")]
                     private float activeTimeCounter = 4;
    [SerializeField] private float activeTime;
    [SerializeField] private float[] xOffset;   
    private SpriteRenderer sr;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        isAggresive = true;
        isInvincible = true;
    }

    // Update is called once per frame
    void Update()
    {
        player = PlayerManager.instance?.currentPlayer?.transform;
        if (player == null)
        {
            anim.SetTrigger("vanish");
            return;
        }

        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if (activeTimeCounter > 0)
        {
            //MoveTowards(from position, to position, speed movement)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        //Algo for vanishing and appearing ghost START
        if (idleTimeCounter < 0 && activeTimeCounter < 0 && isAggresive)
        {
            anim.SetTrigger("vanish");
            isAggresive = false;
            idleTimeCounter = idleTime;
        }
        if (idleTimeCounter < 0 && activeTimeCounter < 0 && !isAggresive)
        {
            ChoosePosition();
            anim.SetTrigger("appear");
            isAggresive = true;
            activeTimeCounter = activeTime;
        }
        //Algo ----------------------------------END

        //is looking to the left
        FlipController();
    }

    private void FlipController()
    {
        if (player == null)
            return;

        if (facedirection == -1 && transform.position.x < player.position.x)
        {
            Flip();
        }
        else if (facedirection == 1 && transform.position.x > player.position.x)
        {
            Flip();
        }
    }

    protected override void AnimationController()
    {
        base.AnimationController();
    }

    private void ChoosePosition()
    {
        float _xOffset = xOffset[Random.Range(0, xOffset.Length)];
        float _yOffset = Random.Range(-5, 5);
        transform.position = new Vector2(player.position.x + _xOffset, player.position.y + _yOffset);
    }

    public void Vanish()
    {
        sr.enabled = false;
    } 

    public void Appear()
    {
        sr.enabled = true; 
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(isAggresive)
        {
            base.OnTriggerEnter2D(collider);
        }
    }
}
