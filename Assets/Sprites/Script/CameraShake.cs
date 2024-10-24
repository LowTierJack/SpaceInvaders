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
            Vector2 offset = Random.insideUnitCircle * amplitude;


            transform.localPosition = new Vector3(offset.x, offset.y, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;


    }


}
