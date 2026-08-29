using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerAterImage : MonoBehaviour
    {
        [SerializeField] private float activeTIme = 0.1f;
        private float timeActivated;
        private float alpha;
        [SerializeField] private float alphaSet = 0.8f;
        private float alphaMultiplier = 0.85f;

        private GameObject player;

        private SpriteRenderer SR;
        private SpriteRenderer playerSR;

        private Color color;

        private void OnEnable()
        {
        }

        private void Start()
        {
            SR = GetComponent<SpriteRenderer>();
            player = PlayerManager.instance?.currentPlayer;
            playerSR = player.GetComponent<SpriteRenderer>();

            alpha = alphaSet;
            SR.sprite = playerSR.sprite;
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
            timeActivated = Time.time;

        }

        private void Update()
        {
            Debug.Log(player.transform.position);
            alpha = alphaMultiplier;
            color = new Color(1f, 1f, 1f, alpha);
            SR.color = color;

            if(Time.time >= timeActivated)
            {
                PlayerAfterImagePool.Instance.AddToPool(gameObject);
            }
        }
    }
}
