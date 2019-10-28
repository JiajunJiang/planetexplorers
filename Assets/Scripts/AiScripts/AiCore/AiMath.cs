using UnityEngine;
using System;
using System.Collections;

namespace AiAsset
{
    public class AiMath
    {

        public static bool Raycast(Vector3 start, Vector3 end, LayerMask layer)
        {
            float distance = Vector3.Distance(start, end);
            Vector3 direction = start - end;
            return Physics.Raycast(start, direction, distance, layer);
        }

        public static bool Raycast(Vector3 start, Vector3 end, out RaycastHit hitInfo, LayerMask layer)
        {
            float distance = Vector3.Distance(start, end);
            Vector3 direction = start - end;
            return Physics.Raycast(start, direction, out hitInfo, distance, layer);
        }

        public static Vector3 ProjectOntoPlane(Vector3 v, Vector3 normal)
        {
            return v - Vector3.Project(v, normal);
        }

        public static float ProjectDistance(Vector3 v, Vector3 normal)
        {
            return (v - Vector3.Project(v, normal)).magnitude;
        }

        public static float ProjectDistance(Vector3 position1, Vector3 position2, Vector3 normal)
        {
            Vector3 v = position2 - position1;
            return (v - Vector3.Project(v, normal)).magnitude;
        }

        public static float InverseAngle(Transform local, Vector3 dir)
        {
            if (local == null || dir == Vector3.zero)
            {
                Debug.LogWarning("local || dir is error");
                return 0.0f;
            }

            Vector3 dirProject = AiMath.ProjectOntoPlane(dir, local.transform.up); 

            return Vector3.Angle(local.forward, dirProject);
        }

        public static float Dot(Transform self, Transform target)
        {
            Vector3 direction = target.position - self.position;
            Vector3 forward = Vector3.Project(direction, self.forward);
            Vector3 right = Vector3.Project(direction, self.right);

            Vector3 newDirection = forward + right;

            return Vector3.Dot(newDirection.normalized, self.forward);
        }

        public static bool IsNumberic(string message, out int result)
        {
            //�ж��Ƿ�Ϊ�����ַ��� 
            //�ǵĻ�����ת��Ϊ���ֲ�������Ϊout���͵����ֵ������true, ����Ϊfalse 
            result = -1;   //result ����Ϊout �������ֵ 
            try
            {
                //�������ַ�����Ϊ������4ʱ���������ֶ�����ת������ѡһ�� 
                //���λ������4�Ļ�����ѡ��Convert.ToInt32() ��int.Parse() 

                //result = int.Parse(message); 
                //result = Convert.ToInt16(message); 
                result = Convert.ToInt32(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
