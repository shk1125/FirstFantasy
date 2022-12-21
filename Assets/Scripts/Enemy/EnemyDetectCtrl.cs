using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectCtrl : MonoBehaviour
{
    public float detectRadius;

    SphereCollider sphereCollider;
    EnemyCtrl enemyCtrl;



    void Start()
    {
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = detectRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            enemyCtrl.playerCtrl = other.GetComponent<PlayerCtrl>();
            if (enemyCtrl.state != EnemyCtrl.State.ReturnToIdle)
            {
                enemyCtrl.HPBar.gameObject.SetActive(true);
                enemyCtrl.state = EnemyCtrl.State.Follow;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
