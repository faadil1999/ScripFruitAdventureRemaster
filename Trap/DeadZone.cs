using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class DeadZone : MonoBehaviour
    {
        //Destroy player after passing throught THE DEADZONE

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Player>() != null)
            {
                PlayerManager.instance.OnFalling();
            }
        }
    }
}
