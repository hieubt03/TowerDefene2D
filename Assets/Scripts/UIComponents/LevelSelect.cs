using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    [SerializeField] private string levelSelectId = "";
    public void SetLevelSelectSprite(GameData data) {
        if (data == null) {
            image.sprite = sprites[0];
        } else {
            switch (data.levelStar) {
                case int n when n > 10 && n <= 15:
                    image.sprite = sprites[3];
                    break;
                case int n when n > 5 && n <= 10:
                    image.sprite = sprites[2];
                    break;
                case int n when n > 0 && n <= 5:
                    image.sprite = sprites[0];
                    break;
            }
        }
    }
    public string GetLevelSelectId() {
        return levelSelectId;
    }
}
