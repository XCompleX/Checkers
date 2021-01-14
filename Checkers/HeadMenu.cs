using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class HeadMenu : Form
    {
        public HeadMenu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximumSize = new Size(500, 450);
            MinimumSize = new Size(500, 450);
        }

        private void Exit_b_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pvp_b_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1();
            game.hm = this;
            game.Show();            
            this.Hide();
        }

        private void HM_button_hover(object sender, EventArgs e) {
            ((Button)sender).BackgroundImage = Properties.Resources.button_hover;
        }

        private void HM_button_leave(object sender, EventArgs e) {
            ((Button)sender).BackgroundImage = Properties.Resources.button;
        }
    }
}
