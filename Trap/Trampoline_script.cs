using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline_script : MonoBehaviour
{
    [SerializeField] private float pushForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            GetComponent<Animator>().SetTrigger("pushed");
            AudioManager.instance.PlaySFX(15);
            collision.GetComponent<Player>().PushPlayer(pushForce);
        }
    }
}
