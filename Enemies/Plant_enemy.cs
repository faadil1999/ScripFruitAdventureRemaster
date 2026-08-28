using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_enemy : Enemy
{
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform shoot_position;
    [SerializeField] private bool facingRight;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if( facingRight )
        {
            Flip();
        }
    }


    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        idleTimeCounter -= Time.deltaTime;
        bool playerDetected = playerDetection.collider?.GetComponent<Player>() != null;

        if (idleTimeCounter < 0 && playerDetected)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("attack");
        }

    }

    private void AttackEvent()
    {
        GameObject bullet = Instantiate(bulletprefab, shoot_position.transform.position, shoot_position.transform.rotation);

        bullet.GetComponent<Bullet_Plant>().SetupSpeed(speed * facedirection, 0);
    }

}
