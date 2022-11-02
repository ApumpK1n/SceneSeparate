using System;
using System.Collections.Generic;
using UnityEngine;
using Pumpkin.Utility;

namespace Pumpkin.SceneSeparate
{
    public class BVHBuildNode : INode
    {
        public Bounds Bounds { get; set; }
        public BVHBuildNode Left { get; set; }
        public BVHBuildNode Right { get; set; }

        public SceneObjectDataMono SceneObject { get; set; }


        public void CheckBoundIsInCamera(Camera camera)
        {
            // leaf
            if (Left == null && Right == null)
            {
                if (Bounds.CheckBoundIsInCamera(camera))
                {
                    ObjDisplayControl.Instance.Show(SceneObject);
                }
                else
                {
                    ObjDisplayControl.Instance.Hide(SceneObject);
                }
                return;
            }

            if (Bounds.CheckBoundIsInCamera(camera))
            {
                if (Left.Bounds.CheckBoundIsInCamera(camera))
                {
                    Left.CheckBoundIsInCamera(camera);
                }
                else
                {
                    Left.OutsideCamera(camera);
                }

                if (Right.Bounds.CheckBoundIsInCamera(camera))
                {
                    Right.CheckBoundIsInCamera(camera);
                }
                else
                {
                    Right.OutsideCamera(camera);
                }
            }
            else
            {
                OutsideCamera(camera);
            }
           
        }

        public void OutsideCamera(Camera camera)
        {
            if (Left == null && Right == null)
            {
                ObjDisplayControl.Instance.Hide(SceneObject);
                return;
            }

            Left.OutsideCamera(camera);
            Right.OutsideCamera(camera);
        }


#if UNITY_EDITOR
        public void DrawBounds()
        {
            if (Left != null)
            {
                Left.DrawBounds();
            }

            if (Right != null)
            {
                Right.DrawBounds();
            }

            Bounds.DrawBounds(Color.red);
   
        }
#endif
    }
}
