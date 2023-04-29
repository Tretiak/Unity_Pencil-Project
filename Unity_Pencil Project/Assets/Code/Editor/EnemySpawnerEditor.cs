using Code.Enemy;
using Code.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        private static float _radius;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawnPoint, GizmoType gizmoType)
        {
            _radius = 0.5f;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawnPoint.transform.position, _radius);
        }
    }
}