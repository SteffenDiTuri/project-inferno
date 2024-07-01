using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject icon; // Assign the icon GameObject in the Inspector

    public void Start(){
        if (icon != null){
            icon.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        icon.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        icon.SetActive(false);
    }
}
