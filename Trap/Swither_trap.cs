using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class Swither_trap : MonoBehaviour
    {
        // Start is called before the first frame update
                         private Trap_fire mytrap;
                         private Animator anim;
                         private float couldDown;
        [SerializeField] private float timeNoActive = 2;


        private void Start()
        {

            anim = GetComponent<Animator>();
            mytrap = GetComponentInChildren<Trap_fire>();
        }

        private void Update()
        {
            couldDown -= Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(couldDown > 0) { return; }

            if(collision.GetComponent<Player>()!= null) 
            {
                couldDown = timeNoActive;
                anim.SetTrigger("pressed"); 
                mytrap.FireSwitchOnTime(timeNoActive);
            }
        }

    }
}
