using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{       
    public List<UpgradeProtectileIcon> upgradeProtectileIcons;
    public List<GameObject> dots;
    public Image towerImage;
    public List<Sprite> towerImageList;
    public int dotIndex = 0;
    public TextMeshProUGUI level;
    public TextMeshProUGUI info;
    private void Awake() {
        UpdateTowerInfo(dotIndex);
    }
    void OnEnable() {
        EventManager.StartListening("NextButtonClicked", NextButtonClicked);
        EventManager.StartListening("BackButtonClicked", BackButtonClicked);
        EventManager.StartListening("ProtectileSelected", ProjectileSelected);
        
    }
    void OnDisable() {
        EventManager.StopListening("NextButtonClicked", NextButtonClicked);
        EventManager.StopListening("BackButtonClicked", BackButtonClicked);
        EventManager.StopListening("ProtectileSelected", ProjectileSelected);
    }
    void Start() {
        string selectedProtectile = DataPersistenceManager.instance.projectileData.curentSelectedProtectile;
        if (selectedProtectile == "") {
            selectedProtectile = "Arrow";
        }
        foreach(UpgradeProtectileIcon upgradeProtectileIcon in upgradeProtectileIcons) {
            if (upgradeProtectileIcon.projectileName != selectedProtectile) {
                upgradeProtectileIcon.setSprite(false);
            } else {
                upgradeProtectileIcon.setSprite(true);
            }
        } 
        dotIndex = 0;
        UpdateTowerInfo(dotIndex);
    }
    private void NextButtonClicked(GameObject gameObj, string param) {
        if (dotIndex < 4) dotIndex ++;
        UpdateTowerInfo(dotIndex);
    }
    private void BackButtonClicked(GameObject gameObj, string param) {
        if (dotIndex > 0) dotIndex --;
        UpdateTowerInfo(dotIndex);
    }
    public void ProjectileSelected(GameObject gameObj, string param) {
        ProjectileData projectileData = DataPersistenceManager.instance.projectileData;
        string selectedProtectile = projectileData.curentSelectedProtectile;
        foreach(UpgradeProtectileIcon upgradeProtectileIcon in upgradeProtectileIcons) {
            if (upgradeProtectileIcon.projectileName != selectedProtectile) {
                upgradeProtectileIcon.setSprite(false);
            }
        }    
        dotIndex = 0;
        UpdateTowerInfo(dotIndex);
    }
    private void UpdateTowerInfo(int dotIndex) {
        level.text = "LEVEL" + (dotIndex + 1).ToString();
        foreach (GameObject dot in dots) {
            if (dotIndex != dots.IndexOf(dot)) {
                dot.SetActive(false);
            } else {
                dot.SetActive(true);
            }
        }
        switch (dotIndex) {
            case 0:
                switch (DataPersistenceManager.instance.projectileData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "Arrow lv 1";
                        towerImage.sprite = towerImageList[0];
                        break;  
                    case "Bomb" :
                        info.text = "Bomb lv 1";
                        towerImage.sprite = towerImageList[5];
                        break;
                    case "Lightning" :
                        info.text = "Lightning lv 1";
                        towerImage.sprite = towerImageList[10];
                        break;
                    case "Bullet" :
                        info.text = "Bullet lv 1";
                        towerImage.sprite = towerImageList[15];
                        break;
                }
                break;
            case 1:
                switch (DataPersistenceManager.instance.projectileData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "Arrow lv 2";
                        towerImage.sprite = towerImageList[1];
                        break;
                    case "Bomb" :
                        info.text = "Bomb lv 2";
                        towerImage.sprite = towerImageList[6];
                        break;
                    case "Lightning" :
                        info.text = "Lightning lv 2";
                        towerImage.sprite = towerImageList[11];
                        break;
                    case "Bullet" :
                        info.text = "Bullet lv 2";
                        towerImage.sprite = towerImageList[16];
                        break;
                }
                break;
            case 2:
                switch (DataPersistenceManager.instance.projectileData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "Arrow lv 3";
                        towerImage.sprite = towerImageList[2];
                        break;
                    case "Bomb" :
                        info.text = "Bomb lv 3";
                        towerImage.sprite = towerImageList[7];
                        break;
                    case "Lightning" :
                        info.text = "Lightning lv 3";
                        towerImage.sprite = towerImageList[12];
                        break;
                    case "Bullet" :
                        info.text = "Bullet lv 3";
                        towerImage.sprite = towerImageList[17];
                        break;
                }
                break;
            case 3:
                switch (DataPersistenceManager.instance.projectileData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "Arrow lv 4";
                        towerImage.sprite = towerImageList[3];
                        break;
                    case "Bomb" :
                        info.text = "Bomb lv 4";
                        towerImage.sprite = towerImageList[8];
                        break;
                    case "Lightning" :
                        info.text = "Lightning lv 4";
                        towerImage.sprite = towerImageList[13];
                        break;
                    case "Bullet" :
                        info.text = "Bullet lv 4";
                        towerImage.sprite = towerImageList[18];
                        break;
                }
                break;
            case 4:
                switch (DataPersistenceManager.instance.projectileData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "Arrow lv 5";
                        towerImage.sprite = towerImageList[4];
                        break;
                    case "Bomb" :
                        info.text = "Bomb lv 5";
                        towerImage.sprite = towerImageList[9];
                        break;
                    case "Lightning" :
                        info.text = "Lightning lv 5";
                        towerImage.sprite = towerImageList[14];
                        break;
                    case "Bullet" :
                        info.text = "Bullet lv 5";
                        towerImage.sprite = towerImageList[19];
                        break;
                }
                break;
        }
    }
}
