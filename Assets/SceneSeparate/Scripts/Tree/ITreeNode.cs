using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pumpkin.SceneSeparate
{
    public interface ITreeNode : INode
    {

        TreeType TreeNodeType { get; set; }

        /// <summary>
        /// 添加场景物体到节点
        /// </summary>
        /// <param name="obj"></param>
        void AddSceneObj(SceneObjectDataMono obj);
    }

}
