using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public enum FruitTypes {
        apple,
        banana,
        cherry,
        kiwi,
        melon,
        orange,
        pineaple,
        strawberry
    }
    public class Fruit : MonoBehaviour
    {
        public FruitTypes fruit_type;
        [SerializeField] protected SpriteRenderer sr;
        [SerializeField] private Sprite[] fruit_images;
        [SerializeField] private GameObject pickFruitFx;

        [SerializeField] protected Animator anim;
        public void FruitSetup(int fruit_index)
        {
            anim = GetComponent<Animator>();    
            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.SetLayerWeight(i, 0);
            }

            anim.SetLayerWeight((fruit_index), 1);
        } 

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                AudioManager.instance.PlaySFX(SoundId.FruitPickup);
                collision.GetComponent<Player>().IncrementFruits();
                Destroy(gameObject);
                GameObject newObj = Instantiate(pickFruitFx, transform.position, transform.rotation);
                Destroy(newObj, 3.5f);
            }
        }

        private void OnValidate()
        {
            if (sr == null)
                sr = GetComponent<SpriteRenderer>();

            int index = (int)fruit_type;
            if (sr != null && fruit_images != null && index >= 0 && index < fruit_images.Length)
                sr.sprite = fruit_images[index];
        }
    }
}
