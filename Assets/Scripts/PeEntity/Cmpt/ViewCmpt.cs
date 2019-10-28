using System;
using UnityEngine;

namespace Pathea
{
    public abstract class ViewCmpt : PeCmpt
	{
		/// <summary>
		/// �Ƿ���� View ��
		/// </summary>
		public abstract bool hasView { get; }


		/// <summary>
		/// ���� Transform ����. ����ʵ��ʱӦ����֤ View �����ʱ������Ч�� transform ����, ���򷵻� null
		/// </summary>
		public abstract Transform centerTransform { get; }


		/// <summary>
		/// ����λ��. �ɷ����߱�֤���� View �����ʱ���ʴ�����
		/// </summary>
		public Vector3 centerPosition { get { return (centerTransform != null) ? centerTransform.position : transform.position; } }
    }
}