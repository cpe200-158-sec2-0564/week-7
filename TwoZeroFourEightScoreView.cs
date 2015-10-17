using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twozerofoureight
{
    public partial class TwoZeroFourEightScoreView : Form,View
    {
        public TwoZeroFourEightScoreView()
        {
            InitializeComponent();
        }

        public void Notify(Model m)
        {
            label1.Text = "Total Score : " + ((TwoZeroFourEightModel)m).Score.ToString();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            TwoZeroFourEightView o = new TwoZeroFourEightView(); // New Game Windows
            o.Show();
            this.Close();
        }
    }
}
