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
    public partial class CBeepBopBase : Form
    {
        private const string CportName = "COM5"; // Port du Lecteur RFID
        public CBeepBopBase()
        {
            InitializeComponent();
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


        private void m_serialPortRfid_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            m_lblNumeroCarte.Text = LectureNumeroCarte().ToString();
        }
    }
}
