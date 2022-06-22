using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;

    public void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null){
            //randomizes the x position of the spawn
            //item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
            item.transform.position = gameConstants.gombaSpawnPointStart;
            item.SetActive(true);
        }
        else
        {
            Debug.Log("Pool out of items");
        }
    }

        void Awake()
    {
        Debug.Log("Im awake");
        for (int j=0; j<2; j++)
        {
            //Debug.Log("SPAWN");
            int randomvalue = Random.Range(0,2);
            //Debug.Log(randomvalue.ToString());
            if (randomvalue==0)
            {
                spawnFromPooler(ObjectType.turtleEnemy);
            }
            else 
            {
                spawnFromPooler(ObjectType.gombaEnemy);
            }
            
            
        }
    }
}
