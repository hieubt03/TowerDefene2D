using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AiStateAttack : MonoBehaviour, IAiState
{
    public bool targetPriority = true;
    public string passiveState;
    private AiBehavior aiBehavior;
    private GameObject target;
    private List<GameObject> targetsList = new List<GameObject>();
    private IAttack rangedAttack;
    private IAttack lightningAttack;
    private IAttack bombAttack;
    private bool targetLess;
    private Animator anim;
    void Awake() {
        aiBehavior = GetComponent<AiBehavior>();
        anim = GetComponent<Animator>();
        rangedAttack = GetComponent<AttackRanged>() as IAttack;
        lightningAttack = GetComponent<AttackLightning>() as IAttack;
        bombAttack = GetComponent<AttackBomb>() as IAttack;
        Debug.Assert(aiBehavior != null, "Wrong initial parameter");
    }

    public void OnStateEnter(string previousState, string newState) {

    }

    public void OnStateExit(string previousState, string newState) {
        LoseTarget();
    }

    void FixedUpdate() {
        if ((target == null) && (targetsList.Count > 0)) {
            target = GetPriorityTarget();
        }
        if (target == null) {
            if (targetLess == false) {
                targetLess = true;
            } else {
                aiBehavior.ChangeState(passiveState);
                if (anim != null) {
                    anim.SetTrigger("Idle");
                }
            }
        }
    }
    private GameObject GetPriorityTarget() {
        GameObject res = null;
        if (targetPriority == true) {
            float minPathDistance = float.MaxValue;
            foreach (GameObject ai in targetsList) {
                if (ai != null) {
                    AiStateMove aiStateMove = ai.GetComponent<AiStateMove>();
                    float distance = aiStateMove.GetRemainingPath();
                    if (distance < minPathDistance) {
                        minPathDistance = distance;
                        res = ai;
                    }
                }
            }
        } else {
            res = targetsList[0];
        }
        targetsList.Clear();
        return res;
    }
    
    private void LoseTarget() {
        target = null;
        targetLess = false;
    }

    public void TriggerEnter(Collider2D my, Collider2D other) {

    }

    public void TriggerStay(Collider2D my, Collider2D other) {
        if (target == null) {
            targetsList.Add(other.gameObject);
        } else {
            if (target == other.gameObject) {
                if (rangedAttack != null) {
                    rangedAttack.Attack(other.transform);
                }
                else if (lightningAttack != null) {
                    lightningAttack.Attack(other.transform);
                }
                else if (bombAttack != null) {
                    bombAttack.Attack(other.transform);
                }
            }
        }
    }

    public void TriggerExit(Collider2D my, Collider2D other) {
        if (other.gameObject == target) {
            LoseTarget();
        }
    }
}
