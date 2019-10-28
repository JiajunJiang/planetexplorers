using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.SqliteClient;

public class PromptData
{

 //"ItemName%"  -  ��������
 //"PlayerName%"  -  ��ɫ����
 //"HeroName%"   -   �ʹ�����
 //"MissionName%"  -  ��������
 //"SkillName%"   -   ��������
 //"ItemNum%"    -    ��������


    public enum PromptType
    {
        PromptType_Unkown,
        PromptType_1,//��������
        PromptType_2,//����ʧ��
        PromptType_3,//���񽻻�  ���ܶ��
        PromptType_4,//ʰȡ��Ʒ  ���ܶ��
        PromptType_5,//�ϳ���Ʒ
        PromptType_6,//ɾ����Ʒ
        PromptType_7,//��ļ
        PromptType_8,//ǲɢ�ʹ�
        PromptType_9,//�ʹ�����
        PromptType_10,//ѧϰ�ϳɼ���
        PromptType_11,//ʹ�ûظ�����ҩ��
        PromptType_12,//ʹ�����ʶȻظ�ʳƷ
        PromptType_13,//������Ʒ
        PromptType_14,//������Ʒ
        PromptType_15,//������Ʒ
        PromptType_16,//����ʱ��ʧ��Ʒ   ���ܶ��
        PromptType_17,//��������ֵ���ͣ�����ֵ<20%
        PromptType_18,//�����ʶȹ��ͣ����ʶ�<20%
        PromptType_19,//�ʹ�����ֵ���ͣ�����ֵ<20%
        PromptType_20,//�ʹ����ʶȹ��ͣ����ʶ�<20%
        PromptType_21,//��������ĵ�����ʾ
        PromptType_22,//����״̬����
        PromptType_23,//����״̬����
        PromptType_24,//�Ѿ�ѧ���ϳɼ����ˣ�������ѧ��
        PromptType_25,//Ǯ����
    }

    public int m_Type;
    public string m_Content;

}

public class PromptRepository
{
    public static Dictionary<int, PromptData> m_PromptMap = new Dictionary<int, PromptData>();

    public static PromptData GetPromptData(int type)
    {
        return m_PromptMap.ContainsKey(type) ? m_PromptMap[type] : null;
    }

    public static string GetPromptContent(int type)
    {
        PromptData data = GetPromptData(type);
        if (data == null)
            return "";

        return data.m_Content;
    }

    public static void LoadData()
    {
        SqliteDataReader reader = LocalDatabase.Instance.ReadFullTable("System_prompt");
        while (reader.Read())
        {
            PromptData data = new PromptData();
            data.m_Type = Convert.ToInt32(reader.GetString(reader.GetOrdinal("id")));
            data.m_Content = reader.GetString(reader.GetOrdinal("hint"));

            m_PromptMap.Add(data.m_Type, data);
        }
    }
}
