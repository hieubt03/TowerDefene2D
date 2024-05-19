using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUiManager : MonoBehaviour
{
    public TextMeshProUGUI totalPoint;
    public List<StatField> statFields;
    public List<UpgradeProtectileIcon> upgradeProtectileIcons;
    void Awake() {
        SetTotalPoint(DataPersistenceManager.instance.gameData.totalPoint);
    }
    void OnEnable() {
        EventManager.StartListening("ProtectileSelected", ProjectileSelected);
        EventManager.StartListening("SpendTotalPoint", SpendTotalPoint);
    }
    void OnDisable() {
        EventManager.StopListening("ProtectileSelected", ProjectileSelected);
        EventManager.StopListening("SpendTotalPoint", SpendTotalPoint);
    }
    void Start() {
        string selectedProtectile = DataPersistenceManager.instance.gameData.curentSelectedProtectile;
        if (selectedProtectile == "") {
            selectedProtectile = "Arrow";
        }
        statFieldMenu(selectedProtectile);
        foreach(UpgradeProtectileIcon upgradeProtectileIcon in upgradeProtectileIcons) {
            if (upgradeProtectileIcon.projectileName != selectedProtectile) {
                upgradeProtectileIcon.setSprite(false);
            } else {
                upgradeProtectileIcon.setSprite(true);
            }
        } 
    }
    public void ProjectileSelected(GameObject gameObj, string param) {
        GameData gameData = DataPersistenceManager.instance.gameData;
        string selectedProtectile = gameData.curentSelectedProtectile;
        foreach(UpgradeProtectileIcon upgradeProtectileIcon in upgradeProtectileIcons) {
            if (upgradeProtectileIcon.projectileName != selectedProtectile) {
                upgradeProtectileIcon.setSprite(false);
            }
        }    
        statFieldMenu(selectedProtectile);
    }
    
    private void statFieldMenu(string selectedProtectile) {
        GameData gameData = DataPersistenceManager.instance.gameData;
        switch (selectedProtectile) {
            case "Arrow":
                statFields[0].statFieldName = "arrowDamageScale";
                statFields[0].SetStatFieldSprite(gameData.arrowDamageScale);
                statFields[1].statFieldName = "arrowFireRate";
                statFields[1].SetStatFieldSprite(gameData.arrowSpeed);
                statFields[2].statFieldName = "arrowAttackSpeed";
                statFields[2].SetStatFieldSprite(gameData.arrowAttackSpeed);
                break;
            case "Bullet":
                statFields[0].statFieldName = "bulletDamageScale";
                statFields[0].SetStatFieldSprite(gameData.bulletDamageScale);
                statFields[1].statFieldName = "bulletFireRate";
                statFields[1].SetStatFieldSprite(gameData.bulletSpeed);
                statFields[2].statFieldName = "bulletAttackSpeed";
                statFields[2].SetStatFieldSprite(gameData.bulletAttackSpeed);
                break;
            case "Bomb":
                statFields[0].statFieldName = "bombDamageScale";
                statFields[0].SetStatFieldSprite(gameData.bombDamageScale);
                statFields[1].statFieldName = "bombFireRate";
                statFields[1].SetStatFieldSprite(gameData.bombSpeed);
                statFields[2].statFieldName = "bombAttackSpeed";
                statFields[2].SetStatFieldSprite(gameData.bombAttackSpeed);
                break;
            case "Lightning":
                statFields[0].statFieldName = "lightningDamageScale";
                statFields[0].SetStatFieldSprite(gameData.lightballDamageScale);
                statFields[1].statFieldName = "lightningFireRate";
                statFields[1].SetStatFieldSprite(gameData.lightbalSpeed);
                statFields[2].statFieldName = "lightningAttackSpeed";
                statFields[2].SetStatFieldSprite(gameData.lightballAttackSpeed);
                break;
        }
    }

    private int GetTotalPoint() {
        int point;
        int.TryParse(totalPoint.text, out point);
        return point;
    }

    private void SetTotalPoint(int point) {
        DataPersistenceManager.instance.gameData.totalPoint = point;
        totalPoint.text = point.ToString();
    }

    public void AddTotalPoint(int point) {
        SetTotalPoint(GetTotalPoint() + point);
    }
    public void SpendTotalPoint(GameObject gameObject, string param) {
        SetTotalPoint(GetTotalPoint() - 1);
    }
    public void resetPointUsed() {
        int tempPoint = GetTotalPoint();
        GameData gameData = DataPersistenceManager.instance.gameData;
        tempPoint = tempPoint + gameData.arrowSpeed
                              + gameData.arrowAttackSpeed
                              + gameData.arrowDamageScale
                              + gameData.bulletAttackSpeed
                              + gameData.bulletSpeed
                              + gameData.bulletDamageScale
                              + gameData.bombAttackSpeed
                              + gameData.bombSpeed
                              + gameData.bombDamageScale
                              + gameData.lightballAttackSpeed
                              + gameData.lightbalSpeed
                              + gameData.lightballDamageScale;
        SetTotalPoint(tempPoint);
        gameData.arrowAttackSpeed = 0;
        gameData.arrowSpeed = 0;
        gameData.arrowDamageScale = 0;
        gameData.bulletAttackSpeed = 0;
        gameData.bulletSpeed = 0;
        gameData.bulletDamageScale = 0;
        gameData.bombAttackSpeed = 0;
        gameData.bombSpeed = 0;
        gameData.bombDamageScale = 0;
        gameData.lightballAttackSpeed = 0;
        gameData.lightbalSpeed = 0;
        gameData.lightballDamageScale = 0;
        statFieldMenu(gameData.curentSelectedProtectile);
    }
}
