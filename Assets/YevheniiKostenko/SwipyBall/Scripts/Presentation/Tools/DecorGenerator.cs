using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor; 
#endif

namespace YevheniiKostenko.SwipyBall.Presentation.Tools
{
    public class DecorGenerator : MonoBehaviour
    {
        [SerializeField]
        private Transform _decorParent;
        [SerializeField]
        private Transform[] _decorPoints;
        [SerializeField]
        private GameObject[] _decorPrefabs;
        
        #if UNITY_EDITOR
        public void GenerateDecors()
        {
            if (_decorParent == null || _decorPoints == null || _decorPoints.Length == 0 || _decorPrefabs == null || _decorPrefabs.Length == 0)
                return;

            Undo.IncrementCurrentGroup();
            int group = Undo.GetCurrentGroup();
            Undo.SetCurrentGroupName("Generate Decors");

            Undo.RegisterFullObjectHierarchyUndo(_decorParent.gameObject, "Generate Decors");

            ClearExistingDecors();

            int numberOfPoints = Random.Range(1, _decorPoints.Length + 1);
            HashSet<int> usedPoints = new HashSet<int>();

            for (int i = 0; i < numberOfPoints; i++)
            {
                int pointIndex;
                do
                {
                    pointIndex = Random.Range(0, _decorPoints.Length);
                } while (usedPoints.Contains(pointIndex));

                usedPoints.Add(pointIndex);
                Transform decorPoint = _decorPoints[pointIndex];

                GameObject decorPrefab = _decorPrefabs[Random.Range(0, _decorPrefabs.Length)];
                GameObject decorObject = PrefabUtility.InstantiatePrefab(decorPrefab, _decorParent) as GameObject;
                if (decorObject == null)
                    continue;

                Undo.RegisterCreatedObjectUndo(decorObject, "Generate Decors");

                decorObject.transform.SetPositionAndRotation(decorPoint.position, decorPoint.rotation);
            }

            EditorUtility.SetDirty(_decorParent);
            EditorUtility.SetDirty(this);

            Undo.CollapseUndoOperations(group);
        }

        private void ClearExistingDecors()
        {
            for (int i = _decorParent.childCount - 1; i >= 0; i--)
            {
                Transform child = _decorParent.GetChild(i);
                Undo.DestroyObjectImmediate(child.gameObject);
            }
        }
        
        #endif
    }
}