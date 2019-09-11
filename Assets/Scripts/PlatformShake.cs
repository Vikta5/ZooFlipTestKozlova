using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShake : MonoBehaviour
{
    [Tooltip("Радиус дрожания")]
    float shakeAmount = 0.35f;
    [Tooltip("Скорость движения")]
    public float shakeSpeed = 2f;
    [Tooltip("Время дрожания")]
    public float shakeTime = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Vector3 originPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            randomPoint.z = 0;
            randomPoint.y = originPosition.y;
            transform.localPosition = Vector3.Lerp(transform.localPosition, randomPoint, shakeSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            shakeAmount += Time.deltaTime * 5;
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = new Vector3(0, transform.position.y, 0);
    }
}