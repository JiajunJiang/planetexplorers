using PatheaScript;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Event = PatheaScript.Event;

namespace PatheaScriptExt
{
    public class PeTimerMgr
    {
        static PeTimerMgr instance = null;
        public static PeTimerMgr Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new PeTimerMgr();
                }

                return instance;
            }
        }

        Dictionary<string, PETimer> mDicTimer = null;
        public void Add(string name, PETimer timer)
        {
            if (null == mDicTimer)
            {
                mDicTimer = new Dictionary<string, PETimer>(2);
            }

            if (mDicTimer.ContainsKey(name))
            {
                Debug.LogError("timer:" + name + " exist.");
                return;
            }

            mDicTimer.Add(name, timer);
        }

        public bool Remove(string name)
        {
            if (null == mDicTimer)
            {
                return false;
            }

            return mDicTimer.Remove(name);
        }

        public PETimer Get(string name)
        {
            if (null == mDicTimer)
            {
                return null;
            }

            if (!mDicTimer.ContainsKey(name))
            {
                return null;
            }

            return mDicTimer[name];
        }

        public void Update()
        {
            if (null == mDicTimer)
            {
                return;
            }

            foreach (PETimer timer in mDicTimer.Values)
            {
                timer.Update(Time.deltaTime);
            }
        }
    }
}