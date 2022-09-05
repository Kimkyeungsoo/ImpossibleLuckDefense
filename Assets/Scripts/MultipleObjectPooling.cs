using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectPooling : MonoBehaviour
{
    public static MultipleObjectPooling instance;

    public GameObject[] poolPrefabs;    // Prefabs
    public int poolingCount;            // 각각 Prefab 생성할 숫자

    private Dictionary<object, List<GameObject>> pooledObjects = new Dictionary<object, List<GameObject>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
        CreateMultiplePoolObjects();
    }

    public void CreateMultiplePoolObjects()
    {
        for (int i = 0; i < poolPrefabs.Length; i++)
        {
            for (int j = 0; j < poolingCount; j++)
            {
                if (!pooledObjects.ContainsKey(poolPrefabs[i].name))
                {
                    List<GameObject> newList = new List<GameObject>();
                    pooledObjects.Add(poolPrefabs[i].name, newList);
                }

                GameObject newDoll = Instantiate(poolPrefabs[i], transform);
                newDoll.SetActive(false);
                pooledObjects[poolPrefabs[i].name].Add(newDoll);
            }
        }
    }

    public GameObject GetPooledObject(string _name)
    {
        if (pooledObjects.ContainsKey(_name))
        {
            for (int i = 0; i < pooledObjects[_name].Count; i++)
            {
                if (!pooledObjects[_name][i].activeSelf)
                {
                    return pooledObjects[_name][i];
                }
            }

            int beforeCreateCount = pooledObjects[_name].Count;

            return pooledObjects[_name][beforeCreateCount];
        }
        else
        {
            return null;
        }
    }
    // 출처: https://simppen-gamedev.tistory.com/6 [게임 개발 초보자들을 위한:티스토리]
}
