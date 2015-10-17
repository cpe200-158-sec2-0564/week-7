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
    public partial class TwoZeroFourEightView : Form, View
    {
        Model model;
        Controller controller;
        TwoZeroFourEightScoreView newView;
        private int tileCounter=0;

        public TwoZeroFourEightView()
        {
            InitializeComponent();
            model = new TwoZeroFourEightModel();
            model.AttachObserver(this);
            controller = new TwoZeroFourEightController();
            controller.AddModel(model);
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
            newView = new TwoZeroFourEightScoreView();
            model.AttachObserver(newView);
        }
        private async void EndingView(bool e)
        {
            if (this.Visible == true && e)
            {
                await Task.Delay(1500);
                newView.Visible = e;
                newView.Enabled = e;
                this.Visible = false;
            }
        }


        private void UpdateScore(int a)
        {
            lblScore.Text = "Score : "+a;
        } 

        public void Notify(Model m)
        {
            UpdateBoard(((TwoZeroFourEightModel) m).GetBoard());
            UpdateScore(((TwoZeroFourEightModel)m).Score);
            EndingView(((TwoZeroFourEightModel)m).IsEnd);
        }

        private void UpdateTile(Label l, int i)
        {
            if (i != 0)
            {
                l.Text = Convert.ToString(i);
                Occupied();                         // There is a number here
            } else {
                l.Text = "";
            }
            switch (i)
            {
                case 0:
                    l.BackColor = Color.Gray;
                    break;
                case 2:
                    l.BackColor = Color.Tan;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular);
                    break;
                case 4:
                    l.BackColor = Color.RosyBrown;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular);
                    break;
                case 8:
                    l.BackColor = Color.DarkOrange;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular);
                    break;
                case 16:
                    l.BackColor = Color.OrangeRed;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular);
                    break;
                case 32:
                case 64:
                    l.BackColor = Color.Firebrick;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular);
                    break;
                case 128:
                case 256:
                case 512:
                    l.BackColor = Color.Firebrick;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, FontStyle.Bold);
                    break;
                case 1024:
                case 2048:
                case 4096:
                case 8192:
                    l.BackColor = Color.Firebrick;
                    l.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
                    break;
            }
        }
        private void UpdateBoard(int[,] board)
        {
            tileCounter = 0;
            UpdateTile(lbl00,board[0, 0]);
            UpdateTile(lbl01,board[0, 1]);
            UpdateTile(lbl02,board[0, 2]);
            UpdateTile(lbl03,board[0, 3]);
            UpdateTile(lbl10,board[1, 0]);
            UpdateTile(lbl11,board[1, 1]);
            UpdateTile(lbl12,board[1, 2]);
            UpdateTile(lbl13,board[1, 3]);
            UpdateTile(lbl20,board[2, 0]);
            UpdateTile(lbl21,board[2, 1]);
            UpdateTile(lbl22,board[2, 2]);
            UpdateTile(lbl23,board[2, 3]);
            UpdateTile(lbl30,board[3, 0]);
            UpdateTile(lbl31,board[3, 1]);
            UpdateTile(lbl32,board[3, 2]);
            UpdateTile(lbl33,board[3, 3]);
            if (tileCounter == 16) FullBoard();     // Start scan when board is 16-tiled full
        }

        private int Occupied()
        {
            tileCounter += 1;
            return tileCounter;
        }                 // Check number of tiles being occupied

        private void FullBoard()
        {
            controller.ActionPerformed(TwoZeroFourEightController.SCAN);
        }               // Perform 16 tiles scan

        private void btnLeft_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.DOWN);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                controller.ActionPerformed(TwoZeroFourEightController.UP);
            }
            if (keyData == Keys.Down)
            {
                controller.ActionPerformed(TwoZeroFourEightController.DOWN);
            }
            if (keyData == Keys.Left)
            {
                controller.ActionPerformed(TwoZeroFourEightController.LEFT);
            }
            if (keyData == Keys.Right)
            {
                controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }    // Arrow control

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }           // Modify close button
    }
}
