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
        int star = healthPoint / 5;
        switch (star) {
            case 3:
                image.sprite = sprites[0];
                break;
            case 2:
                image.sprite = sprites[1];
                break;
            case 1:
                image.sprite = sprites[2];
                break;
        }
    }
    public void SetStar(int star) {
        this.star = star;
    }
    public void LoadData(LevelData data) {
        star = data.levelStar;
    }
    public void SaveData(LevelData data) {
        data.levelStar = star;
    }
}