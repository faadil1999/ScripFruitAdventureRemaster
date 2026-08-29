using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class Trap_saw_multiple_p : HitZoneable
    {
        // Start is called before the first frame update
        private Animator anim;
        [SerializeField] private bool isWorking;
        [SerializeField] private Transform[] movepoints;
        [SerializeField] private float speed;
        [SerializeField] private float cooldown;

        private float cooldownTimer;
        private int indexMovepoint;
        private bool isGoingForward = true;

        void Start()
        {
            indexMovepoint = 0;
            anim = GetComponent<Animator>();
            Flip();
            transform.position = movepoints[0].position;

        }

        // Update is called once per frame
        void Update()
        {
            cooldownTimer -= Time.deltaTime;

            isWorking = cooldownTimer < 0;
            AnimaionSawTrapController();
            if (isWorking)
            {
                transform.position = Vector3.MoveTowards(transform.position, movepoints[indexMovepoint].position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, movepoints[indexMovepoint].position) < 0.15f)
            {

                cooldownTimer = cooldown;

                if(isGoingForward == true)
                {
                    indexMovepoint++;
                }
                else
                {
                    indexMovepoint--;
                }

                if (indexMovepoint == movepoints.Length)
                {
                    indexMovepoint = movepoints.Length - 1;
                    isGoingForward = false;
                }
                else if(indexMovepoint == 0)
                {
                    isGoingForward = true;
                    Flip();
                }
            }

        }

        void AnimaionSawTrapController()
        {
            anim.SetBool("isWorking", isWorking);
        }

        private void Flip()
        {
            transform.localScale = new Vector2(1, transform.localScale.y * -1);
        }

    }
}
