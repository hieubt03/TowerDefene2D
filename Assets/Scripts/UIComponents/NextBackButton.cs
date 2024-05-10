using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextBackButton : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    void OnMouseOver() {
        image.sprite = sprites[1];
        Debug.Log("Mouse Over");
    }

    void OnMouseExit() {
        image.sprite = sprites[0];
    }
    public void OnNextButtonClick() {
        EventManager.TriggerEvent("NextButtonClicked", null, null);
    }

    public void OnBackButtonClick() {
        EventManager.TriggerEvent("BackButtonClicked", null, null);
    }
}
