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
        void AddSceneObj(SceneObject obj);
        ///// <summary>
        ///// �������ߣ����ǣ��ڸýڵ���ʱ��ʾ����
        ///// </summary>
        ///// <param name="camera"></param>
        //void Inside(Camera camera);
        ///// <summary>
        ///// �������ߣ����ǣ����ڸýڵ���ʱ��������
        ///// </summary>
        ///// <param name="camera"></param>
        //void Outside(Camera camera);
    }

}
