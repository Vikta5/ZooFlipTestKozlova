using UnityEngine;
public class Coin : MonoBehaviour
{
    [SerializeField] Sprite coinAdd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            GameController.instance.Newcoins();
            AudioManager.instance.Play(AudioManager.Sound.Coin);
            GetComponent<SpriteRenderer>().sprite = coinAdd;
            Destroy(gameObject, 1.2f);
        }
    }
}
