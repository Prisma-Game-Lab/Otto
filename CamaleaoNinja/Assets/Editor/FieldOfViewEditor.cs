using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Visao))]
public class FieldOfViewEditor : Editor {

    // Mostra o raio do campo de visão do inimigo e o angulo da visao no editor
    private void OnSceneGUI()
    {
        Visao fow = (Visao)target;
        Handles.color = Color.white;
        Vector3 viewAngleA = fow.DirectionFromAngle(-fow.AnguloVisao / 2, false);
        Vector3 viewAngleB = fow.DirectionFromAngle(fow.AnguloVisao / 2, false);
        //Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.DistanciaVisao);
        Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.AnguloVisao, fow.DistanciaVisao);


        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.DistanciaVisao);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.DistanciaVisao);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fow.visibleTargets)
        {
            Debug.Log("nunca entro aqui");
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }

    }


}
