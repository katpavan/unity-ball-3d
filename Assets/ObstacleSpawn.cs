using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{   
    private GameObject obstacle;
    private Transform playerTransform;
    private float numberOfObstacles = 4;

    // Start is called before the first frame update
    void Start()
    {
        obstacle = Resources.Load<GameObject>("ObstacleObject");
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();  
        
        for(int i = 0; i < numberOfObstacles; i++){
            //Quaternion does rotations, has a w value in addition to x,y,z
            //we have it so it'll rotate to the right direction so it'll be pointed to the player
            Instantiate(obstacle, playerTransform.position + Vector3.forward * (60 + i * 40), Quaternion.Euler(0, 90, 0)); //increase 40 to make it easier and lessen it to make it harder
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
