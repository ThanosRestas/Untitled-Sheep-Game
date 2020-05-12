using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine (ScaleOverTime (30));
        GetComponent<Rigidbody> ().AddForce (new Vector3 (0f, 0f, -0.5f), ForceMode.Impulse);
    }

    IEnumerator ScaleOverTime (float time)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3 (3.0f, 3.0f, 3.0f);

        float currentTime = 0.0f;
        do
        {
            transform.localScale = Vector3.Lerp (originalScale, destinationScale, currentTime);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }
}