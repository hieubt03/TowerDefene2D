using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatField : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    public string statFieldName;
    public int filedPoint;
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
    // onclick cua field
    public void OnUpgradeButtonClicked() {
        ProjectileData projectileData = DataPersistenceManager.instance.projectileData;
        if (projectileData.totalPoint >= 1) {
            switch (statFieldName) {
                case "arrowDamageScale" : 
                    if (projectileData.arrowDamageScale < 10) {
                        projectileData.SpendPoint();
                        projectileData.arrowDamageScale += 1;
                        SetStatFieldSprite(projectileData.arrowDamageScale);
                    }
                    break;
                case "arrowFireRate" : 
                    if (projectileData.arrowSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.arrowSpeed += 1;
                        SetStatFieldSprite(projectileData.arrowSpeed);
                    }
                    break;
                case "arrowAttackSpeed" : 
                    if (projectileData.arrowAttackSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.arrowAttackSpeed += 1;
                        SetStatFieldSprite(projectileData.arrowAttackSpeed);
                    };
                    break;
                case "bulletDamageScale" : 
                    if (projectileData.bulletDamageScale < 10) {
                        projectileData.SpendPoint();
                        projectileData.bulletDamageScale += 1;
                        SetStatFieldSprite(projectileData.bulletDamageScale);
                    }
                    break;
                case "bulletFireRate" : 
                    if (projectileData.bulletSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.bulletSpeed += 1;
                        SetStatFieldSprite(projectileData.bulletSpeed);
                    }
                    break;
                case "bulletAttackSpeed" : 
                    if (projectileData.bulletAttackSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.bulletAttackSpeed += 1;
                        SetStatFieldSprite(projectileData.bulletAttackSpeed);
                    }
                    break;
                case "bombDamageScale" : 
                    if (projectileData.bombDamageScale < 10) {
                        projectileData.SpendPoint();
                        projectileData.bombDamageScale += 1;
                        SetStatFieldSprite(projectileData.bombDamageScale);
                    }
                    break;
                case "bombFireRate" : 
                    if (projectileData.bombSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.bombSpeed += 1;
                        SetStatFieldSprite(projectileData.bombSpeed);
                    }
                    break;
                case "bombAttackSpeed" : 
                    if (projectileData.bombAttackSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.bombAttackSpeed += 1;
                        SetStatFieldSprite(projectileData.bombAttackSpeed);
                    }
                    break;
                case "lightningDamageScale" : 
                    if (projectileData.lightballDamageScale < 10) {
                        projectileData.SpendPoint();
                        projectileData.lightballDamageScale += 1;
                        SetStatFieldSprite(projectileData.lightballDamageScale);
                    }
                    break;
                case "lightningFireRate" : 
                    if (projectileData.lightbalSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.lightbalSpeed += 1;
                        SetStatFieldSprite(projectileData.lightbalSpeed);
                    }
                    break;
                case "lightningAttackSpeed" : 
                    if (projectileData.lightballAttackSpeed < 10) {
                        projectileData.SpendPoint();
                        projectileData.lightballAttackSpeed += 1;
                        SetStatFieldSprite(projectileData.lightballAttackSpeed);
                    }
                    break;
            }
        }
    }
}
