using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMove : MonoBehaviour, IAiState
{
    public Pathway pathway;
    public bool loop = false;
    public WayPoint destination;
    private AiBehavior aiBehavior;
    private Animator anim;
    NavAgent navAgent;
    void Awake () {
        aiBehavior = GetComponent<AiBehavior>();
        navAgent = GetComponent<NavAgent>();
        anim = GetComponent<Animator>();
        Debug.Assert(aiBehavior && navAgent && anim, "Wrong initial parameters");
    }

    public void OnStateEnter(string previousState, string newState) {
        if (pathway == null) {
            pathway = FindObjectOfType<Pathway>();
            Debug.Assert(pathway, "Have no path");
        }
        if (destination == null) {
            destination = pathway.GetNearestWayPoint(transform.position);
        }
        navAgent.destination = destination.transform.position;
        navAgent.move = true;
    }

    public void OnStateExit(string previousState, string newState) {
        navAgent.move = false;
    }

    void FixedUpdate() {
        if (destination != null) {
            if ((Vector2)destination.transform.position == (Vector2)transform.position) {
                destination = pathway.GetNextWayPoint(destination, loop);
                if (destination != null) {
                    Vector2 direction = (Vector2)destination.transform.position - (Vector2)navAgent.prevPosition;
                    TriggerTurn(direction);
                    navAgent.destination = destination.transform.position;
                }
            }
        }
    }

    public void TriggerTurn(Vector2 direction) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (anim != null) {
                anim.SetFloat("angle", angle);
                /*
                switch (angle) {
                case float n when n > 60:
                    anim.SetTrigger("BackView");
                    break;
                case float n when n < -60:
                    anim.SetTrigger("FrontView");
                    break;
                default:
                    anim.SetTrigger("SideView");
                    break;
                }
                */
        }

    }

    public float GetRemainingPath() {
        Vector2 distance = destination.transform.position - transform.position;
        return (distance.magnitude + pathway.GetPathDistance(destination));
    }

    public void TriggerEnter(Collider2D my, Collider2D other) {

    }

    public void TriggerStay(Collider2D my, Collider2D other) {

    }

    public void TriggerExit(Collider2D my, Collider2D other) {

    }
}
