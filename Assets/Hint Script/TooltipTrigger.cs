using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private static LTDescr delay;
    public string header;

    [Multiline()]
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(.7f, () =>
        {
            TooltipSystem.Show(content, header);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }

    //Hides the tooltip when clicking, otherwise it continues to show when opening menus since OnPointerExit won't trigger when adding popups
    public void OnPointerClick(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}

