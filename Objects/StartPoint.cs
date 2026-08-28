using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    
    void Start()
    {
        PlayerManager.instance.respawnPosition = respawnPoint;
        PlayerManager.instance.PlayerRespawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if(!GameManager.instance.start_timer)
            {
                GameManager.instance.start_timer = true;
            }
           if (collision.transform.position.x < transform.position.x)
                GetComponent<Animator>().SetTrigger("touched");

        }
    }
    // Update is called once per frame

}
