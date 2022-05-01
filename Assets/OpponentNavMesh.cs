using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentNavMesh : MonoBehaviour
{ 
    [SerializeField] 
    private Transform movePositionTransform;
    
    [SerializeField] 
    private NavMeshAgent navMeshAgent;

    [SerializeField] 
    private Animator animator;

    private GameManager gm;
    private PlayerStates _currentState;
    private float _rand; 
    
    void Start()
    {
        gm = GameManager.instance;
        _rand = Random.value * 12;

    }

    void Update()
    {
        if (gm.gameState==GameStates.Paurkour&&_rand>0)
        {
            navMeshAgent.destination = new Vector3(12-_rand,movePositionTransform.position.y,movePositionTransform.position.z);
            animator.SetBool(PlayerAnims.RunForward.ToString(),true);
            _rand = -1;

        }
       
        
    }

}
