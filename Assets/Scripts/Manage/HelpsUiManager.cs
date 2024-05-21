using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpsUiManager : MonoBehaviour
{       
    public List<UpgradeProtectileIcon> upgradeProtectileIcons;
    public List<GameObject> dots;
    public Image towerImage;
    public List<Sprite> towerImageList;
    public int dotIndex = 0;
    public TextMeshProUGUI level;
    public TextMeshProUGUI info;
    private void Awake() {
        string selectedProtectile = DataPersistenceManager.instance.gameData.curentSelectedProtectile;
        if (selectedProtectile == "") {
            DataPersistenceManager.instance.gameData.curentSelectedProtectile = "Arrow";
            selectedProtectile = DataPersistenceManager.instance.gameData.curentSelectedProtectile;
            UpdateTowerInfo(dotIndex);
        }
        foreach(UpgradeProtectileIcon upgradeProtectileIcon in upgradeProtectileIcons) {
            if (upgradeProtectileIcon.projectileName != selectedProtectile) {
                upgradeProtectileIcon.setSprite(false);
            } else {
                upgradeProtectileIcon.setSprite(true);
            }
        } 

    }
    void OnEnable() {
        EventManager.StartListening("ProtectileSelected", ProjectileSelected);
        
    }
    void OnDisable() {
        EventManager.StopListening("ProtectileSelected", ProjectileSelected);
    }
    void Start() {
        dotIndex = 0;
        UpdateTowerInfo(dotIndex);
    }
    public void NextButtonClicked() {
        if (dotIndex < 4) dotIndex ++;
        UpdateTowerInfo(dotIndex);
    }
    public void BackButtonClicked() {
        if (dotIndex > 0) dotIndex --;
        UpdateTowerInfo(dotIndex);
    }
    public void ProjectileSelected(GameObject gameObj, string param) {
        GameData gameData = DataPersistenceManager.instance.gameData;
        string selectedProtectile = gameData.curentSelectedProtectile;
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
                switch (DataPersistenceManager.instance.gameData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "This tower is equiped with a precise archer who fires sharp arrows at invaliding zombies. It is effective against flying zombie or not protected zombie";
                        towerImage.sprite = towerImageList[0];
                        break;  
                    case "Bomb" :
                        info.text = "This tower launches a bomb that creates a significant impact within the explosion radius. This tower is only effective against ground zombies";
                        towerImage.sprite = towerImageList[5];
                        break;
                    case "Lightning" :
                        info.text = "This tower launches a lightning ball continuously. This tower is only effective against ground zombies. This tower may initially be weak but the more you upgrade it, the stronger it becomes";
                        towerImage.sprite = towerImageList[10];
                        break;
                    case "Bullet" :
                        info.text = "This tower features a soldier who uses a sniper rifle to fire a bullet that damages all enemies hit from a great distance. It has the longest range but the slowest rate of fire. It is effective against flying zombie or not protected zombie";
                        towerImage.sprite = towerImageList[15];
                        break;
                }
                break;
            case 1:
                switch (DataPersistenceManager.instance.gameData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "The arrow inflict 25% more damage";
                        towerImage.sprite = towerImageList[1];
                        break;
                    case "Bomb" :
                        info.text = "The bomb inflict 25% more damage";
                        towerImage.sprite = towerImageList[6];
                        break;
                    case "Lightning" :
                        info.text = "The lightning balls inflict 12,5% more damage. The tower's fire rate increased";
                        towerImage.sprite = towerImageList[11];
                        break;
                    case "Bullet" :
                        info.text = "The bullet inflict 25% more damage";
                        towerImage.sprite = towerImageList[16];
                        break;
                }
                break;
            case 2:
                switch (DataPersistenceManager.instance.gameData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "The arrow inflict 50% more damage. The tower's attack range and fire rate increases by 25%";
                        towerImage.sprite = towerImageList[2];
                        break;
                    case "Bomb" :
                        info.text = "The bomb inflict 50% more damage. The tower's attack range and fire rate increases by 25%";
                        towerImage.sprite = towerImageList[7];
                        break;
                    case "Lightning" :
                        info.text = "The lightning ball inflict 25% more damage. The tower's attack range increases by 25%. More lightning balls are generated";
                        towerImage.sprite = towerImageList[12];
                        break;
                    case "Bullet" :
                        info.text = "The bullet inflict 50% more damage. The tower's attack range and fire rate increases by 25%";
                        towerImage.sprite = towerImageList[17];
                        break;
                }
                break;
            case 3:
                switch (DataPersistenceManager.instance.gameData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "The arrow inflict 75% more damage";
                        towerImage.sprite = towerImageList[3];
                        break;
                    case "Bomb" :
                        info.text = "The bomb inflict 75% more damage";
                        towerImage.sprite = towerImageList[8];
                        break;
                    case "Lightning" :
                        info.text = "The lightning ball inflict 37,5% more damage";
                        towerImage.sprite = towerImageList[13];
                        break;
                    case "Bullet" :
                        info.text = "The bomb inflict 75% more damage";
                        towerImage.sprite = towerImageList[18];
                        break;
                }
                break;
            case 4:
                switch (DataPersistenceManager.instance.gameData.curentSelectedProtectile) {
                    case "Arrow" :
                        info.text = "The arrow inflict 100% more damage. The tower's attack range increases by 50%. Now the tower can now fire continuously";
                        towerImage.sprite = towerImageList[4];
                        break;
                    case "Bomb" :
                        info.text = "The bomb inflict 100% more damage. The tower's attack range increases by 50%. Now the tower cause significant damage in long distance";
                        towerImage.sprite = towerImageList[9];
                        break;
                    case "Lightning" :
                        info.text = "The lightning ball inflict 50% more damage. The tower's attack range increases by 50%. Now the tower can generate mutiple lightning ball";
                        towerImage.sprite = towerImageList[14];
                        break;
                    case "Bullet" :
                        info.text = "The bomb inflict 100% more damage. The tower's attack range increases by 50%. Now the tower deal massive damage in the area";
                        towerImage.sprite = towerImageList[19];
                        break;
                }
                break;
        }
    }
}
