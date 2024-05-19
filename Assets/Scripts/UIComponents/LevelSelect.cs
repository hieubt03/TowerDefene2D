using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    [SerializeField] private string levelSelectId = "";
    public void SetLevelSelectSprite(LevelData data) {
        if (data == null) {
            image.sprite = sprites[0];
        } else {
            int star = data.levelStar / 5;
            switch (star) {
                case 3:
                    image.sprite = sprites[3];
                    break;
                case 2:
                    image.sprite = sprites[2];
                    break;
                case 1:
                    image.sprite = sprites[1];
                    break;
                case 0:
                    image.sprite = sprites[0];
                    break;
            }
        }
    }
    public string GetLevelSelectId() {
        return levelSelectId;
    }
}
