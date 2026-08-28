using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Animated : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Vector2 backgroundSpeed;
    [SerializeField] private GameObject playerManager;

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        mesh.material.mainTextureOffset += backgroundSpeed * Time.deltaTime;
    }
}
