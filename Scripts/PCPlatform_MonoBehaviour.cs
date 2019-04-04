using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PCPlatform_MonoBehaviour : MonoBehaviour
{
    [Header("PLATFORM MANAGER")]
    [Space(10)]

    [Tooltip("Ative para mover a plataforma")]
    [SerializeField] private bool movePlatform = true;

    [Tooltip("Transform do grupo de alvos")]
    [SerializeField] private Transform pathfindingTargets;

    [Tooltip("Tipo do movimento")]
    [SerializeField] private MovementType movementType;
    private enum MovementType { Normal, Random, PingPong};

    [Tooltip("Layer que pode colidir")]
    [SerializeField] private LayerMask layerMaskCollision;

    //===================================================================>>

    [Header("VALUES MANAGER")]
    [Space(10)]

    [Tooltip("Velocidade de movimento para o próximo ponto")]
    [Range(0, 100)] [SerializeField]
    private float speedMove = 5;

    [Tooltip("Distância do alvo")]
    [Range(0, 255)] [SerializeField]
    private byte targetDistance = 0;

    [Tooltip("Tempo de espera para se mover pro próximo alvo")]
    [Range(0, 255)] [SerializeField]
    private byte waitingTime = 1;

    //===================================================================>>

    [Header("LIMIT MOVEMENTS")]
    [Space(10)]

    [Tooltip("Limitar movimentos da plataforma")]
    [SerializeField] private LimitOfMovements limitOfMovements;
    private enum LimitOfMovements {No, Yes};

    [Tooltip("Quantidade de vezes que a plataforma pode se mover")]
    [Range(0, 255)] [SerializeField]
    private byte numberOfMovements;

    //===================================================================>>

    [Header("LOOKAT TARGETS")]
    [Space(10)]

    [Tooltip("Olhar para alvo")]
    [SerializeField] private LookAtTarget lookAtTarget;
    private enum LookAtTarget { No, LookAt_WithoutX, LookAt_WithX };

    [Tooltip("Velocidade para olhar para o alvo")]
    [Range(0, 127)] [SerializeField]
    private sbyte speedLookAtTarget = 1;

    //===================================================================>>

    private bool endTarget;
    private byte idTarget;
    private byte amountChild;
    private float setTime;
    private bool canMove = true;

    #region MonoBehaviour
    private void Start()
    {
        this.transform.position = this.pathfindingTargets.GetChild(0).transform.position;
        this.setTime = this.waitingTime;
    }

    private void Update()
    {
        if (this.movePlatform)
        {
            if(this.lookAtTarget != LookAtTarget.No)
                this.LookAtTargetMethod(this.pathfindingTargets.GetChild(this.idTarget).transform);

            if (this.setTime > 0)
                this.setTime -= Time.deltaTime;
            else
                this.FollowTargetsManager();
        }
    }
    #endregion

    #region Collision
    private void OnCollisionStay(Collision collision)
    {
        if ((this.layerMaskCollision.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((this.layerMaskCollision.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            collision.gameObject.transform.parent = null;
        }
    }
    #endregion
}