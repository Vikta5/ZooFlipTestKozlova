using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public int currentPlatformCount { get; set; }

    [SerializeField] Text recordText, coinsText;
    [SerializeField] GameObject finishPanel;

    [SerializeField] List<Color> backgroundColors = new List<Color>();

    public bool PlayerDeath { get; private set; }
    public static GameController Instance
    {
        get
        {
            return Instance;
        }
    }

    public static GameController instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        recordText.text = PlayerPrefs.GetInt("record").ToString();
        coinsText.text = PlayerPrefs.GetInt("coins").ToString();
        currentPlatformCount = 0;
        PlayerDeath = false;
        finishPanel.SetActive(false);
        StartCoroutine(BackGroundColor());
    }
    public void NewRecord()
    {
        PlayerPrefs.SetInt("record", PlayerPrefs.GetInt("record") + 1);
        recordText.text = PlayerPrefs.GetInt("record").ToString();
    }
    public void Newcoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1);
        coinsText.text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void NewGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Death()
    {
        finishPanel.transform.GetChild(0).GetComponentInChildren<Text>().text = currentPlatformCount + " points";
        finishPanel.SetActive(true);
        PlayerDeath = true;
    }

    IEnumerator BackGroundColor()
    {
        yield return new WaitForSeconds(10f);
        Color color = backgroundColors[Random.Range(0, backgroundColors.Count)];
        while (Camera.main.backgroundColor != color)
        {
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, color, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(BackGroundColor());
    }
}