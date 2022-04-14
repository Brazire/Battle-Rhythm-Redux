using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOffShow : MonoBehaviour
{
    public void ShowCase()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        BossUIManager.uiManager.StartShake();
        ShowOffManager.soManager.StartShowingOff();
        yield return new WaitForSeconds(3f);
        SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();
        for (float i = renderer.material.color.a; i >= 0f; i -= 0.1f)
        {
            Color newColor = new Color(1, 1, 1, i);
            renderer.material.color = newColor;
            yield return new WaitForSeconds(0.01f);
        }
        renderer.material.color = new Color(1, 1, 1, 0f);
        yield return new WaitForSeconds(1f);
        ShowOffManager.soManager.SetPlaying(true);
        gameObject.SetActive(false);
    }
}
