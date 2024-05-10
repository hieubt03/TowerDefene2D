using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingIcon : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameObject sellPrefab;
    private TextMeshProUGUI price;
    private BuildingTree myTree;

    void OnEnable() {
        EventManager.StartListening("UserClick", UserClick);
    }

    void OnDisable() {
        EventManager.StopListening("UserClick", UserClick);
    }

    void Awake() {
        myTree = transform.GetComponentInParent<BuildingTree>();
        price = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Assert(price && myTree, "Wrong initial parameters");
        if (towerPrefab == null) { 
            gameObject.SetActive(false);
        }
        else {
            if (gameObject.tag == "SellIcon") {
                price.text = sellPrefab.GetComponent<Price>().price.ToString();
            } else {
                price.text = towerPrefab.GetComponent<Price>().price.ToString();
            }          
        }
    }
    private void UserClick(GameObject gameObj, string param) {
        if (gameObj == gameObject) {
            myTree.Build(towerPrefab);
            if (gameObject.tag == "SellIcon") {
                myTree.Sell(sellPrefab);
            }
        }
    }
}
