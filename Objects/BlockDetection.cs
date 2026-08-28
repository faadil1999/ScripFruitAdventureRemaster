using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetection : MonoBehaviour
{
    [SerializeField] GameObject blockDoor;

    // Start is called before the first frame update
    void Start()
    {
        blockDoor.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            blockDoor.SetActive(true);

        }
    }
}
