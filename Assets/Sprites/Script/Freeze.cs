using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [Range(0f, 1f)]
    public float duration = 1f;
    bool isFrozen = false;
    float pendingFreezeDuration = 0f;

    // Update is called once per frame
    void Update()
    {
        if (pendingFreezeDuration > 0 && !isFrozen)
        {
            StartCoroutine(DoFreeze()); Debug.Log("frez");
        }
    }

    public void Freeza()
    {
        pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        pendingFreezeDuration = 0;
        isFrozen = false;

    }
}
