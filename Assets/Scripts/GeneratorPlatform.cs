using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPlatform : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [HideInInspector] public bool isFirstPlatform = true;

    GameObject platform;
    float targetY;
    float speed = 6;

    public static GeneratorPlatform Instance
    {
        get
        {
            return Instance;
        }
    }

    public static GeneratorPlatform instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (platform)
        {
            if (Vector3.Distance(platform.transform.position, new Vector3(0, targetY, 0)) > 0.1f)
            {
                platform.transform.position += platform.transform.up * -1 * speed * Time.fixedDeltaTime;
            }

            else
            {
                platform = null;
            }
        }
    }

    public void Generate(float playerPosition, GameObject currentPlatform)
    {
        platform = Instantiate(platformPrefab, transform.position, Quaternion.identity) as GameObject;
        platform.GetComponent<PlatformMove>().pastPlatform = currentPlatform;
        targetY = Random.Range(playerPosition, 1.8f);
        gameObject.GetComponent<GeneratorCoin>().NewCoin();
    }

}
