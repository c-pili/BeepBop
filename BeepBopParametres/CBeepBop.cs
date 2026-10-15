using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    public partial class CBeepBop : Form
    {
        //private const string CportName = "COM3"; // Port du Lecteur RFID
        public CBeepBop()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            //this.m_serialPortRfid.Open();
        }
        
            private int LectureNumeroCarte()
        {
            // Récupération des 12 octets avec ReadExisting :
            string l_trame = this.m_serialPortRfid.ReadExisting();
            int l_numeroCarte;
            string l_derniersOctets = l_trame.Substring(7, 4);
            l_numeroCarte = Int32.Parse(l_derniersOctets,
            System.Globalization.NumberStyles.HexNumber);
            return l_numeroCarte;
        }

        private void CBeepBop_Load(object sender, EventArgs e)
        {

        }

        private void m_serialPortRfid_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            m_lblNumeroCarte.Text = LectureNumeroCarte().ToString();
        }

        private void m_BpConnecterSerialPort_Click(object sender, EventArgs e)
        {
            if(this.m_serialPortRfid.PortName == "")
            {
                MessageBox.Show("Veuillez choisir un port série.");
                return;
            }
            if(this.m_serialPortRfid.BaudRate == 0)
            {
                MessageBox.Show("Veuillez choisir un débit en bauds.");
                return;
            }
            bool Etat = this.m_serialPortRfid.IsOpen;
            this.m_serialPortRfid.PortName = this.m_TxtSerialPort.Text;
            //this.m_serialPortRfid.BaudRate = int.Parse(this.m_TxtSerialBaud.Text);
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                this.m_serialPortRfid.Open();
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
            }
        }

        private void m_TxtSerialPort_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
