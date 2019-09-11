using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject[] echo;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            int rand = Random.Range(0, echo.Length);
            if (player.isJump)
            {
                GameObject instance = (GameObject)Instantiate(echo[rand], transform.position, Quaternion.identity);
                Destroy(instance, 0.4f);
            }
            timeBtwSpawns = startTimeBtwSpawns;

        }

        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
