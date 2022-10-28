using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Pumpkin.SceneSeparate
{
    [DisallowMultipleComponent]
    public class SceneObjectDataMono : MonoBehaviour, ISceneObjectData
    {

        [SerializeField]
        private Vector3 m_BoundsSize;
        [SerializeField]
        private Vector3 m_CenterOffset;

        private Bounds m_Bounds;

        public Bounds Bounds => m_Bounds;

        public uint Depth { get; set; }

        private void Awake()
        {
            m_Bounds = new Bounds(transform.position + m_CenterOffset, 
                new Vector3(transform.localScale.x * m_BoundsSize.x, transform.localScale.y * m_BoundsSize.y, transform.localScale.z * m_BoundsSize.z) );
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Bounds bounds = new Bounds(transform.position + m_CenterOffset,
                new Vector3(transform.localScale.x * m_BoundsSize.x, transform.localScale.y * m_BoundsSize.y, transform.localScale.z * m_BoundsSize.z));

            bounds.DrawBounds(Color.yellow);

            Handles.Label(transform.position, Depth.ToString());
        }
#endif


    }
}

