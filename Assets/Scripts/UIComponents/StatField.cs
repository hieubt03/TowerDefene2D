using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatField : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    public string statFieldName;
    public void SetStatFieldSprite(int filedPoint) {
        switch (filedPoint) {
            case 0:
                image.sprite = sprites[0];
                break;
            case 1:
                image.sprite = sprites[1];
                break;
            case 2:
                image.sprite = sprites[2];
                break;
            case 3:
                image.sprite = sprites[3];
                break;
            case 4:
                image.sprite = sprites[4];
                break;
            case 5:
                image.sprite = sprites[5];
                break;
            case 6:
                image.sprite = sprites[6];
                break;
            case 7:
                image.sprite = sprites[7];
                break;
            case 8:
                image.sprite = sprites[8];
                break;
            case 9:
                image.sprite = sprites[9];
                break;
            case 10:
                image.sprite = sprites[10];
                break;
            }
    }
    public void OnUpgradeButtonClicked() {
        GameData gameData = DataPersistenceManager.instance.gameData;
        if (gameData.totalPoint >= 1) {
            switch (statFieldName) {
                case "arrowDamageScale" : 
                    if (gameData.arrowDamageScale < 10) {
                        gameData.SpendPoint();
                        gameData.arrowDamageScale += 1;
                        SetStatFieldSprite(gameData.arrowDamageScale);
                    }
                    break;
                case "arrowFireRate" : 
                    if (gameData.arrowSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.arrowSpeed += 1;
                        SetStatFieldSprite(gameData.arrowSpeed);
                    }
                    break;
                case "arrowAttackSpeed" : 
                    if (gameData.arrowAttackSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.arrowAttackSpeed += 1;
                        SetStatFieldSprite(gameData.arrowAttackSpeed);
                    };
                    break;
                case "bulletDamageScale" : 
                    if (gameData.bulletDamageScale < 10) {
                        gameData.SpendPoint();
                        gameData.bulletDamageScale += 1;
                        SetStatFieldSprite(gameData.bulletDamageScale);
                    }
                    break;
                case "bulletFireRate" : 
                    if (gameData.bulletSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.bulletSpeed += 1;
                        SetStatFieldSprite(gameData.bulletSpeed);
                    }
                    break;
                case "bulletAttackSpeed" : 
                    if (gameData.bulletAttackSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.bulletAttackSpeed += 1;
                        SetStatFieldSprite(gameData.bulletAttackSpeed);
                    }
                    break;
                case "bombDamageScale" : 
                    if (gameData.bombDamageScale < 10) {
                        gameData.SpendPoint();
                        gameData.bombDamageScale += 1;
                        SetStatFieldSprite(gameData.bombDamageScale);
                    }
                    break;
                case "bombFireRate" : 
                    if (gameData.bombSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.bombSpeed += 1;
                        SetStatFieldSprite(gameData.bombSpeed);
                    }
                    break;
                case "bombAttackSpeed" : 
                    if (gameData.bombAttackSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.bombAttackSpeed += 1;
                        SetStatFieldSprite(gameData.bombAttackSpeed);
                    }
                    break;
                case "lightningDamageScale" : 
                    if (gameData.lightballDamageScale < 10) {
                        gameData.SpendPoint();
                        gameData.lightballDamageScale += 1;
                        SetStatFieldSprite(gameData.lightballDamageScale);
                    }
                    break;
                case "lightningFireRate" : 
                    if (gameData.lightbalSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.lightbalSpeed += 1;
                        SetStatFieldSprite(gameData.lightbalSpeed);
                    }
                    break;
                case "lightningAttackSpeed" : 
                    if (gameData.lightballAttackSpeed < 10) {
                        gameData.SpendPoint();
                        gameData.lightballAttackSpeed += 1;
                        SetStatFieldSprite(gameData.lightballAttackSpeed);
                    }
                    break;
            }
        }
    }
}
