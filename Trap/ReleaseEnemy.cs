using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseEnemy : MonoBehaviour
{
    [SerializeField] Enemy enemyToRelease;
    // Start is called before the first frame update
    void Start()
    {
        enemyToRelease.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.GetComponent<Player>() != null)
        {
            Debug.Log("bien player");
            enemyToRelease.enabled=true;
        }
    }
}
