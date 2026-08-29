using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class EnemyDropController : MonoBehaviour
    {
        [SerializeField] private GameObject fruit;
        [Range(2,10)]
        [SerializeField] private int dropAmount;

        public void DropFruits()
        {
            for (int i = 0; i < dropAmount; i++)
            {
                GameObject droppedFruit = Instantiate(fruit, transform.position, transform.rotation);
                Destroy(droppedFruit, 5);
            }
        }

    }
}
