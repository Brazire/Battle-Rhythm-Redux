using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomSelectButtonScroll : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private int spacing;

    public void OnSelect(BaseEventData eventData)
    {
        var size = ((transform as RectTransform).rect.size.y) + /*scrollRect.content.GetComponent<VerticalLayoutGroup>().*/spacing;
        var position = Mathf.Abs(gameObject.GetComponent<RectTransform>().anchoredPosition.y) - ((transform as RectTransform).rect.size.y / 2);
        var viewSize = (scrollRect.transform as RectTransform).rect.size.y;
        var realMax = scrollRect.content.rect.height - viewSize;

        //find value for min and max height
        var min = scrollRect.content.anchoredPosition.y;
        var max = Mathf.Clamp(min + viewSize, 0, scrollRect.content.rect.height);

        var anchor = scrollRect.content.anchoredPosition;
        //scroll
        if (position < min)
            anchor.y = Mathf.Clamp(position, 0, realMax);
        else if (position + (transform as RectTransform).rect.size.y > max)
            anchor.y = Mathf.Clamp(position + size - viewSize, 0, realMax);
        
        scrollRect.content.anchoredPosition = anchor;
    }
}
