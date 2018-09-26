using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameSimulator;
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
           // m_GameSimulator = new GameSimulator.GameSimulator();
        }


        private void DLLButton_Click(object sender, EventArgs e)
        {
            string sFilePath;
            OpenFileDialog openfileDialog = new OpenFileDialog();
            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                sFilePath = openfileDialog.FileName;

                var dll = Assembly.LoadFile(sFilePath);
                string sName = dll.GetName().Name;
                Type type = dll.GetType("GameSimulator." + sName);
                m_GameSimulator = (GameSimulator.GameSimulator)Activator.CreateInstance(type);//(simType)m_GameSimulator;
                m_GameSimulator.Initialize();
                //this.dataGridView1
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

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
