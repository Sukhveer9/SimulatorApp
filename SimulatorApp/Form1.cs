using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using GameSimulator;
using System.Reflection;

namespace SimulatorApp
{
    public partial class Form1 : Form
    {
        private GameSimulator.GameSimulator m_GameSimulator;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            m_GameSimulator = new GameSimulator.GameSimulator();
            m_GameSimulator.m_UpdateSpin = SpinUpdated;
        }

        public void SpinUpdated(int num)
        {
            MethodInvoker inv = delegate
            {
                this.label.Text = num.ToString();
            };
            this.Invoke(inv);
        }


        private void DLLButton_Click(object sender, EventArgs e)
        {
            string sFilePath;
            OpenFileDialog openfileDialog = new OpenFileDialog();
            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                sFilePath = openfileDialog.FileName;

                 m_GameSimulator.LoadDll(sFilePath);
            }
        }

        private void PayTableButton_Click(object sender, EventArgs e)
        {
            string sFilePath;
            OpenFileDialog openfileDialog = new OpenFileDialog();
            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                sFilePath = openfileDialog.FileName;
                m_GameSimulator.LoadXML(sFilePath);
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            m_GameSimulator.StartSpin(10000);
        }

    }
}
