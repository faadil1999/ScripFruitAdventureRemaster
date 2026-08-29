using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class Trap_fire : HitZoneable
    {

        public bool isWorking;
        private Animator anim ;


        public float repeatRate;

        private void Start()
        { 
            anim = GetComponent<Animator>();
            if(transform.parent == null)
            {
                InvokeRepeating("FireSwitch", 0, repeatRate);
            }

        }

        private void Update() 
        {

            AnimTrapController();

        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if(isWorking)
                base.OnTriggerEnter2D(collision);
        }

        public void FireSwitch() 
        {
            isWorking = !isWorking;

        }

        public void FireSwitchOnTime(float sec)
        {
            isWorking = false;
            Invoke("FireSwitch", sec);
        }

        void AnimTrapController() 
        {
            anim.SetBool("isTrapOn", isWorking);
        }

    }
}
