using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pumpkin.SceneSeparate
{
    public interface ITreeNode : INode
    {

        TreeType TreeNodeType { get; set; }

        /// <summary>
        /// ��ӳ������嵽�ڵ�
        /// </summary>
        /// <param name="obj"></param>
        void AddSceneObj(SceneObjectDataMono obj);
    }

}
