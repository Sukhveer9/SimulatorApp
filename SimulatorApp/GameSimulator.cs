using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameEngine;

namespace GameSimulator
{

    public class GameSimulator
    {

        private int m_iCurrentSpinCount;
        private int m_iTotalSpinCount;

        protected SlotEngine m_SlotEngine;
        private Thread m_StartThread;

        public int CurrentSpinCount
        { get { return m_iCurrentSpinCount; } }

        public GameSimulator()
        {

        }

        public virtual void Initialize()
        {
            m_iCurrentSpinCount = 0;
        }

        public T GetSlotType<T>(object o)
        {
            return (T)o;
        }

        public void LoadDll(string sPath)
        {
            var dll = Assembly.LoadFrom(sPath);//
            string sName = dll.GetName().Name;
            Type type = dll.GetType(sName + "." + sName);
            object test = null;
            m_SlotEngine = Activator.CreateInstance(type) as SlotEngine;
           // m_SlotEngine = GetSlotType<t
        }

        public virtual void LoadXML(string sXMLPath)
        {
            m_SlotEngine.LoadXML(sXMLPath);
        }

        public void StartSpin(int iTotalSpinCount)
        {
            m_iTotalSpinCount = iTotalSpinCount;
            m_StartThread = new Thread(Start);
            m_StartThread.Start();
        }


        public Action<int> m_UpdateSpin;
        private Stopwatch m_Watch;

        public void Start()
        {
            int icounter = 0;
            m_Watch = Stopwatch.StartNew();
            while(icounter < m_iTotalSpinCount)
            {
                if(m_SlotEngine.ReadyForSpin())
                {
                     //m_SlotEngine.PlayGameSimulator();
                    m_SlotEngine.PlayThread(1);
                    icounter++;
                    m_UpdateSpin(icounter);
                }
            }
            ShowStats();
            RNG.Random.ExitRandom();

        }

        public void ShowStats()
        {
            StringBuilder sStat = m_SlotEngine.getStatisticsOutout(m_iTotalSpinCount * 25);
            string sFile = "statistics.txt";
            using (StreamWriter sw = File.CreateText(sFile))
            {
                sw.WriteLine(m_Watch.Elapsed.ToString());
                sw.WriteLine(sStat.ToString());
            }
        }

    }
}
