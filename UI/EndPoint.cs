using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class EndPoint : MonoBehaviour
    {
        private AllCounter allCounter;
        // Start is called before the first frame update
        void Start()
        {
            allCounter = GameObject.Find("CanvasInGameUI").GetComponent<AllCounter>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Player>() != null)
            {
                if (GameManager.instance.start_timer)
                {
                    GameManager.instance.start_timer = false;
                }
                GetComponent<Animator>().SetTrigger("touched");
                Destroy(collision.gameObject);
                GameManager.instance.SaveBestTime();
                GameManager.instance.SaveTotalFruitCollected();
                GameManager.instance.SaveLevelInfo();
                allCounter.OnEndLevel();
            }
        }
    }
}
