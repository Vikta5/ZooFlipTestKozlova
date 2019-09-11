using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject ground;
    [SerializeField] Sprite playerDeath;
    Vector3 deathPosition;
    Animator anim;
    Rigidbody rigidbody;

    float timer;
    float jumpSpeed = 4;
    bool isDeath = false;
    public bool isJump { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isJump == false)
        {
            anim.SetTrigger("jumpAnim");
        }

        if (Input.GetMouseButton(0) && isJump == false)
        {
            timer += Time.deltaTime * 4;
        }

        if (Input.GetMouseButtonUp(0) && isJump == false)
        {
            Jump();
        }

        if (isDeath)
        {
            transform.position = Vector3.Lerp(transform.position, deathPosition, 1);
        }
    }

    void Jump()
    {

        anim.SetTrigger("endJump");
        rigidbody.velocity = new Vector3(0, jumpSpeed + timer, 0);
        timer = 0;
        isJump = true;
        AudioManager.instance.Play(AudioManager.Sound.Up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            other.gameObject.GetComponentInParent<PlatformMove>().Move();
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "Finish")
        {
            isDeath = true;
            GameController.instance.Death();
            AudioManager.instance.Play(AudioManager.Sound.Finish);
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerDeath;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rigidbody.sleepThreshold = 1;
            deathPosition = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlatformMove>() || collision.gameObject.GetComponent<GroundMove>())
        {
            AudioManager.instance.Play(AudioManager.Sound.Down);
            GetComponent<ParticleSystem>().Play();
        }
        isJump = false;
    }
}