using UnityEngine;
using System.Collections;

//�Զ������ű�����Ŀǰ
//�Զ������ű��ƶ��켣, �����½�����ƶ������Ͻ��յ�

//z��������
//^
//||
//||
//||
//||
//=========>x��������

//^=>-------------------------�յ�
//||
//^-----------------------------<=^
//                                          ||
//^=>----------------------------^
//||
//^----------------------------<=^
//                                          ||
//���=>-------------------------^


//�Զ�����camera�����Ĺ���
public class AutoRunCamera : MonoBehaviour
{
    public float m_moveSpd = 25.0f; //������ƶ��ٶ�
    public float m_waitSec = 30.0f; //�ƶ���ָ��λ�ú󣬵ȴ�ʱ��
    Vector3 m_offset = new Vector3(0.0f, 30.0f, 0.0f); //camera��Ը߶�
    public Vector3 m_curPos = new Vector3();
    public int m_curCol = 0; //��ǰsubTerrain���
    public int m_curRow = 0;

    public float m_distX = 0.0f;
    public float m_distZ = 0.0f;
    VoxelEditor m_voxelEditor;
    public float m_elps = 0.0f; //��ǰ�ȴ�ʱ��

    public float m_moveStep = 256.0f; //subTerrain��Χ
    public float  m_terrainSize = 24576.0f; //���η�Χ

    int m_maxRow = 0; //���η�Χ�����ӵ��η�Χ
//    int m_maxRowDec1 = 0;
    int m_maxCol = 0; //���η�Χ�����ӵ��η�Χ
//    int m_maxColDec1 = 0;

    //�Զ�����״̬
    public enum AutoRunState
    {
        eIdle,
        eMove,
        eWaitFillVegetation,
    };

    //camera�ƶ�����, ��+x�ᣬ��-x�ᣬ��+z��
    public enum MoveDir
    {
        eRight,
        eLeft,
        eUp,
    };

    public MoveDir m_eMoveDir = MoveDir.eRight;
    public AutoRunState m_eRS = AutoRunState.eIdle;

    void Start()
    {
        m_voxelEditor = GameObject.Find("Voxel Terrain").GetComponent<VoxelEditor>();
        //int m_maxRow = (int)(m_terrainSize / m_moveStep); //���η�Χ�����ӵ��η�Χ, ���� 24576.0f / 256.0f = 96
        //int m_maxRowDec1 = m_maxRow - 1;
//        int m_maxCol = m_maxRow; //���η�Χ�����ӵ��η�Χ, ���� 24576.0f / 256.0f = 96
//        int m_maxColDec1 = m_maxRowDec1;
    }

    // Update is called once per frame
    void Update()
    {
        //�����ǰ״̬ʱ�ȴ�״̬ʱ
        if (m_eRS == AutoRunState.eIdle)
        {
            //���¼����Ҽ������Զ�����״̬
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_curCol = (int)(transform.position.x / m_moveStep);
                m_curRow = (int)(transform.position.z / m_moveStep);
                m_curCol = Mathf.Clamp(m_curCol, 0, m_maxCol);
                m_curRow = Mathf.Clamp(m_curRow, 0, m_maxRow);

                if (m_curRow == m_maxRow && m_curCol == m_maxCol)
                    Debug.Log("AutoRunCamera::reach end point !!!");
                else
                {
                    m_curPos.x = m_curCol * m_moveStep;// +m_offset.x;
                    m_curPos.z = m_curRow * m_moveStep;// +m_offset.z;
                    m_curPos.y = transform.position.y;
                    transform.position = m_curPos;

                    if (m_curCol == m_maxCol)
                    {
                        m_eMoveDir = MoveDir.eUp;
                        m_distZ = m_curPos.z + m_moveStep;
                    }
                    else
                    {
                        m_eMoveDir = MoveDir.eRight;
                        m_distX = m_curPos.x + m_moveStep;
                        //m_eMoveDir = MoveDir.eLeft;
                        //m_distX = m_curPos.x - m_moveStep;
                    }

                    m_eRS = AutoRunState.eMove;
                }
            }
        }

        //�����ǰ״̬���ƶ�״̬
        if (m_eRS == AutoRunState.eMove)
        {
            m_curPos = this.transform.position;
            float _step = m_moveSpd * Time.deltaTime;

            if (m_eMoveDir == MoveDir.eRight)  //����ƶ���������
            {
                m_curPos.x += _step;

                if (m_curPos.x >= m_distX || m_curPos.x >= m_terrainSize) //8189.0f)
                {
                    m_curPos.x = m_distX;
                    m_eRS = AutoRunState.eWaitFillVegetation;
                    ++m_curCol;
                }
                        
            }
            else if (m_eMoveDir == MoveDir.eLeft) //����ƶ���������
            {
                m_curPos.x -= _step;
                if (m_curPos.x <= m_distX || m_curPos.x <= 0.0f)
                {
                    m_curPos.x = m_distX;
                    m_eRS = AutoRunState.eWaitFillVegetation;
                    --m_curCol;
                }
            }
            else if (m_eMoveDir == MoveDir.eUp) //����ƶ�������
            {
                m_curPos.z += _step;
                if (m_curPos.z >= m_distZ)
                {
                    m_curPos.z = m_distZ;
                    m_eRS = AutoRunState.eWaitFillVegetation;
                    ++m_curRow;
                }
            }

            m_curPos.y = 1600.0f;
            Ray _ray = new Ray(m_curPos, new Vector3(0.0f, -1.0f, 0.0f));
            RaycastHit _raycastHit;
            if (Physics.Raycast(_ray, out _raycastHit, m_curPos.y))
            {
                m_curPos = _raycastHit.point;
                m_curPos.y += m_offset.y;
                this.transform.position = m_curPos;
            }
        }

        //�����ǰ״̬ʱ�ȴ���ֲֲ����״̬
        if (m_eRS == AutoRunState.eWaitFillVegetation)
        {
			if (!VFVoxelTerrain.self.IsInGenerating)
            {
                m_elps += Time.deltaTime;
                if (m_elps >= 6.0f)
                {
                    m_voxelEditor.AutoFillVegetation();
                    m_elps = 0.0f;
                    if (m_curCol == m_maxCol && m_curRow == m_maxRow)
                        m_eRS = AutoRunState.eIdle;
                    else
                    {
                        if (m_eMoveDir == MoveDir.eRight)
                        {
                            if (m_curCol == m_maxCol)
                            {
                                m_eMoveDir = MoveDir.eUp;
                                m_distZ = m_curPos.z + m_moveStep;
                            }
                            else
                                m_distX = m_curPos.x + m_moveStep;
                        }
                        else if (m_eMoveDir == MoveDir.eLeft)
                        {
                            if (m_curCol == 0)
                            {
                                m_eMoveDir = MoveDir.eUp;
                                m_distZ = m_curPos.z + m_moveStep;
                            }
                            else
                                m_distX = m_curPos.x - m_moveStep;
                        }
                        else if (m_eMoveDir == MoveDir.eUp)
                        {
                            if (m_curCol == 0)
                            {
                                m_eMoveDir = MoveDir.eRight;
                                m_distX = m_curPos.x + m_moveStep;
                            }
                            else if (m_curCol == m_maxCol)
                            {
                                m_eMoveDir = MoveDir.eLeft;
                                m_distX = m_curPos.x - m_moveStep;
                            }
                        }

                        m_eRS = AutoRunState.eMove;
                    }
                }
            }

        }
    }
}
