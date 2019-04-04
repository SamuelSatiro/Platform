using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PCPlatform_MonoBehaviour
{
    #region FollowNormalTargets
    /// <summary>
    /// Seguir os alvos normalmente até chegar no último
    /// </summary>
    void FollowNormalTargets()
    {
        if (this.idTarget < this.pathfindingTargets.childCount - 1)
            this.idTarget++;
        else
            this.idTarget = 0;
    }
    #endregion

    #region FollowPingPongTargets
    /// <summary>
    /// Ir e voltar por todos os alvos
    /// </summary>
    void FollowPingPongTargets()
    {
        if (!this.endTarget)
        {
            if (this.idTarget < this.pathfindingTargets.childCount - 1)
                this.idTarget++;
            else
                this.endTarget = true;
        }
        else
        {
            if (this.idTarget > 0)
                this.idTarget--;
            else
                this.endTarget = false;
        }
    }
    #endregion

    #region FollowRandomTargets
    /// <summary>
    /// Ir para alvos aleatoriamente
    /// </summary>
    void FollowRandomTargets()
    {
        if (this.numberOfMovements > 0)
            this.amountChild++;

        int randomIndexTarget = Random.Range(0, this.pathfindingTargets.childCount);
        this.idTarget = (byte)randomIndexTarget;
    }
    #endregion
}