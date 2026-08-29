using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class MovingPleform : MonoBehaviour
    {
        private Animator anim;
        [SerializeField] private bool isWorking;
        [SerializeField] private Transform[] movepoints;
        [SerializeField] private float speed;
        [SerializeField] private float cooldown;

        private float cooldownTimer;
        private int indexMovepoint;
        // Start is called before the first frame update
        void Start()
        {
            indexMovepoint = 0;
            anim = GetComponent<Animator>();
            transform.position = movepoints[0].position;
        }

        // Update is called once per frame
        void Update()
        {
            cooldownTimer -= Time.deltaTime;

            isWorking = cooldownTimer < 0;
            anim.SetBool("isWorking", isWorking);
            if (isWorking)
            {
                transform.position = Vector3.MoveTowards(transform.position, movepoints[indexMovepoint].position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, movepoints[indexMovepoint].position) < 0.15f)
            {
                cooldownTimer = cooldown;
                indexMovepoint++;
                if (indexMovepoint == movepoints.Length)
                {
                    indexMovepoint = 0;
                }
            }

        }

        //this function is for allowing the player to move once it is on plateform
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Player>() != null)
            {
                collision.transform.SetParent(transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                collision.transform.SetParent(null);
            }
        }
    }
}
