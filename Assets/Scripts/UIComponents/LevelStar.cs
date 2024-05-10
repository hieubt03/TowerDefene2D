using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStar : MonoBehaviour, IDataPersistence
{   
    private int star;
    public Image image;
    public List<Sprite> sprites;
    public void SetSprite(int healthPoint) {
        switch (healthPoint) {
            case int n when n > 10 && n <= 15:
                image.sprite = sprites[0];
                break;
            case int n when n > 5 && n <= 10:
                image.sprite = sprites[1];
                break;
            case int n when n > 0 && n <= 5:
                image.sprite = sprites[2];
                break;
        }
    }
    public void SetStar(int star) {
        this.star = star;
    }
    public void LoadData(GameData data) {
        star = data.levelStar;
    }
    public void SaveData(GameData data) {
        data.levelStar = star;
    }
}