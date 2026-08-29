using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        public int fruits;
        public Transform respawnPosition ;
        public GameObject currentPlayer = null;
        public int choosenCharacterId ;
        public AllCounter gameUi;
        [Header("Parameters")]
        [SerializeField] private int timeDroppedCollectedFruitsAutoDestruction = 20; 
        [SerializeField] private GameObject fruitPrefab;

        [Header("Info player")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject deadPlyaer_fx;

        //For camera manager 
        [Header("Camera manager")]
        [SerializeField] private CinemachineImpulseSource impulse;
        [SerializeField] private Vector2 shakeDirection;
        [SerializeField] private float forceShake;

        public void ScreenShake(int facingDirection)
        {
            impulse.m_DefaultVelocity = new Vector3(shakeDirection.x * facingDirection, shakeDirection.y) * forceShake;
            impulse.GenerateImpulse();
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

        }

        void Update()
        {

        }

        //This function verify if the player has enought fruits
        private bool HasEnoughtFruit()
        {
            if(fruits > 0)
            {
                fruits --;
                DropFruit();
                if (fruits < 0)
                {
                    fruits = 0;
                }
                return true;
            }
            return false;
        }

        //This function is for dropping fruits by the player after getting damages
        private void DropFruit()
        {
            int fruit_index = UnityEngine.Random.Range(0, Enum.GetNames(typeof(FruitTypes)).Length);
            GameObject newFruits = Instantiate(fruitPrefab, currentPlayer.transform.position, currentPlayer.transform.rotation);
            newFruits.GetComponent<Fruit_Dropped>().FruitSetup(fruit_index);
            Destroy(newFruits, timeDroppedCollectedFruitsAutoDestruction);
        }


        public void OnTakingDamage()
        {
            if(!HasEnoughtFruit())
            {
                int difficulty = GameManager.instance.game_difficulty;
                KillPlayer();

                if (difficulty < 2) 
                {
                    Invoke("PlayerRespawn", 1);
                }
                else
                {
                    gameUi.SwitchUiWhenDead();
                }

            }

        }

        //In case the player fall 
        public void OnFalling()
        {
            int difficulty = GameManager.instance.game_difficulty;
            KillPlayer();
            if (difficulty < 2)
            {
                Invoke("PlayerRespawn", 1);
                if (difficulty > 0)
                {
                    HasEnoughtFruit();
                }
            }
            else
            {
                gameUi.SwitchUiWhenDead();
            }

        }

        //This function is for respawning the player
        public void PlayerRespawn()
        {
            if(currentPlayer == null)
            {
                //AudioManager.instance.PlaySFX(SoundId.PlayerRespawn);
                currentPlayer = Instantiate(playerPrefab, respawnPosition.position, transform.rotation);
                currentPlayer.GetComponent<Player>().ProtectionAfterRespawn();
                gameUi.AssignPlayerControl(currentPlayer.GetComponent<Player>());

            }
        }

        //this function is for killing the player
        public void KillPlayer()
        {
            AudioManager.instance.PlaySFX(SoundId.PlayerDeath);
            GameObject newDeath = Instantiate(deadPlyaer_fx, currentPlayer.transform.position, currentPlayer.transform.rotation);
            Destroy(newDeath, 0.4f);
            Destroy(currentPlayer);
        }
    }
}
