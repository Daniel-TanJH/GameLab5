using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPoolItem> itemsToPool; // types of different object to pool
    public List<ExistingPoolItem> pooledObjects; // list of objects in the pool, regardless of type
    // Start is called before the first frame update
    public static ObjectPooler SharedInstance;
    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<ExistingPoolItem>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                //pickup is a local variable found only within this loop
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent = this.transform;
                ExistingPoolItem e = new ExistingPoolItem(pickup, item.type);
                pooledObjects.Add(e);
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Return item to pool if inactive
    public GameObject GetPooledObject(ObjectType type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type)
            {
                return pooledObjects[i].gameObject;
            }
        }

        foreach(ObjectPoolItem item in itemsToPool)
        {
            if (item.type == type){
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent = this.transform;
                pooledObjects.Add (new ExistingPoolItem(pickup, item.type));
                return pickup;
            }
        }
        return null;
    }
}

public enum ObjectType{
    gombaEnemy = 0,
    turtleEnemy = 1
}

//Allows for customisation within the inspector rather than in the code
//Helper class that does not need to inherit the MonoBehavior
[System.Serializable]
public class ObjectPoolItem
{
    public int amount;
    public GameObject prefab;
    public bool expandPool;
    public ObjectType type;
}

public class ExistingPoolItem{
    public GameObject gameObject;
    public ObjectType type;
    public ExistingPoolItem(GameObject gameobject, ObjectType type){
        //reference input
        this.gameObject = gameobject;
        this.type = type;
    }
}

