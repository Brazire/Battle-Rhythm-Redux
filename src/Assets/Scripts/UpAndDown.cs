using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DoUp();
    }

    private void DoUp()
    {
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        while (gameObject.transform.localPosition.y < 40f)
        {
            gameObject.transform.localPosition += new Vector3(0f, 4f, 0f);
            yield return new WaitForSeconds(0.06f);
        }
        float seconds = Random.Range(0, 3);
        yield return new WaitForSeconds(seconds);
        DoDown();
    }

    private void DoDown()
    {
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (gameObject.transform.localPosition.y > -40f)
        {
            gameObject.transform.localPosition -= new Vector3(0f, 4f, 0f);
            yield return new WaitForSeconds(0.06f);
        }
        float seconds = Random.Range(0, 3);
        yield return new WaitForSeconds(seconds);
        DoUp();
    }
}
