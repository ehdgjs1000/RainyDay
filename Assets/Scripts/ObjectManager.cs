using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject rainPrefab;
    public GameObject itemSPrefab;
    public GameObject itemUPrefab;
    public GameObject lightningPrefab;

    GameObject[] rains;
    GameObject[] itemS;
    GameObject[] itemU;
    GameObject[] lightnings;
    GameObject[] targetPool;

    void Awake()
    {
        rains = new GameObject[500];
        lightnings = new GameObject[10];
        itemS = new GameObject[5];
        itemU = new GameObject[5];

        Genarate();
    }
    void Genarate()
    {
        for(int index = 0; index < rains.Length; index++)
        {
            rains[index] = Instantiate(rainPrefab);
            rains[index].SetActive(false);
        }
        for (int index = 0; index < itemS.Length; index++)
        {
            itemS[index] = Instantiate(itemSPrefab);
            itemS[index].SetActive(false);
        }
        for (int index = 0; index < itemU.Length; index++)
        {
            itemU[index] = Instantiate(itemUPrefab);
            itemU[index].SetActive(false);
        }
        for (int index = 0; index < lightnings.Length; index++)
        {
            lightnings[index] = Instantiate(lightningPrefab);
            lightnings[index].SetActive(false);
        }
    }
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "rain":
                targetPool = rains;
                break;
            case "itemS":
                targetPool = itemS;
                break;
            case "itemU":
                targetPool = itemU;
                break;
            case "lightning":
                targetPool = lightnings;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject DestroyObj()
    {
        for(int index = 0; index < targetPool.Length; index++)
        {
            targetPool[index].SetActive(false);
        }
        return null;
    }
    

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "rain":
                targetPool = rains;
                break;
            case "itemS":
                targetPool = itemS;
                break;
            case "itemU":
                targetPool = itemU;
                break;
        }
        return targetPool;
    }

}
