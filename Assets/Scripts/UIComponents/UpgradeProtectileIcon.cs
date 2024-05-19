using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProtectileIcon : MonoBehaviour
{
    public string projectileName;
    public Image image;
    public List<Sprite> sprites;
    public void setSprite(bool isSelected) {
        if (isSelected) {
            image.sprite = sprites[0];
        } else {
            image.sprite = sprites[1];
        }
    }
    public void OnprojectIconClick() {
        DataPersistenceManager.instance.gameData.curentSelectedProtectile = projectileName;
        setSprite(true);
        EventManager.TriggerEvent("ProtectileSelected", null, null);
    }
}
