using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tileArray;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void RandomizeObstacle() 
    { 
        for(int i = 0; i < tileArray.Length; i++) { 
            tileArray[i].SetActive(true); 
        }

        int number = Random.Range(1, 15); //how many tiles do we want to decativate

        for(int i = 0; i < number; i++) { //loop up to that number 
            tileArray[Random.Range(0, tileArray.Length)].SetActive(false); //and deactivate those tiles
        }
    }
}
