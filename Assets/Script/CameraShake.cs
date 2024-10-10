using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraShake : MonoBehaviour
{



    public IEnumerator Shake(float shakeTime, float amplitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsedTime = 0f;
        while (elapsedTime < shakeTime)
        {

            float x = Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI)) * amplitude;
            float y = Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI)) * amplitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;


    }


}
