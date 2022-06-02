/* using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCFieldOfView))]
public class NPCFieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        NPCFieldOfView fov = (NPCFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov._pointOfView.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov._pointOfView.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov._pointOfView.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov._pointOfView.position, fov._pointOfView.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov._pointOfView.position, fov._pointOfView.position + viewAngle02 * fov.radius);

        if (fov.inFOV)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov._pointOfView.position, fov.objectInFOV.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
} */