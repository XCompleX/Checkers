using System;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;

namespace Checkers
{
    public partial class HeadMenu : Form
    {
        MediaPlayer mplayer = new MediaPlayer();
        
        string resourcePath = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\Resources\");

        public HeadMenu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximumSize = new Size(557, 547);
            MinimumSize = new Size(557, 547);
            soundPlay("start.wav");
        }

        private void Exit_b_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pvp_b_Click(object sender, EventArgs e)
        {
            soundPlay("click.wav");
            Form1 game = new Form1();
            game.hm = this;
            game.Show();            
            this.Hide();
            
        }

        private void HM_button_hover(object sender, EventArgs e) {
            ((Button)sender).BackgroundImage = Properties.Resources.button_hover;
            soundPlay("hover.wav");
        }

        private void HM_button_leave(object sender, EventArgs e) {
            ((Button)sender).BackgroundImage = Properties.Resources.button;
        }

        void soundPlay(string pesnya)
        {
            mplayer.Open(new Uri(resourcePath + pesnya, UriKind.Relative));
            mplayer.Play();
        }
    }
}
