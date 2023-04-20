using Code.Enemy;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        private static float _radius;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner enemySpawner, GizmoType gizmoType)
        {
            _radius = 0.5f;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(enemySpawner.transform.position, _radius);
        }
    }
}