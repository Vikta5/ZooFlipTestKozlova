using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlatformMove : MonoBehaviour
{
    float speed = 0.4f;
    bool isMove = false, jump = false;
    GameObject player;
    public GameObject pastPlatform { get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.PlayerDeath)
        {
            GetComponent<PlatformShake>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) > 0.1f)
            {
                transform.position += transform.up * -1 * 6 * Time.fixedDeltaTime;
                player.transform.position += player.transform.up * -1 * 6 * Time.fixedDeltaTime;
            }

            else
            {
                jump = false;
                isMove = true;
                GeneratorPlatform.instance.Generate(player.transform.position.y, this.gameObject);
            }
        }

        if (isMove)
        {
            transform.position += transform.up * -1 * speed * Time.fixedDeltaTime;
        }
    }

    public void Move()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            player = collision.gameObject;
            jump = true;
            isMove = false;

            if (!GeneratorPlatform.instance.isFirstPlatform && pastPlatform)
            {
                pastPlatform.GetComponent<PlatformMove>().StartCoroutine(MoveDown());
            }

            else
            {
                GeneratorPlatform.instance.isFirstPlatform = false;
            }

            if (GetComponentInChildren<Text>().color.a != 1)
            {
                GetComponentInChildren<Text>().text = (GameController.instance.currentPlatformCount + 1).ToString();
                GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);
                GameController.instance.currentPlatformCount++;
            }

            if (GameController.instance.currentPlatformCount > PlayerPrefs.GetInt("record"))
            {
                GameController.instance.NewRecord();
            }

        }
    }

    IEnumerator MoveDown()
    {
        while (pastPlatform.transform.position.y > -10f)
        {
            pastPlatform.transform.position += pastPlatform.transform.up * -1 * speed *10 * Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}