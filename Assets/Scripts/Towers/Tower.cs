using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject buildingTreePrefab;
    private UiManager uiManager;
    private Canvas canvas;
    private Collider2D bodyCollider;
    private BuildingTree activeBuildingTree;

    void OnEnable() {
        EventManager.StartListening("GamePaused", GamePaused);
        EventManager.StartListening("UserClick", UserClick);
    }

    void OnDisable() {
        EventManager.StopListening("GamePaused", GamePaused);
        EventManager.StopListening("UserClick", UserClick);
    }

    void Awake() {
        uiManager = FindObjectOfType<UiManager>();
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canv in canvases) {
            if (canv.CompareTag("LevelUI")) {
                canvas = canv;
                break;
            }
        }
        bodyCollider = GetComponent<Collider2D>();
        Debug.Assert(uiManager && canvas && bodyCollider, "Wrong initial parameters");
    }

    private void OpenBuildingTree() {
        if (buildingTreePrefab != null) {
            activeBuildingTree = Instantiate<GameObject>(buildingTreePrefab, canvas.transform).GetComponent<BuildingTree>();
            Vector3 position = transform.position;
            if (position.x < -4.35) { position.x = -4.35F; };
            if (position.x > 4.35) { position.x = 4.35F; };
            position.y = position.y + 2;
            activeBuildingTree.transform.position = Camera.main.WorldToScreenPoint(position);
            activeBuildingTree.myTower = this;
            bodyCollider.enabled = false;
        }
    }

    private void CloseBuildingTree() {
        if (activeBuildingTree != null) {
            Destroy(activeBuildingTree.gameObject);
            bodyCollider.enabled = true;
        }
    }

    public void BuildTower(GameObject towerPrefab) {
        CloseBuildingTree();
        Price price = towerPrefab.GetComponent<Price>();
        if (uiManager.SpendGold(price.price) == true) {
            GameObject newTower = Instantiate<GameObject>(towerPrefab, transform.parent);
            newTower.transform.position = transform.position;
            newTower.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }
    public void SellTower(GameObject towerPrefab){
        Price price = towerPrefab.GetComponent<Price>();
        if (price != null) {
            uiManager.AddGold(price.price);
        }
    }

    private void GamePaused(GameObject gameObj, string param) {
        if (param == bool.TrueString) {
            CloseBuildingTree();
            bodyCollider.enabled = false;
        }
        else {
            bodyCollider.enabled = true;
        }
    }

    private void UserClick(GameObject gameObj, string param) {
        if (gameObj == gameObject) {
            if (activeBuildingTree == null) {
                OpenBuildingTree();
            }
        }
        else {
            CloseBuildingTree();
        }
    }
}
