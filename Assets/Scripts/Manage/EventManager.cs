using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class MyEvent : UnityEvent<GameObject, string> {

}

// Message system.

public class EventManager : MonoBehaviour {
    private Dictionary<string, MyEvent> eventDictionary;
    private static EventManager eventManager;
    public static EventManager instance {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }  
    }
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, MyEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<GameObject, string> listener) {
        MyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new MyEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<GameObject, string> listener) {
        if (eventManager == null) return;
        MyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName,GameObject gameObject, string param) {
        MyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(gameObject, param);
        }
    }
}
