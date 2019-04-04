using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PCPlatform_MonoBehaviour
{
    #region FollowTargets
    /// <summary>
    /// Gerencia os tipos de sistema de seguir o alvo
    /// </summary>
    void FollowTargetsManager()
    {
        Vector3 newPositionTarget = this.pathfindingTargets.GetChild(this.idTarget).transform.position;

        if (this.canMove)
            if (Vector3.Distance(this.transform.position, newPositionTarget) <= this.targetDistance)
            {
                switch (this.movementType)
                {
                    case MovementType.Normal:
                        this.FollowNormalTargets();
                        break;

                    case MovementType.Random:
                        this.FollowRandomTargets();
                        break;

                    case MovementType.PingPong:
                        this.FollowPingPongTargets();
                        break;
                }

                if (this.limitOfMovements == LimitOfMovements.Yes)
                    this.CheckAmountMovements();

                this.setTime = this.waitingTime;
            }
        this.transform.position = Vector3.MoveTowards(this.transform.position, newPositionTarget, this.speedMove * Time.deltaTime);

    }
    #endregion

    #region LookAtTargetMethod
    /// <summary>
    /// Olha para o alvo
    /// </summary>
    /// <param name="targetPosition">Transform do alvo</param>
    void LookAtTargetMethod(Transform targetPosition)
    {
        switch (this.lookAtTarget)
        {
            case LookAtTarget.LookAt_WithoutX:

                Vector3 newRotation = new Vector3(this.transform.position.x, targetPosition.position.y, this.transform.position.z);
                targetPosition.LookAt(newRotation);

                break;

            case LookAtTarget.LookAt_WithX:

                targetPosition.LookAt(this.transform.position);
                break;
        }
           
        this.transform.rotation  = Quaternion.Slerp(this.transform.rotation, targetPosition.rotation, this.speedLookAtTarget * Time.deltaTime);
    }
    #endregion

    #region CheckAmountMovements
    /// <summary>
    /// Verifica a quantidade de movimentos que pode ser feita
    /// </summary>
    void CheckAmountMovements()
    {
        if (this.numberOfMovements > 0)
            this.canMove = true;
        else
            this.canMove = false;

        switch (this.movementType)
        {
            case MovementType.Normal:
                if (this.idTarget == this.pathfindingTargets.childCount - 1)
                    this.numberOfMovements--;

                break;

            case MovementType.PingPong:
                if (this.endTarget && this.idTarget <= 0)
                    this.numberOfMovements--;
                break;

            case MovementType.Random:
                if (this.amountChild >= this.pathfindingTargets.childCount)
                {
                    this.numberOfMovements--;
                    this.amountChild = 0;
                }
                break;
        }
    }
    #endregion
}