using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeneratorCoin : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    GameObject coin;
    int newCoin;

    // Start is called before the first frame update
    void Start()
    {
        newCoin = Random.Range(1, 6);   
    }

    void Generate()
    {
        coin = Instantiate(coinPrefab, new Vector3(0, Random.Range(2.6f, 3f), 0), Quaternion.identity) as GameObject;
    }

    public void NewCoin()
    {
        newCoin--;

        if (newCoin == 0)
        {
            if (coin == null)
            {
                Generate();
                newCoin = Random.Range(1, 6);
            }

            else
            {
                newCoin = 1;
            }
        }
    }

}
