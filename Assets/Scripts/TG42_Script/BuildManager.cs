using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//基于box方式的搭建系统

public class IntVec2
{
    public int x, y;

    public IntVec2() { }

    public IntVec2(float _x, float _y)
    {
        x = Mathf.FloorToInt(_x);
        y = Mathf.FloorToInt(_y);
    }

    public IntVec2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public IntVec2(IntVec2 _v2)
    {
        x = _v2.x;
        y = _v2.y;
    }

    public void BestMatchInt(float _x, float _y)
    {
        x = (int)Mathf.Floor(_x);
        y = (int)Mathf.Floor(_y);
        float _temp = _x - (int)Mathf.Floor(_x);
        if (_temp >= 0.5f)
            ++x;

        _temp = _y - (int)Mathf.Floor(_y);
        if (_temp >= 0.5f)
            ++y;
    }

    public override bool Equals(object _obj)
    {
        if (_obj == null)
            return false;

        if (_obj.GetType() != this.GetType())
            return false;

        IntVec2 _v2 = _obj as IntVec2;
        if (x != _v2.x)
            return false;
        if (y != _v2.y)
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }

};

public class IntVec3
{
    public int x, y, z;
	
	public static IntVec3 zero { get { return new IntVec3(0,0,0); } }
	public static IntVec3 one { get { return new IntVec3(1,1,1); } }
	public static IntVec3 minusone { get { return new IntVec3(-1,-1,-1); } }

	public IntVec3() { x = y = z = 0; }

    public IntVec3(float _x, float _y, float _z)
    {
        x = (int)Mathf.Floor(_x);
        y = (int)Mathf.Floor(_y);
        z = (int)Mathf.Floor(_z);
    }

    public IntVec3(Vector3 _v3)
    {
        x = (int)Mathf.Floor(_v3.x);
        y = (int)Mathf.Floor(_v3.y);
        z = (int)Mathf.Floor(_v3.z);
    }

    public IntVec3(IntVec3 _v3)
    {
        x = _v3.x;
        y = _v3.y;
        z = _v3.z;
    }

	public IntVec3(Int64 _xyz)
	{
		x = ((int)(_xyz >> 44));
		y = (((int)(_xyz >> 24)) & 0x000FFFFF);
		z = (((int)(_xyz)) & 0x00FFFFFF);
	}

	public Int64 _XYZ
	{
		get
		{
			Int64 _tmp = 1;
			_tmp = _tmp << 20;
			_tmp |= (UInt32)x;
			_tmp = _tmp << 20;
			_tmp |= (UInt32)y;
			_tmp = _tmp << 24;
			_tmp += z;

			return _tmp;
		}
	}

    public void BestMatchInt(float _x, float _y, float _z)
    {
        x = Mathf.RoundToInt(_x);
        y = Mathf.RoundToInt(_y);
        z = Mathf.RoundToInt(_z);
    }
	
	public void BestMatchInt(Vector3 _v3)
    {
        x = Mathf.RoundToInt(_v3.x);
        y = Mathf.RoundToInt(_v3.y);
        z = Mathf.RoundToInt(_v3.z);
    }

    public override bool Equals(object _obj)
    {
        if (_obj == null)
            return false;

        if (_obj.GetType() != this.GetType())
            return false;

        IntVec3 _v3 = _obj as IntVec3;
        if (x != _v3.x)
            return false;
        if (y != _v3.y)
            return false;
        if (z != _v3.z)
            return false;

        return true;
    }
	
	public static IntVec3 operator * (IntVec3 mul0, IntVec3 mul1)
	{
		return new IntVec3(mul0.x*mul1.x, mul0.y*mul1.y, mul0.z*mul1.z);
	}
	public static IntVec3 operator * (IntVec3 mul0, int mul1)
	{
		return new IntVec3(mul0.x*mul1, mul0.y*mul1, mul0.z*mul1);
	}
	public static IntVec3 operator - (IntVec3 vec0, IntVec3 vec1)
	{
		return new IntVec3(vec0.x-vec1.x, vec0.y-vec1.y, vec0.z-vec1.z);
	}
	public static IntVec3 operator + (IntVec3 vec0, IntVec3 vec1)
	{
		return new IntVec3(vec0.x+vec1.x, vec0.y+vec1.y, vec0.z+vec1.z);
	}
	
	public Vector3 ToVector3()
	{
		return new Vector3(x,y,z);
	}

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
    }

};
#if false
public class CubeBrick
{
    public CubeBrick[] m_neighbour = new CubeBrick[6];
    public int m_dirP = 6, m_dirN = 6;
    public GameObject m_go;

    public CubeBrick GetBrickDirP()
    {
        if(m_dirP == 6)
            return null;
        return m_neighbour[m_dirP];
    }

    public void SetBrickDirP(CubeBrick _cb)
    {
        m_neighbour[m_dirP] = _cb;
    }

    public CubeBrick GetBrickDirN()
    {
        if (m_dirN == 6)
            return null;
        return m_neighbour[m_dirN];
    }

    public void SetBrickDirN(CubeBrick _cb)
    {
        m_neighbour[m_dirN] = _cb;
    }

    public IntVec3 GetIdx()
    {
        return new IntVec3(m_go.transform.position);
    }

    public IntVec3 GetPX()
    {
        IntVec3 _idx = new IntVec3(m_go.transform.position);
        _idx.x += 1;
        return _idx;
    }

    public IntVec3 GetNX()
    {
        IntVec3 _idx = new IntVec3(m_go.transform.position);
        _idx.x -= 1;
        return _idx;
    }

    public IntVec3 GetPY()
    {
        IntVec3 _idx = new IntVec3(m_go.transform.position);
        _idx.y += 1;
        return _idx;
    }

    public IntVec3 GetNY()
    {
        IntVec3 _idx = new IntVec3(m_go.transform.position);
        _idx.y -= 1;
        return _idx;
    }

    public IntVec3 GetPZ()
    {
        IntVec3 _idx = new IntVec3(m_go.transform.position);
        _idx.z += 1;
        return _idx;
    }

    public IntVec3 GetNZ()
    {
        IntVec3 _idx = new IntVec3(m_go.transform.position);
        _idx.z -= 1;
        return _idx;
    }
};

public class BuildManager : MonoBehaviour
{
    public bool m_enableBuild = true;
    public GameObject m_prefabBrick;

    Vector3 m_axisPX = new Vector3(1.0f, 0.0f, 0.0f);
    Vector3 m_axisNX = new Vector3(-1.0f, 0.0f, 0.0f);
    Vector3 m_axisPY = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 m_axisNY = new Vector3(0.0f, -1.0f, 0.0f);
    Vector3 m_axisPZ = new Vector3(0.0f, 0.0f, 1.0f);
    Vector3 m_axisNZ = new Vector3(0.0f, 0.0f, -1.0f);

    const int PXAxis = 0;
    const int NXAxis = 1;
    const int PZAxis = 2;
    const int NZAxis = 3;
    const int PYAxis = 4;
    const int NYAxis = 5;
    const int NoDir = 6;

    enum EBuildState
    {
        eIdleState,
        eDragState
    };
    EBuildState m_eBuildState = EBuildState.eIdleState;
    Dictionary<IntVec3, CubeBrick> m_buildBrickMap = new Dictionary<IntVec3, CubeBrick>();
    List<CubeBrick> m_dragBrickList = new List<CubeBrick>();
    CubeBrick m_firstBrick;
    bool m_bNewFirstBrick = false;
    IntVec3 m_dragIdx;
    int m_dragDir = NoDir;
    bool m_bCollision = false;
    Dictionary<IntVec3, CubeBrick> m_checkListMap = new Dictionary<IntVec3, CubeBrick>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_enableBuild == true)
            CheckBuildOperation();
    }

    void LocateFirstBuildBrick()
    {
        RaycastHit _raycastHit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _raycastHit, 256))
        {
            Vector3 _hitPoint = _raycastHit.point;
            IntVec3 _cubeIdx = new IntVec3(_hitPoint);
            m_dragDir = NoDir;
            m_dragIdx = _cubeIdx;
            m_bCollision = false;

            //为brick对象且碰撞点法线不为Y轴正方向
            if (_raycastHit.collider.gameObject.name == "CubeBrick(Clone)" &&  _raycastHit.normal.y != 1.0f)
            {
                m_firstBrick = m_buildBrickMap[new IntVec3(_raycastHit.collider.gameObject.transform.position)];
                m_bNewFirstBrick = false;
            }
            else
            {
                if (m_buildBrickMap.ContainsKey(_cubeIdx) == false)
                {
                    m_firstBrick = new CubeBrick();
                    m_firstBrick.m_go = (GameObject)GameObject.Instantiate(m_prefabBrick, new Vector3(_cubeIdx.x + 0.5f, _cubeIdx.y + 0.5f, _cubeIdx.z + 0.5f), Quaternion.identity);
                    m_bNewFirstBrick = true;
                }
                else
                {
                    m_firstBrick = m_buildBrickMap[_cubeIdx];
                    m_bNewFirstBrick = false;
                }
            }

        }

    }

   

    void DestroyDragList()
    {
        foreach (CubeBrick _cb in m_dragBrickList)
            Object.DestroyImmediate(_cb.m_go);
        m_dragBrickList.Clear();
    }

    void GetNearestRayDir(ref Ray _screenRay, ref Vector3 _srcPos, ref Vector3 _dir, ref Vector3 _dstPos)
    {
        Ray[] _rayGroup = new Ray[6];
        _rayGroup[PXAxis] = new Ray(_srcPos, m_axisPX);
        _rayGroup[NXAxis] = new Ray(_srcPos, m_axisNX);
        _rayGroup[PZAxis] = new Ray(_srcPos, m_axisPZ);
        _rayGroup[NZAxis] = new Ray(_srcPos, m_axisNZ);
        _rayGroup[PYAxis] = new Ray(_srcPos, m_axisPY);
        _rayGroup[NYAxis] = new Ray(_srcPos, m_axisNY);
     
        float _minDistSq = float.MaxValue;
        float _s = 0.0f, _t = 0.0f;
        int _iDir = 0;

        for (int _i = 0; _i != 6; ++_i)
        {
            if (_rayGroup[_i].direction.x == 0.0f && _rayGroup[_i].direction.y == 0.0f && _rayGroup[_i].direction.z == 0.0f)
                continue;
            float _tempS = 0.0f, _tempT = 0.0f;
            float _distSq = AuxLib.Get().DistSqRayRay(ref _rayGroup[_i], ref _screenRay, ref _tempS, ref _tempT);
            if (_minDistSq > _distSq)
            {
                _minDistSq = _distSq;
                _s = _tempS;
                _t = _tempT;
                _iDir = _i;
            }
        }

        _dir = _rayGroup[_iDir].direction;
        _dstPos = _rayGroup[_iDir].origin + _rayGroup[_iDir].direction * _s;
    }

    void UpdateDragBrick()
	{
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 _pos = m_firstBrick.m_go.transform.position;

        Ray[] _rayGroup = new Ray[6];

        if (m_firstBrick.m_dirP != NoDir && m_firstBrick.m_dirN != NoDir)
        {
            if (m_firstBrick.m_dirP == PXAxis)
                _rayGroup[PXAxis] = new Ray(_pos, m_axisPX);

            if (m_firstBrick.m_dirN == NXAxis)
                _rayGroup[NXAxis] = new Ray(_pos, m_axisNX);

            if (m_firstBrick.m_dirP == PZAxis)
                _rayGroup[PZAxis] = new Ray(_pos, m_axisPZ);

            if (m_firstBrick.m_dirN == NZAxis)
                _rayGroup[NZAxis] = new Ray(_pos, m_axisNZ);

            if (m_firstBrick.m_dirP == PYAxis)
                _rayGroup[PYAxis] = new Ray(_pos, m_axisPY);

            if (m_firstBrick.m_dirN == NYAxis)
                _rayGroup[NYAxis] = new Ray(_pos, m_axisNY);
        }
        else
        {
            _rayGroup[PXAxis] = new Ray(_pos, m_axisPX);
            _rayGroup[NXAxis] = new Ray(_pos, m_axisNX);
            _rayGroup[PZAxis] = new Ray(_pos, m_axisPZ);
            _rayGroup[NZAxis] = new Ray(_pos, m_axisNZ);
            _rayGroup[PYAxis] = new Ray(_pos, m_axisPY);
            //_rayGroup[NYAxis] = new Ray(_pos, m_axisNY);
        }

        float _minDistSq = float.MaxValue;
        float _s = 0.0f, _t = 0.0f;
        int _iDir = 0;

        for (int _i = 0; _i != 6; ++_i)
        {
            if (_rayGroup[_i].direction.x == 0.0f && _rayGroup[_i].direction.y == 0.0f && _rayGroup[_i].direction.z == 0.0f)
                continue;
            float _tempS = 0.0f, _tempT = 0.0f;
            float _distSq = AuxLib.Get().DistSqRayRay(ref _rayGroup[_i], ref _ray, ref _tempS, ref _tempT);
            if (_minDistSq > _distSq)
            {
                _minDistSq = _distSq;
                _s = _tempS;
                _t = _tempT;
                _iDir = _i;
            }
        }
   
        if (m_dragDir != _iDir)
        {
            DestroyDragList();
            m_dragIdx = new IntVec3(m_firstBrick.m_go.transform.position);
            m_bCollision = false;
        }
        m_dragDir = _iDir;

        Vector3 _destPos = new Vector3();
        _destPos = _rayGroup[_iDir].origin + _rayGroup[_iDir].direction * _s;
        IntVec3 _destIdx = new IntVec3(_destPos);
        if (m_dragIdx != _destIdx)
        {
            int _iOffset = 0;
            bool _bInc = true;
            switch (m_dragDir)
            {
                case PXAxis:
                    {
                        _iOffset = Mathf.Abs(_destIdx.x - m_dragIdx.x);
                        if (_destIdx.x < m_dragIdx.x)
                            _bInc = false;

                        for (int _i = 1; _i <= _iOffset; ++_i)
                        {
                            IntVec3 _curIdx = new IntVec3(m_dragIdx);
                            if (_bInc == true)
                            {
                                if (m_bCollision == true)
                                    break;
                                _curIdx.x += _i;
                                if (m_buildBrickMap.ContainsKey(_curIdx) == false)
                                {
                                    CubeBrick _cb = new CubeBrick();
                                    _cb.m_dirP = PXAxis;
                                    _cb.m_dirN = NXAxis;

                                    _cb.m_go = (GameObject)Instantiate(m_prefabBrick, new Vector3(_curIdx.x + 0.5f, _curIdx.y + 0.5f, _curIdx.z + 0.5f), Quaternion.identity);
                                    m_dragBrickList.Add(_cb);
                                }
                                else
                                    m_bCollision = true;
                            }
                            else
                            {
                                int _iPos = m_dragBrickList.Count - 1;
                                if (_iPos >= 0)
                                {
                                    CubeBrick _cb = m_dragBrickList[_iPos];
                                    Object.DestroyImmediate(_cb.m_go);
                                    m_dragBrickList.RemoveAt(_iPos);
                                }
                            }

                        }
                        break;
                    }
                case NXAxis:
                    {
                        _iOffset = Mathf.Abs(_destIdx.x - m_dragIdx.x);
                        if (_destIdx.x > m_dragIdx.x)
                            _bInc = false;

                        for (int _i = 1; _i <= _iOffset; ++_i)
                        {
                            IntVec3 _curIdx = new IntVec3(m_dragIdx);
                            if (_bInc == true)
                            {
                                if (m_bCollision == true)
                                    break;
                                _curIdx.x -= _i;
                                if (m_buildBrickMap.ContainsKey(_curIdx) == false)
                                {
                                    CubeBrick _cb = new CubeBrick();
                                    _cb.m_dirP = PXAxis;
                                    _cb.m_dirN = NXAxis;
                                    _cb.m_go = (GameObject)Instantiate(m_prefabBrick, new Vector3(_curIdx.x + 0.5f, _curIdx.y + 0.5f, _curIdx.z + 0.5f), Quaternion.identity);
                                    m_dragBrickList.Add(_cb);
                                }
                                else
                                    m_bCollision = true;
                            }
                            else
                            {
                                int _iPos = m_dragBrickList.Count - 1;
                                if (_iPos >= 0)
                                {
                                    CubeBrick _cb = m_dragBrickList[_iPos];
                                    Object.DestroyImmediate(_cb.m_go);
                                    m_dragBrickList.RemoveAt(_iPos);
                                }

                            }
                        }
                        break;
                    }
                case PYAxis:
                    {
                        _iOffset = Mathf.Abs(_destIdx.y - m_dragIdx.y);
                        if (_destIdx.y < m_dragIdx.y)
                            _bInc = false;

                        for (int _i = 1; _i <= _iOffset; ++_i)
                        {
                            IntVec3 _curIdx = new IntVec3(m_dragIdx);
                            if (_bInc == true)
                            {
                                if (m_bCollision == true)
                                    break;
                                _curIdx.y += _i;
                                if (m_buildBrickMap.ContainsKey(_curIdx) == false)
                                {
                                    CubeBrick _cb = new CubeBrick();
                                    _cb.m_dirP = PYAxis;
                                    _cb.m_dirN = NYAxis;
                                    _cb.m_go = (GameObject)Instantiate(m_prefabBrick, new Vector3(_curIdx.x + 0.5f, _curIdx.y + 0.5f, _curIdx.z + 0.5f), Quaternion.identity);
                                    m_dragBrickList.Add(_cb);
                                }
                                else
                                    m_bCollision = true;
                            }
                            else
                            {
                                int _iPos = m_dragBrickList.Count - 1;
                                if (_iPos >= 0)
                                {
                                    CubeBrick _cb = m_dragBrickList[_iPos];
                                    Object.DestroyImmediate(_cb.m_go);
                                    m_dragBrickList.RemoveAt(_iPos);
                                }

                            }

                        }
                        break;
                    }
                case NYAxis:
                    {
                        _iOffset = Mathf.Abs(_destIdx.y - m_dragIdx.y);
                        if (_destIdx.y > m_dragIdx.y)
                            _bInc = false;

                        for (int _i = 1; _i <= _iOffset; ++_i)
                        {
                            IntVec3 _curIdx = new IntVec3(m_dragIdx);
                            if (_bInc == true)
                            {
                                if (m_bCollision == true)
                                    break;
                                _curIdx.y -= _i;
                                if (m_buildBrickMap.ContainsKey(_curIdx) == false)
                                {
                                    CubeBrick _cb = new CubeBrick();
                                    _cb.m_dirP = PYAxis;
                                    _cb.m_dirN = NYAxis;
                                    _cb.m_go = (GameObject)Instantiate(m_prefabBrick, new Vector3(_curIdx.x + 0.5f, _curIdx.y + 0.5f, _curIdx.z + 0.5f), Quaternion.identity);
                                    m_dragBrickList.Add(_cb);
                                }
                                else
                                    m_bCollision = true;
                            }
                            else
                            {
                                int _iPos = m_dragBrickList.Count - 1;
                                if (_iPos >= 0)
                                {
                                    CubeBrick _cb = m_dragBrickList[_iPos];
                                    Object.DestroyImmediate(_cb.m_go);
                                    m_dragBrickList.RemoveAt(_iPos);
                                }

                            }
                        }
                        break;
                    }
                case PZAxis:
                    {
                        _iOffset = Mathf.Abs(_destIdx.z - m_dragIdx.z);
                        if (_destIdx.z < m_dragIdx.z)
                            _bInc = false;

                        for (int _i = 1; _i <= _iOffset; ++_i)
                        {
                            IntVec3 _curIdx = new IntVec3(m_dragIdx);
                            if (_bInc == true)
                            {
                                if (m_bCollision == true)
                                    break;
                                _curIdx.z += _i;
                                if (m_buildBrickMap.ContainsKey(_curIdx) == false)
                                {
                                    CubeBrick _cb = new CubeBrick();
                                    _cb.m_dirP = PZAxis;
                                    _cb.m_dirN = NZAxis;
                                    _cb.m_go = (GameObject)Instantiate(m_prefabBrick, new Vector3(_curIdx.x + 0.5f, _curIdx.y + 0.5f, _curIdx.z + 0.5f), Quaternion.identity);
                                    m_dragBrickList.Add(_cb);
                                }
                                else
                                    m_bCollision = true;
                            }
                            else
                            {
                                int _iPos = m_dragBrickList.Count - 1;
                                if (_iPos >= 0)
                                {
                                    CubeBrick _cb = m_dragBrickList[_iPos];
                                    Object.DestroyImmediate(_cb.m_go);
                                    m_dragBrickList.RemoveAt(_iPos);
                                }

                            }

                        }
                        break;
                    }
                case NZAxis:
                    {
                        _iOffset = Mathf.Abs(_destIdx.z - m_dragIdx.z);
                        if (_destIdx.z > m_dragIdx.z)
                            _bInc = false;

                        for (int _i = 1; _i <= _iOffset; ++_i)
                        {
                            IntVec3 _curIdx = new IntVec3(m_dragIdx);
                            if (_bInc == true)
                            {
                                if (m_bCollision == true)
                                    break;
                                _curIdx.z -= _i;
                                if (m_buildBrickMap.ContainsKey(_curIdx) == false)
                                {
                                    CubeBrick _cb = new CubeBrick();
                                    _cb.m_dirP = PZAxis;
                                    _cb.m_dirN = NZAxis;
                                    _cb.m_go = (GameObject)Instantiate(m_prefabBrick, new Vector3(_curIdx.x + 0.5f, _curIdx.y + 0.5f, _curIdx.z + 0.5f), Quaternion.identity);
                                    m_dragBrickList.Add(_cb);
                                }
                                else
                                    m_bCollision = true;
                            }
                            else
                            {
                                int _iPos = m_dragBrickList.Count - 1;
                                if (_iPos >= 0)
                                {
                                    CubeBrick _cb = m_dragBrickList[_iPos];
                                    Object.DestroyImmediate(_cb.m_go);
                                    m_dragBrickList.RemoveAt(_iPos);
                                }

                            }
                        }
                        break;
                    }
            }

            m_dragIdx = _destIdx;

        }

    }

    void BuildDragBrick()
    {
        if(m_bNewFirstBrick == true)
        {
            IntVec3 _pos = new IntVec3(m_firstBrick.m_go.transform.position);
            m_buildBrickMap.Add(_pos, m_firstBrick);
        }

        for (int _i = 0; _i != m_dragBrickList.Count; ++_i)
        {
            CubeBrick _cb = m_dragBrickList[_i];
            if (m_dragDir == _cb.m_dirP)
            {
                int _nextP = _i + 1;
                int _nextN = _i - 1;
                if (_nextP < m_dragBrickList.Count)
                    _cb.m_neighbour[_cb.m_dirP] = m_dragBrickList[_nextP];
                if (_nextN >= 0)
                    _cb.m_neighbour[_cb.m_dirN] = m_dragBrickList[_nextN];
            }
            else
            {
                if (m_dragDir == _cb.m_dirN)
                {
                    int _nextP = _i - 1;
                    int _nextN = _i + 1;
                    if (_nextP >= 0)
                        _cb.m_neighbour[_cb.m_dirP] = m_dragBrickList[_nextP];
                    if (_nextN < m_dragBrickList.Count)
                        _cb.m_neighbour[_cb.m_dirN] = m_dragBrickList[_nextN];
                }
            }

            IntVec3 _pos = new IntVec3(_cb.m_go.transform.position);
            m_buildBrickMap.Add(_pos, _cb);
        }

        if (m_dragBrickList.Count != 0)
        {
            CubeBrick _cb = m_dragBrickList[0];
            m_firstBrick.m_dirP = _cb.m_dirP;
            m_firstBrick.m_dirN = _cb.m_dirN;
            if (m_dragDir == _cb.m_dirP)
            {
                m_firstBrick.SetBrickDirP(m_dragBrickList[0]);
                m_dragBrickList[0].SetBrickDirN(m_firstBrick);
            }
            else
            {
                if (m_dragDir == _cb.m_dirN)
                {
                    m_firstBrick.SetBrickDirN(m_dragBrickList[0]);
                    m_dragBrickList[0].SetBrickDirP(m_firstBrick);
                }
            }
        }
        m_dragBrickList.Clear();
        m_firstBrick = null;
    }

    void DestroyTargetBrick()
    {
        RaycastHit _raycastHit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _raycastHit, 256))
        {
            Vector3 _hitPoint = _raycastHit.point;
			if(_raycastHit.collider.gameObject.name != "CubeBrick(Clone)")
				return;
			
            IntVec3 _cubeIdx = new IntVec3(_raycastHit.collider.gameObject.transform.position);
       
            if (m_buildBrickMap.ContainsKey(_cubeIdx) == true)
            {
                CubeBrick _cb = m_buildBrickMap[_cubeIdx];
 	
				//清理targetBrick
                m_buildBrickMap.Remove(_cubeIdx);
                Object.DestroyImmediate(_cb.m_go);

                CubeBrick _cbNgb = _cb.GetBrickDirP();
				if(_cbNgb != null)
                    _cbNgb.SetBrickDirN(null);

                _cbNgb = _cb.GetBrickDirN();
                if (_cbNgb != null)
                    _cbNgb.SetBrickDirP(null);

                //无论哪种情况都需要检查上面的brick
                IntVec3 _upIdx = new IntVec3(_cubeIdx);
                _upIdx.y += 1;
                if (m_buildBrickMap.ContainsKey(_upIdx) == true)
                {
                    m_checkListMap.Add(_upIdx, m_buildBrickMap[_upIdx]);
                }

                //如果targetBrick为PY方向 或者 targetBrick无相邻对象，则通知其上面的brick
                if ((_cb.m_dirP == PYAxis) || (_cb.m_dirN == NoDir && _cb.m_dirP == NoDir))
                    return;

                //如果targetBrick为其它方向，则通知其相邻方向和上面的brick
                if (_cb.m_dirP != NoDir)
				{
                    _cbNgb = _cb.GetBrickDirP();
                    if(_cbNgb != null)
                        m_checkListMap.Add(_cbNgb.GetIdx(), _cbNgb);
				}

                if (_cb.m_dirN != NoDir)
                {
                    _cbNgb = _cb.GetBrickDirN();
                    if(_cbNgb != null)
                        m_checkListMap.Add(_cbNgb.GetIdx(), _cbNgb);
                }

                
            }
          
        }

    }

    void DetachBrick(CubeBrick _cb)
    {
        _cb.m_go.rigidbody.isKinematic = false;
        _cb.m_go.rigidbody.AddForce(0.0f, -9.8f, 0.0f);
        IntVec3 _posIdx = _cb.GetIdx();
        m_buildBrickMap.Remove(_posIdx);
        m_checkListMap.Remove(_posIdx);
    }

    void UpdateCheckList()
    {
        while (m_checkListMap.Count != 0)
        {
            Dictionary<IntVec3, CubeBrick>.Enumerator _enum = m_checkListMap.GetEnumerator();
            bool _b = _enum.MoveNext();
            KeyValuePair<IntVec3, CubeBrick> _it = _enum.Current;
            CubeBrick _cb = _it.Value;

            //如果为独立brick，无任何连接方向
            if (_cb.m_dirP == NoDir && _cb.m_dirN == NoDir)
            {
                IntVec3 _upIdx = _cb.GetPY();
                if (m_buildBrickMap.ContainsKey(_upIdx))
                {
                    m_checkListMap.Add(_upIdx, m_buildBrickMap[_upIdx]);
                }
                DetachBrick(_cb);
                break;
            }

            //如果为Y轴正方向
            if (_cb.m_dirP == PYAxis)
            {
                if (_cb.m_neighbour[_cb.m_dirP] != null)  //非Y轴最顶层
                {
                    m_checkListMap.Add(_cb.m_neighbour[_cb.m_dirP].GetIdx(), _cb.m_neighbour[_cb.m_dirP]);    
                }
                else //为Y抽最顶层
                {
                    IntVec3 _upIdx = _cb.GetPY();
                    if (m_buildBrickMap.ContainsKey(_upIdx))
                    {
                        m_checkListMap.Add(_upIdx, m_buildBrickMap[_upIdx]);
                    }
                }

                DetachBrick(_cb);
                continue;
            }

            //如果为非Y轴队列：先检查每个brick下面是否存在支撑点，如果一个支撑点都没有，则detach改队列所有brick
            bool _hasPillar = false;
            List<CubeBrick> _brickGroup = new List<CubeBrick>();
            IntVec3 _downIdx = _cb.GetNY();

            //首先检查当前brick下面是否存在支撑点
            if (m_buildBrickMap.ContainsKey(_downIdx))
                _hasPillar = true;
            else
                _brickGroup.Add(_cb); //加入当前cubebrick

            if(_hasPillar == false)
            {
                CubeBrick _cbNgb = _cb.m_neighbour[_cb.m_dirP];
                //检查正方向
                while (_cbNgb != null)
                {
                    _downIdx = _cbNgb.GetNY();
                    if (m_buildBrickMap.ContainsKey(_downIdx))
                    {
                        _hasPillar = true;
                        break;
                    }
                    else
                    {
                        _brickGroup.Add(_cbNgb);
                        _cbNgb = _cbNgb.m_neighbour[_cbNgb.m_dirP];
                    }
                }

                //检查负方向
                if (_hasPillar == false)
                {
                    _cbNgb = _cb.m_neighbour[_cb.m_dirN];
                    while (_cbNgb != null)
                    {
                        _downIdx = _cbNgb.GetNY();
                        if (m_buildBrickMap.ContainsKey(_downIdx))
                        {
                            _hasPillar = true;
                            break;
                        }
                        else
                        {
                            _brickGroup.Add(_cbNgb);
                            _cbNgb = _cbNgb.m_neighbour[_cbNgb.m_dirN];
                        }
                    }
                }
            }

            if (_hasPillar == false)
            {
                foreach (CubeBrick _brick in _brickGroup)
                {
                    m_buildBrickMap.Remove(_brick.GetIdx());
                    DetachBrick(_brick);
                    IntVec3 _upIdx = _brick.GetPY();
                    if (m_buildBrickMap.ContainsKey(_upIdx))
                        m_checkListMap.Add(_upIdx, m_buildBrickMap[_upIdx]);
                }
            }

            m_checkListMap.Remove(_cb.GetIdx());
        }
        //yield return 0;
    }

    void CheckBuildOperation()
    {
        //if (m_checkListMap.Count != 0)
            UpdateCheckList();

        switch (m_eBuildState)
        {
            case EBuildState.eIdleState:
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        LocateFirstBuildBrick();
						if(m_firstBrick != null)
                        	m_eBuildState = EBuildState.eDragState;
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse2))
                        {
                            DestroyTargetBrick();
                        }
                    }
                    break;
                }
            case EBuildState.eDragState:
                {
                    UpdateDragBrick();
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        m_eBuildState = EBuildState.eIdleState;
                        BuildDragBrick();
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                        {
                            m_eBuildState = EBuildState.eIdleState;
                            DestroyDragList();
                            if (m_bNewFirstBrick == true)
                            {
                                Object.DestroyImmediate(m_firstBrick.m_go);
                            }
                        }
                    }

                    break;
                }
        };


    }
}
#endif