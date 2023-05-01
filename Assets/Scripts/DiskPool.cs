using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskPool : MonoBehaviour
{

    public static DiskPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;


    void Awake(){
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++){
            if(!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        GameObject tmp = Instantiate(objectToPool);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
        amountToPool++;
        return tmp;
    }

    public void SpeedUp(){
        for(int i = 0; i < amountToPool; i++){
            if(pooledObjects[i].activeInHierarchy)
                pooledObjects[i].GetComponent<DiskMovement>().SpeedUp();
        }
    }
}
