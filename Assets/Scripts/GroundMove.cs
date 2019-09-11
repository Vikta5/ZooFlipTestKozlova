using UnityEngine;

public class GroundMove : MonoBehaviour
{
    float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        if (!GeneratorPlatform.instance.isFirstPlatform)
        {
            transform.position += transform.up * -1 * speed * Time.fixedDeltaTime;
        }
    }
}