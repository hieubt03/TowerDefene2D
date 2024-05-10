using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgent : MonoBehaviour
{
    public float speed = 1f;
    public bool move = true;
    public Vector2 destination;
    [HideInInspector]
    public Vector2 velocity;
    public Vector2 prevPosition;
    
    void OnEnable() {
        prevPosition = transform.position;
    }
    
    void Update() {
        if (move == true) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
        Vector2 velocity = (Vector2)transform.position - prevPosition;
        velocity /= Time.deltaTime;
        prevPosition = transform.position;
    }
}
