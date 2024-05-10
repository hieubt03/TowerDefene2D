using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUiManager : MonoBehaviour
{
    public TextMeshProUGUI totalPoint;
    public List<StatField> statFields;
    public List<UpgradeProtectileIcon> upgradeProtectileIcons;
    void Awake() {
        SetTotalPoint(DataPersistenceManager.instance.projectileData.totalPoint);
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
        string selectedProtectile = DataPersistenceManager.instance.projectileData.curentSelectedProtectile;
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
        ProjectileData projectileData = DataPersistenceManager.instance.projectileData;
        string selectedProtectile = projectileData.curentSelectedProtectile;
        foreach(UpgradeProtectileIcon upgradeProtectileIcon in upgradeProtectileIcons) {
            if (upgradeProtectileIcon.projectileName != selectedProtectile) {
                upgradeProtectileIcon.setSprite(false);
            }
        }    
        statFieldMenu(selectedProtectile);
    }
    
    private void statFieldMenu(string selectedProtectile) {
        ProjectileData projectileData = DataPersistenceManager.instance.projectileData;
        switch (selectedProtectile) {
            case "Arrow":
                statFields[0].statFieldName = "arrowDamageScale";
                statFields[0].SetStatFieldSprite(projectileData.arrowDamageScale);
                statFields[1].statFieldName = "arrowFireRate";
                statFields[1].SetStatFieldSprite(projectileData.arrowSpeed);
                statFields[2].statFieldName = "arrowAttackSpeed";
                statFields[2].SetStatFieldSprite(projectileData.arrowAttackSpeed);
                break;
            case "Bullet":
                statFields[0].statFieldName = "bulletDamageScale";
                statFields[0].SetStatFieldSprite(projectileData.bulletDamageScale);
                statFields[1].statFieldName = "bulletFireRate";
                statFields[1].SetStatFieldSprite(projectileData.bulletSpeed);
                statFields[2].statFieldName = "bulletAttackSpeed";
                statFields[2].SetStatFieldSprite(projectileData.bulletAttackSpeed);
                break;
            case "Bomb":
                statFields[0].statFieldName = "bombDamageScale";
                statFields[0].SetStatFieldSprite(projectileData.bombDamageScale);
                statFields[1].statFieldName = "bombFireRate";
                statFields[1].SetStatFieldSprite(projectileData.bombSpeed);
                statFields[2].statFieldName = "bombAttackSpeed";
                statFields[2].SetStatFieldSprite(projectileData.bombAttackSpeed);
                break;
            case "Lightning":
                statFields[0].statFieldName = "lightningDamageScale";
                statFields[0].SetStatFieldSprite(projectileData.lightballDamageScale);
                statFields[1].statFieldName = "lightningFireRate";
                statFields[1].SetStatFieldSprite(projectileData.lightbalSpeed);
                statFields[2].statFieldName = "lightningAttackSpeed";
                statFields[2].SetStatFieldSprite(projectileData.lightballAttackSpeed);
                break;
        }
    }
    // OnprojectIconClick
    private int GetTotalPoint() {
        int point;
        int.TryParse(totalPoint.text, out point);
        return point;
    }

    private void SetTotalPoint(int point) {
        DataPersistenceManager.instance.projectileData.totalPoint = point;
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
        ProjectileData projectileData = DataPersistenceManager.instance.projectileData;
        tempPoint = tempPoint + projectileData.arrowSpeed
                              + projectileData.arrowAttackSpeed
                              + projectileData.arrowDamageScale
                              + projectileData.bulletAttackSpeed
                              + projectileData.bulletSpeed
                              + projectileData.bulletDamageScale
                              + projectileData.bombAttackSpeed
                              + projectileData.bombSpeed
                              + projectileData.bombDamageScale
                              + projectileData.lightballAttackSpeed
                              + projectileData.lightbalSpeed
                              + projectileData.lightballDamageScale;
        SetTotalPoint(tempPoint);
        projectileData.arrowAttackSpeed = 0;
        projectileData.arrowSpeed = 0;
        projectileData.arrowDamageScale = 0;
        projectileData.bulletAttackSpeed = 0;
        projectileData.bulletSpeed = 0;
        projectileData.bulletDamageScale = 0;
        projectileData.bombAttackSpeed = 0;
        projectileData.bombSpeed = 0;
        projectileData.bombDamageScale = 0;
        projectileData.lightballAttackSpeed = 0;
        projectileData.lightbalSpeed = 0;
        projectileData.lightballDamageScale = 0;
        statFieldMenu(projectileData.curentSelectedProtectile);
    }
}
