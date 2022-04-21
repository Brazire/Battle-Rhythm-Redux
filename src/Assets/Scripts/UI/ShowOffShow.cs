using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOffShow : MonoBehaviour
{
    //Used to show the show off screen when activating the mechanic.
    private SpriteRenderer renderer;
    
    public void ShowCase()
    {
        gameObject.SetActive(true);
        renderer = transform.GetComponent<SpriteRenderer>();
        renderer.material.color = new Color(1, 1, 1, 1f);
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        BossUIManager.uiManager.StartShake();
        ShowOffManager.soManager.StartShowingOff();
        yield return new WaitForSeconds(3f);
        renderer = transform.GetComponent<SpriteRenderer>();
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
