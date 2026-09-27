using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject pooleado;
        for (int i = 0; i < amountToPool; i++)
        {
            pooleado = Instantiate(objectToPool);
            pooleado.SetActive(false);
            pooledObjects.Add(pooleado);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        GameObject nuevaBala = Instantiate(objectToPool);
        nuevaBala.SetActive(false);
        pooledObjects.Add(nuevaBala);
        return nuevaBala;
    }
}