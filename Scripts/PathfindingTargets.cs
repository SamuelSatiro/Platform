using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTargets : MonoBehaviour
{
    [Tooltip("Ativar o Gizmo")]
    [SerializeField] private bool enableGizmo = true;

    [Tooltip("Cor da Linha do Gizmos")]
    [SerializeField] private Color colorLineGizmos;

    [Tooltip("Cor do Cubo do Gizmos")]
    [SerializeField] private Color colorCubeGizmos;

    [Tooltip("Tamanho do Cubo do Gizmos")]
    [SerializeField] private float sizeCubeGizmos;

    private void OnDrawGizmos()
    {     
        if (this.enableGizmo)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
               if(i < this.transform.childCount - 1)
                {
                    Gizmos.color = this.colorLineGizmos;

                    Gizmos.DrawLine(this.transform.GetChild(i).transform.position,
                   this.transform.GetChild(i + 1).transform.position);

                    Gizmos.color = this.colorCubeGizmos;
                    Gizmos.DrawCube(this.transform.GetChild(i+1).transform.position, Vector3.one * this.sizeCubeGizmos);

                    Gizmos.color = Color.white;
                    Gizmos.DrawCube(this.transform.GetChild(0).transform.position, Vector3.one * this.sizeCubeGizmos);
                }
            }
        }
    }
}