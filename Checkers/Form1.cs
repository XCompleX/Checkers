using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Windows.Media;
using System.IO;
using System.Threading;

namespace Checkers {
    public partial class Form1 : Form {

        public HeadMenu hm;

        Point mouse;

        Image[] checkersForm;

        bool click = false;
        bool needEat = false;
        bool player = true;
        
        int course;
        int position;
        int comboEatCell = -1;

        float dx, dy;
        float sizeX, sizeY;

        int[] checkersMap;
        int[,] possibleMovesEatMap;

        float[,] mapCoordinate;
        float[,,] possibleMoves = new float[4, 7, 4];

        string resourcePath = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\Resources\");

        public Form1() {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            init();
            checkMapOnPossibleMoves();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            click = true;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Resize(object sender, EventArgs e) {
            ResizeMap();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            for (int v = 0; v < 8; v++) {
                for (int h = 0; h < 4; h++) {
                    float X = mapCoordinate[(v % 2 == 0 ? h : h + 4), 0] - sizeX / 2;
                    float Y = mapCoordinate[v, 1] - sizeY / 2;
                    if (checkersMap[h + v * 4] != -1) {
                        Image checker = checkersForm[checkersMap[h + v * 4]];
                        e.Graphics.DrawImage(checker, X, Y, sizeX, sizeY);
                    }
                    if (possibleMovesEatMap[h + v * 4, Convert.ToInt32(needEat)] == 1) {
                        Image checker = needEat ? Properties.Resources.red : Properties.Resources.green;
                        e.Graphics.DrawImage(checker, X, Y, sizeX, sizeY);
                    }
                }
            }

            if (click) {
                ClickOnChecker(e);
                click = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            hm.Show();
            soundPlay("click.wav");
        }

        void ClickOnChecker(PaintEventArgs e) {
            Image allocation = Properties.Resources.yellow;
            mouse = PointToClient(Cursor.Position);

            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 7; j++) {
                    if (mouse.X < possibleMoves[i, j, 0] + sizeX / 2 && mouse.Y < possibleMoves[i, j, 1] + sizeY / 2 &&
                        mouse.X > possibleMoves[i, j, 0] - sizeX / 2 && mouse.Y > possibleMoves[i, j, 1] - sizeY / 2) {

                        if ((int)possibleMoves[i, j, 2] < 4 && player || (int)possibleMoves[i, j, 2] > 27 && !player) {
                            checkersMap[(int)possibleMoves[i, j, 2]] = course + 2;
                            soundPlay("move.wav");
                        }
                        else {
                            checkersMap[(int)possibleMoves[i, j, 2]] = checkersMap[position];
                            soundPlay("move.wav");
                        }

                        checkersMap[position] = -1;
                        if (needEat) {
                            Thread newThread = new Thread(soundPlay);
                            newThread.Start("eat.wav");

                            checkersMap[(int)possibleMoves[i, 0, 3]] = -1;
                            comboEatCell = (int)possibleMoves[i, j, 2];
                            checkMapOnPossibleMoves();
                        }
                        pictureBox1.Invalidate();
                        Array.Clear(possibleMoves, 0, possibleMoves.Length);
                        if (needEat)
                            break;
                        comboEatCell = -1;
                        course += course % 2 == 0 ? 1 : -1;
                        player = !player;
                        checkMapOnPossibleMoves();
                        return;
                    }
                }
            }

            Array.Clear(possibleMoves, 0, possibleMoves.Length);

            for (int v = 0; v < 8 && click; v++) {
                for (int h = 0; h < 4 && click; h++) {
                    float X = mapCoordinate[(v % 2 == 0 ? h : h + 4), 0];
                    float Y = mapCoordinate[v, 1];
                    if (mouse.X < X + sizeX / 2 && mouse.Y < Y + sizeY / 2 &&
                        mouse.X > X - sizeX / 2 && mouse.Y > Y - sizeY / 2) {
                        position = (v % 2 == 0 ? h : h + 4) + 8 * (v - v % 2) / 2;
                        if (checkersMap[position] == course) {
                            e.Graphics.DrawImage(allocation, X - sizeX / 2, Y - sizeY / 2, sizeX, sizeY);
                            for (int i = 0; i < (needEat ? 4 : 2); i++) {
                                int Xdiag = (v % 2 == 0 ? h : h + 4), Ydiag = v, diagPos = 0;
                                cellCalculation(i, ref Xdiag, ref Ydiag, ref diagPos, false);
                                if (needEat) {
                                    if (Xdiag >= 0 && Xdiag < 8 && (comboEatCell == -1 || comboEatCell == position) &&
                                        Ydiag >= 0 && Ydiag < 8 && (checkersMap[diagPos] % 2) != course && checkersMap[diagPos] != -1) {
                                        possibleMoves[i, 0, 3] = diagPos;
                                        cellCalculation(i, ref Xdiag, ref Ydiag, ref diagPos, false);
                                        showPossibleMoves(i, 0, Xdiag, Ydiag, diagPos, allocation, e);
                                    }
                                }
                                else
                                    showPossibleMoves(i, 0, Xdiag, Ydiag, diagPos, allocation, e);
                            }
                        }
                        else if (checkersMap[position] == course + 2) {
                            bool check = false;
                            e.Graphics.DrawImage(allocation, X - sizeX / 2, Y - sizeY / 2, sizeX, sizeY);
                            for (int i = 0; i < 4; i++) {
                                int Xdiag = (v % 2 == 0 ? h : h + 4), Ydiag = v, diagPos = 0;
                                if (needEat) {
                                    for (int j = 0; ; j++) {
                                        cellCalculation(i, ref Xdiag, ref Ydiag, ref diagPos, false);
                                        if ((Xdiag >= 0 && Xdiag < 8 && (comboEatCell == -1 || comboEatCell == position) &&
                                            Ydiag >= 0 && Ydiag < 8 && (checkersMap[diagPos] % 2) != course && checkersMap[diagPos] != -1)) {
                                            check = true;
                                            break;
                                        }
                                        if (Xdiag < 0 || Xdiag > 7 || Ydiag < 0 || Ydiag > 7) {
                                            check = false;
                                            break;
                                        }
                                    }
                                    if (!check)
                                        continue;
                                    possibleMoves[i, 0, 3] = diagPos;
                                }
                                for (int j = 0; ; j++) {
                                    cellCalculation(i, ref Xdiag, ref Ydiag, ref diagPos, false);
                                    if (!showPossibleMoves(i, j, Xdiag, Ydiag, diagPos, allocation, e))
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        void cellCalculation(int number, ref int Xdiag, ref int Ydiag, ref int diagPos, bool search) {
            Xdiag = number < (needEat || (checkersMap[position] == course + 2 || search) ? 2 : 1) ? (Ydiag % 2 == 0 ? Xdiag + 4 : Xdiag - 5) : (Ydiag % 2 == 0 ? Xdiag + 5 : Xdiag - 4);
            Ydiag = Ydiag + ((needEat || checkersMap[position] == course + 2 || search ? number : course) % 2 == 0 ? 1 : -1);
            diagPos = Xdiag + Ydiag / 2 * 8;
        }
        bool showPossibleMoves(int number, int cell, int Xdiag, int Ydiag, int diagPos, Image allocation, PaintEventArgs e) {
            if (Xdiag >= 0 && Xdiag < 8 &&
                Ydiag >= 0 && Ydiag < 8 && checkersMap[diagPos] == -1) {
                possibleMoves[number, cell, 0] = mapCoordinate[Xdiag, 0];
                possibleMoves[number, cell, 1] = mapCoordinate[Ydiag, 1];
                possibleMoves[number, cell, 2] = diagPos;
                e.Graphics.DrawImage(allocation, possibleMoves[number, cell, 0] - sizeX / 2, possibleMoves[number, cell, 1] - sizeY / 2, sizeX, sizeY);
                return true;
            }
            return false;
        }
        void checkMapOnPossibleMoves() {
            needEat = false;
            int[] backCell = new int[4] { -4, 4, -5, 3 };

            for(int i = 0; i < 2; i++)
                for (int j = 0; j < 32; j++)
                    possibleMovesEatMap[j, i] = 0;

            for (int i = 0; i <= 20; i += 4) {
                for (int j = 0; j < 3; j++) {
                    int cell = (i % 8 == 0 ? 5 : 4);
                    
                    for (int k = 0; k < 4; k++) {
                        int fXdiag = (i + j + cell) % 8, fYdiag = (i + j + cell) / 4, fdiagPos = (i + j + cell);
                        int Xdiag = (i + j + cell) % 8, Ydiag = (i + j + cell) / 4, diagPos = (i + j + cell);
                        bool check = false;
                        for (int z = 0; ; z++) {
                            cellCalculation(k, ref Xdiag, ref Ydiag, ref diagPos, true);
                            if (Xdiag < 0 || Xdiag > 7 || Ydiag < 0 || Ydiag > 7)
                                break;
                            if (checkersMap[diagPos] == -1 && !check) {
                                for (int x = course % 2 == 1 ? 0 : 1; x < 4; x += 2) {
                                    int X = Xdiag, Y = Ydiag, D = diagPos;

                                    cellCalculation(x, ref X, ref Y, ref D, true);
                                    if (X >= 0 && X < 8 && Y >= 0 && Y <= 8 && D >= 0 && D < 32 &&
                                        checkersMap[D] % 2 == course) {
                                        possibleMovesEatMap[D, 0] = 1;
                                        //TODO
                                    }
                                }
                                check = true;
                            }
                            if (z == 0 && (checkersMap[diagPos] % 2) == (checkersMap[fdiagPos] % 2))
                                break;
                            if (checkersMap[diagPos] != -1) {
                                if ((z == 0 || (checkersMap[diagPos] - 2 >= 0)) && (comboEatCell == -1 || comboEatCell == diagPos) &&
                                    ((checkersMap[fdiagPos] % 2) != course && (checkersMap[fdiagPos] % 2) != (checkersMap[diagPos] % 2) &&
                                    checkersMap[fdiagPos] != -1) && checkersMap[fdiagPos + backCell[k] + (fYdiag % 2 == 0 ? 1 : 0)] == -1) {
                                    needEat = true;
                                    possibleMovesEatMap[diagPos, 1] = 1;
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            int courseCount = 0;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 32; j++)
                    if(possibleMovesEatMap[j, i] != 0) courseCount++;
            if(courseCount == 0 && course == 0)
                Console.WriteLine("Победили Белые!");
            else if(courseCount == 0 && course == 1)
                Console.WriteLine("Победили Черные!");

        }

        void init() {
            ResizeMap();

            checkersMap = new int[] {
                 0, 0, 0, 0,
                 0, 0, 0, 0,
                 0, 0, 0, 0,
                -1,-1,-1,-1,
                -1,-1,-1,-1,
                 1, 1, 1, 1,
                 1, 1, 1, 1,
                 1, 1, 1, 1,
            };

            possibleMovesEatMap = new int[,] {
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                 { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
            };

            checkersForm = new Image[] { 
                Properties.Resources.black, 
                Properties.Resources.white, 
                Properties.Resources.blackQueen, 
                Properties.Resources.whiteQueen
            };

            course = Convert.ToInt32(player);
        }
        void ResizeMap() {
            dy = pictureBox1.Height;
            dx = pictureBox1.Width;

            sizeX = dx / 9f;
            sizeY = dy / 9.2f;

            mapCoordinate = new float[8, 2] { 
                { dx / 9.0f * 2.0f, dy / 9.0f   },
                { dx / 9.0f * 4.0f, dy / 9.0f * 2.0f },
                { dx / 9.0f * 6.0f, dy / 9.0f * 3.0f },
                { dx / 9.0f * 8.0f, dy / 9.0f * 4.0f },
                { dx / 9.0f,        dy / 9.0f * 5.0f },
                { dx / 9.0f * 3.0f, dy / 9.0f * 6.0f },
                { dx / 9.0f * 5.0f, dy / 9.0f * 7.0f },
                { dx / 9.0f * 7.0f, dy / 9.0f * 8.0f }
            };
            MaximumSize = new Size(Height-25, 1000);
            MinimumSize = new Size(Height-25, 300);
        }

        void soundPlay(object pesnya)
        {
            MediaPlayer mplayer = new MediaPlayer();
            mplayer.Open(new Uri(resourcePath + pesnya.ToString(), UriKind.Relative));
            mplayer.Play();
        }
    }
}
