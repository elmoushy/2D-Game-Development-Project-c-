using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace largegame
{
    public partial class Form1 : Form
    {
        //variables
        Bitmap of;
        Timer timer = new Timer();
        Bitmap m;
        node pnn;
        int scrollct = 0;
        int hh = 0;
        int killct0;
        int ct1 = 0;
        int ct2 = 0;
        int ct3 = 0;
        int ct4 = 0;
        int ss = 1;
        int yellowbulletct = 0;
        int multbullet = 0;
        int hrmovect = 0;
        int Singlect = 0;
        int Singlecount = 0;
        int monlist = 0;
        int eledir = 0;
        int gg = 0;
        int mon1move = 0;
        int loss = 0;
        int flag_gun = 0;
        int countsaw1 = 0;
        ///
        int enterct = 0;
        int runctr = 0;
        int runctl = 0;
        int flagspeed = 0;
        int countdown = 0;
        int ctjump = 0;
        int directionjump = 2;
        int jumpswitch = 0;
        int kx = 4;
        int countsawmove = 0;
        int scroll = 0;
        int paperwidth;
        int paperstart = 0;
        int paperstart2 = 0;
        int paperoneup = 0;
        int papertwoup;
        int paperonedown = 0;
        int papertwodown = 0;
        int walkingsteps = 0;
        int ct_run_right = 0;
        int ct_run_left = 0;
        int slow_move = 0;
        int slow_move2 = 0;
        int felevator = 0;
        int powerct = 0;
        int cthealth = 0;
        int stop = 0;
        int f = 0;
        Random R = new Random();
        //classes
        class node
        {
            public int x, y;
            public Bitmap v;
            public Size s;
            public int hampos;
            public int walpos;
        }
        //lists
        List<node> ground = new List<node>();
        List<node> hero = new List<node>();
        List<node> soldstock = new List<node>();
        List<node> leader = new List<node>();
        List<node> dacore = new List<node>();
        List<node> swit = new List<node>();
        List<node> kill = new List<node>();
        List<node> Move = new List<node>();
        List<node> hammer = new List<node>();
        List<node> mon1 = new List<node>();
        List<node> shot = new List<node>();
        List<node> power = new List<node>();
        List<node> poweradd = new List<node>();
        List<node> ele = new List<node>();
        List<node> newbullet = new List<node>();
        List<node> yellowmon = new List<node>();
        List<node> lazer = new List<node>();
        List<node> magic = new List<node>();
        List<node> bulleteme1 = new List<node>();
        List<node> dragon = new List<node>();
        List<node> dragonfire = new List<node>();
        List<node> hlt = new List<node>();
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            Paint += Form1_Paint;
            timer.Tick += Timer_Tick;
            timer.Start();
            // timer.Interval = 1;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.X + " " + e.Y);
        }

        //logic functions
        void leadermove(ref int ct)
        {
            if (ct == 1)
            {
                hero[0].v = new Bitmap("climb-1.png");
            }
            if (ct == 2)
            {

                hero[0].v = new Bitmap("climb-2.png");

            }
            if (ct == 3)
            {
                hero[0].v = new Bitmap("climb-3.png");
            }
            if (ct == 4)
            {
                hero[0].v = new Bitmap("climb-4.png");
            }
            if (ct == 5)
            {
                hero[0].v = new Bitmap("climb-5.png");
            }
            if (ct == 6)
            {
                hero[0].v = new Bitmap("climb-6.png");
                ct = 0;
            }

        }
        bool IsHit(node This, int X, int Y)
        {
            if (X >= This.x && X <= This.x + This.s.Width)
                if (Y >= This.y && Y <= This.y + This.s.Height)
                    return true;
            return false;
        }
        bool IfSomethingAbove(int Speed)
        {
            for (int i = 0; i < soldstock.Count; i++)
                for (int j = 0; j < hero[0].s.Width; j++)
                    for (int k = 0; k < Speed; k++)
                        if (IsHit(soldstock[i], hero[0].x + j, hero[0].y - k))
                            return false;
            return true;
        }
        bool wall_check(int dir, int Speed)
        {
            if (dir == 1)
            {
                for (int i = 0; i < soldstock.Count; i++)
                    for (int j = 0; j < Speed; j++)
                        for (int k = 0; k < hero[0].s.Height; k++)
                            if (IsHit(soldstock[i], hero[0].x + hero[0].s.Width + j, hero[0].y + k) && soldstock[i] != hero[0])
                                return false;
            }
            if (dir == 2)
            {
                for (int i = 0; i < soldstock.Count; i++)
                    for (int j = 0; j < Speed; j++)
                        for (int k = 0; k < hero[0].s.Height; k++)
                            if (IsHit(soldstock[i], hero[0].x - j, hero[0].y + k) && soldstock[i] != hero[0])
                                return false;

            }
            return true;
        }
        void gravity()
        {
            if (jumpswitch == 0)
            {
                int min = 9999;
                int pos = 0;
                for (int i = 0; i < soldstock.Count; i++)
                {
                    if (hero[0].x >= soldstock[i].x - 40 && hero[0].x + hero[0].s.Width <= soldstock[i].x + soldstock[i].s.Width + 30)
                    {
                        if ((soldstock[i].y) > hero[0].y)
                        {
                            if (min > (soldstock[i].y))
                            {
                                min = (soldstock[i].y);
                                pos = i;
                            }
                        }
                    }
                }
                if (min != 9999)
                {
                    while (soldstock[pos].y - hero[0].s.Height > hero[0].y)
                    {
                        hero[0].y += 1;
                    }
                }
            }
            if (jumpswitch == 1)
            {
                ctjump++;

                if (directionjump == 1 && IfSomethingAbove(30) && wall_check(directionjump, 30))
                {

                    if (ctjump % 1 == 0)
                    {
                        hero[0].x += 30;
                        hero[0].v = new Bitmap("jump-1.png");
                        hero[0].y -= 30;
                    }
                    if (ctjump % 2 == 0)
                    {
                        hero[0].x += 30;
                        hero[0].v = new Bitmap("jump-2.png");
                        hero[0].y -= 30;
                    }
                    if (ctjump % 3 == 0)
                    {
                        hero[0].x += 30;
                        hero[0].v = new Bitmap("jump-3.png");
                        hero[0].y -= 30;
                    }
                    if (ctjump % 4 == 0)
                    {
                        hero[0].x += 30;
                        hero[0].v = new Bitmap("jump-4.png");

                        hero[0].y -= 30;
                    }
                    if (ctjump % 5 == 0)
                    {
                        hero[0].y += 30;
                        hero[0].v = new Bitmap("walk-1.png");
                        jumpswitch = 0;
                        gravity();
                    }
                }
                if (directionjump == 1 && (!IfSomethingAbove(30) || !wall_check(directionjump, 30)))
                {
                    jumpswitch = 0;
                    hero[0].v = new Bitmap("walk-1.png");
                }
                if (directionjump == 2 && (IfSomethingAbove(30) && wall_check(directionjump, 30)))
                {
                    if (soldstock[0].x + 40 < 0)
                    {
                        paperwidth--;
                        paperstart--;
                    }
                    else
                    {
                        paperstart = 0;
                    }

                    if (ctjump % 1 == 0)
                    {
                        hero[0].x -= 30;
                        hero[0].v = new Bitmap("jump1.png");
                        hero[0].y -= 30;
                    }
                    if (ctjump % 2 == 0)
                    {
                        hero[0].x -= 30;
                        hero[0].v = new Bitmap("jump2.png");
                        hero[0].y -= 30;
                    }
                    if (ctjump % 3 == 0)
                    {
                        hero[0].x -= 30;
                        hero[0].v = new Bitmap("jump3.png");
                        hero[0].y -= 30;
                    }
                    if (ctjump % 4 == 0)
                    {
                        hero[0].x -= 30;
                        hero[0].v = new Bitmap("jump4.png");

                        hero[0].y -= 30;
                    }
                    if (ctjump % 5 == 0)
                    {
                        hero[0].y += 30;
                        hero[0].v = new Bitmap("walk1.png");
                        jumpswitch = 0;
                        gravity();
                    }
                }
                if (directionjump == 2 && (!IfSomethingAbove(30) || !wall_check(directionjump, 30)))
                {
                    jumpswitch = 0;
                    hero[0].v = new Bitmap("walk1.png");
                }
            }
        }
        void screen_move(int dir, int x, int y)
        {
            if (dir == 1)
            {
                for (int i = 0; i < soldstock.Count; i++)
                {
                    soldstock[i].x += x;
                }
                for (int i = 0; i < leader.Count; i++)
                {
                    leader[i].x += x;
                }
                for (int i = 0; i < dacore.Count; i++)
                {
                    dacore[i].x += x;
                }
                for (int i = 0; i < swit.Count; i++)
                {
                    swit[i].x += x;
                }
                for (int i = 0; i < kill.Count; i++)
                {
                    kill[i].x += x;
                }
                for (int i = 0; i < Move.Count; i++)
                {
                    Move[i].x += x;
                }
                for (int i = 0; i < hlt.Count; i++)
                {
                    hlt[i].x += x;
                }
                for (int i = 0; i < hammer.Count; i++)
                {
                    hammer[i].x += x;
                }
                for (int i = 0; i < mon1.Count; i++)
                {
                    mon1[i].x += x;
                }
                for (int i = 0; i < shot.Count; i++)
                {
                    shot[i].x += x;
                }
                for (int i = 0; i < ele.Count; i++)
                {
                    ele[i].x += x;
                }
                for (int i = 0; i < hero.Count; i++)
                {
                    hero[i].x += x;
                }
                for (int i = 0; i < poweradd.Count; i++)
                {
                    poweradd[i].x += x;
                }
                for (int i = 0; i < newbullet.Count; i++)
                {
                    newbullet[i].x += x;
                }
                for (int i = 0; i < yellowmon.Count; i++)
                {
                    yellowmon[i].x += x;
                }
                for (int i = 0; i < dragon.Count; i++)
                {
                    dragon[i].x += x;
                }
                for (int i = 0; i < dragonfire.Count; i++)
                {
                    dragonfire[i].x += x;
                }
                for (int i = 0; i < magic.Count; i++)
                {
                    magic[i].x += x;
                }
                for (int i = 0; i < bulleteme1.Count; i++)
                {
                    bulleteme1[i].x += x;
                }
                for (int i = 0; i < lazer.Count; i++)
                {
                    lazer[i].x += x;
                    lazer[i].walpos += x;
                }


            }
            if (dir == 2)
            {
                for (int i = 0; i < soldstock.Count; i++)
                {
                    soldstock[i].y += y;
                }
                for (int i = 0; i < ground.Count; i++)
                {
                    ground[i].y += y;
                }
                for (int i = 0; i < leader.Count; i++)
                {
                    leader[i].y += y;
                }
                for (int i = 0; i < dacore.Count; i++)
                {
                    dacore[i].y += y;
                }
                for (int i = 0; i < swit.Count; i++)
                {
                    swit[i].y += y;
                }
                for (int i = 0; i < kill.Count; i++)
                {
                    kill[i].y += y;
                }
                for (int i = 0; i < Move.Count; i++)
                {
                    Move[i].y += y;
                }
                for (int i = 0; i < hlt.Count; i++)
                {
                    hlt[i].y += y;
                }
                for (int i = 0; i < hammer.Count; i++)
                {
                    hammer[i].y += y;
                }
                for (int i = 0; i < ele.Count; i++)
                {
                    ele[i].y += y;
                }
                for (int i = 0; i < mon1.Count; i++)
                {
                    mon1[i].y += y;
                }
                for (int i = 0; i < shot.Count; i++)
                {
                    shot[i].y += y;
                }
                for (int i = 0; i < poweradd.Count; i++)
                {
                    poweradd[i].y += y;
                }
                for (int i = 0; i < newbullet.Count; i++)
                {
                    newbullet[i].y += y;
                }
                for (int i = 0; i < yellowmon.Count; i++)
                {
                    yellowmon[i].y += y;
                }
                for (int i = 0; i < dragon.Count; i++)
                {
                    dragon[i].y += y;
                }
                for (int i = 0; i < dragonfire.Count; i++)
                {
                    dragonfire[i].y += y;
                }
                for (int i = 0; i < magic.Count; i++)
                {
                    magic[i].y += y;
                }
                for (int i = 0; i < bulleteme1.Count; i++)
                {
                    bulleteme1[i].y += y;
                }
                for (int i = 0; i < lazer.Count; i++)
                {
                    lazer[i].y += y;
                }

            }
        }
        void putblocks(Bitmap m, int x, int y, int w, int h, List<node> l)
        {
            pnn.x = x;
            pnn.y = y;
            pnn.v = m;
            pnn.s.Width = m.Width - w;
            pnn.s.Height = m.Height - h;
            l.Add(pnn);
        }
        void health(int ct)
        {
            if (ct == 0)
            {
                power[0].v = new Bitmap("D6.png");
            }
            if (ct == 1)
            {
                power[0].v = new Bitmap("D5.png");
            }
            if (ct == 2)
            {
                power[0].v = new Bitmap("D4.png");
            }
            if (ct == 3)
            {
                power[0].v = new Bitmap("D3.png");
            }
            if (ct == 4)
            {
                power[0].v = new Bitmap("D2.png");
            }
            if (ct == 5)
            {
                power[0].v = new Bitmap("D1.png");
            }
            if (ct == 6)
            {
                loss = 1;
            }
        }
        void lazermove(ref int ct, int i)
        {

            if (ct == 18)
            {
                lazer[i].x = lazer[i].walpos;
                ct = 0;
            }
            else
            {
                lazer[i].x = (lazer[i].walpos * 7);
                ct++;
            }
        }

        public void move(object sender, KeyEventArgs e, int dir, ref int ct, int velocity)
        {
            if (ct == 1)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-1.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk1.png");

                }
            }
            if (ct == 2)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-2.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk2.png");

                }
            }
            if (ct == 3)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-3.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk3.png");
                }
            }
            if (ct == 4)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-4.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk4.png");
                }
            }
            if (ct == 5)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-5.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk5.png");
                }

            }
            if (ct == 6)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-6.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk6.png");
                }

            }
            if (ct == 7)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-7.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk7.png");
                }

            }
            if (ct == 8)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-8.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk8.png");
                }

            }
            if (ct == 9)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-9.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk9.png");
                }

            }
            if (ct == 10)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-10.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk10.png");
                }

            }
            if (ct == 11)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-11.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk11.png");
                }

            }
            if (ct == 12)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-12.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk12.png");
                }

            }
            if (ct == 13)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-13.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk13.png");
                }

            }
            if (ct == 14)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-14.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk14.png");
                }

            }
            if (ct == 15)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-15.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk15.png");
                }

            }
            if (ct == 16)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("walk-16.png");
                }
                else
                {
                    hero[0].v = new Bitmap("walk16.png");
                }
                ct = 0;
            }

        }
        public void run(object sender, KeyEventArgs e, int dir, ref int ct, int velocity)
        {
            if (ct == 1)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-1.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run1.png");

                }
            }
            if (ct == 2)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-2.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run2.png");

                }
            }
            if (ct == 3)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-3.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run3.png");
                }
            }
            if (ct == 4)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-4.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run4.png");
                }
            }
            if (ct == 5)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-5.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run5.png");
                }
            }
            if (ct == 6)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-6.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run6.png");
                }

            }
            if (ct == 7)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-7.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run7.png");
                }

            }
            if (ct == 8)
            {
                hero[0].x += velocity;
                if (dir == 1)
                {
                    hero[0].v = new Bitmap("run-8.png");
                }
                else
                {
                    hero[0].v = new Bitmap("run8.png");
                }
                ct = 0;

            }
        }
        void hrmove(ref int ct)
        {
            for (int i = 31; i < 34; i++)
            {
                if (ct == 1)
                {
                    soldstock[i].v = new Bitmap("Transporter2.png");
                }
                if (ct == 2)
                {
                    soldstock[i].v = new Bitmap("Transporter3.png");
                }
                if (ct == 3)
                {
                    soldstock[i].v = new Bitmap("Transporter4.png");
                }
                if (ct == 4)
                {
                    soldstock[i].v = new Bitmap("Transporter1.png");
                    ct = 0;
                }
            }
        }
        void dragonmove(ref int ct)
        {
            if (ct == 1)
            {
                dragon[0].v = new Bitmap("g1.png");
                dragon[0].walpos = 1;
            }
            if (ct >= 20 && ct <= 30)
            {
                if (ct % 2 == 0)
                {
                    dragon[0].v = new Bitmap("g3.png");
                }
                if (ct % 2 != 0)
                {
                    dragon[0].v = new Bitmap("g2.png");
                }
                dragon[0].x += 10;
                dragon[0].y -= 50;
                dragon[0].walpos = 0;
            }
            if (ct >= 31 && ct <= 45)
            {
                dragon[0].v = new Bitmap("g4.png");
                dragon[0].walpos = 2;
            }
            if (ct >= 46 && ct <= 70)
            {
                dragon[0].v = new Bitmap("gm.png");
                dragon[0].x += 60;
                dragon[0].walpos = 0;
            }
            if (ct >= 70 && ct <= 85)
            {
                dragon[0].v = new Bitmap("gr4.png");
                dragon[0].walpos = 3;
            }
            if (ct >= 70 && ct <= 85)
            {
                dragon[0].v = new Bitmap("gr4.png");
            }
            if (ct >= 86 && ct <= 96)
            {
                if (ct % 2 == 0)
                {
                    dragon[0].v = new Bitmap("gr3.png");
                }
                if (ct % 2 != 0)
                {
                    dragon[0].v = new Bitmap("gr2.png");
                }
                dragon[0].x += 10;
                dragon[0].y += 50;
                dragon[0].walpos = 0;
            }
            if (ct >= 97 && ct <= 107)
            {
                dragon[0].v = new Bitmap("gr2.png");
                dragon[0].walpos = 4;
            }
            if (ct >= 108 && ct <= 118)
            {
                if (ct % 2 == 0)
                {
                    dragon[0].v = new Bitmap("gr3.png");
                }
                if (ct % 2 != 0)
                {
                    dragon[0].v = new Bitmap("gr2.png");
                }
                dragon[0].x -= 10;
                dragon[0].y -= 50;
                dragon[0].walpos = 0;
            }
            if (ct >= 118 && ct <= 142)
            {
                dragon[0].v = new Bitmap("gr4.png");
                dragon[0].walpos = 3;
            }
            if (ct >= 128 && ct <= 152)
            {
                dragon[0].v = new Bitmap("gmr.png");
                dragon[0].x -= 60;
                dragon[0].walpos = 0;
            }
            if (ct >= 152 && ct <= 166)
            {
                dragon[0].v = new Bitmap("g4.png");
                dragon[0].walpos = 2;
            }
            if (ct >= 167 && ct <= 177)
            {
                if (ct % 2 == 0)
                {
                    dragon[0].v = new Bitmap("g3.png");
                }
                if (ct % 2 != 0)
                {
                    dragon[0].v = new Bitmap("g2.png");
                }
                dragon[0].x -= 10;
                dragon[0].y += 50;
                dragon[0].walpos = 0;
            }
            if (ct == 177)
            {
                ct = -1;
            }
        }
        void dragonfiree(int ct)
        {
            ct1++;
            ct2++;
            ct3++;
            ct4++;
            if (ct == 1)
            {
                if (ct1 % 7 == 0)
                {
                    m = new Bitmap("DB.png");
                    pnn = new node();
                    pnn.walpos = 1;
                    putblocks(m, dragon[0].x + dragon[0].s.Width, dragon[0].y + 270, 20, 20, dragonfire);
                }

            }
            if (ct == 2)
            {
                if (ct2 % 5 == 0)
                {
                    m = new Bitmap("DB.png");
                    pnn = new node();
                    int valuex = R.Next(10, 50);
                    pnn.hampos = valuex;
                    pnn.walpos = 2;
                    putblocks(m, dragon[0].x + dragon[0].s.Width - 25, dragon[0].y + 80, 20, 20, dragonfire);
                    m = new Bitmap("DB.png");
                    pnn = new node();
                    valuex = R.Next(10, 50);
                    pnn.hampos = valuex;
                    pnn.walpos = 2;
                    putblocks(m, dragon[0].x + dragon[0].s.Width - 25, dragon[0].y + 80, 20, 20, dragonfire);
                }


            }
            if (ct == 3)
            {
                if (ct3 % 5 == 0)
                {

                    m = new Bitmap("DB.png");
                    pnn = new node();
                    int valuex = R.Next(10, 100);
                    pnn.hampos = valuex;
                    pnn.walpos = 3;
                    putblocks(m, dragon[0].x - 25, dragon[0].y + 80, 20, 20, dragonfire);
                    m = new Bitmap("DB.png");
                    pnn = new node();
                    valuex = R.Next(10, 50);
                    pnn.hampos = valuex;
                    pnn.walpos = 3;
                    putblocks(m, dragon[0].x - 25, dragon[0].y + 80, 20, 20, dragonfire);

                }
            }
            if (ct == 4)
            {
                if (ct4 % 7 == 0)
                {
                    m = new Bitmap("DB.png");
                    pnn = new node();
                    pnn.walpos = 4;
                    putblocks(m, dragon[0].x, dragon[0].y + 270, 20, 20, dragonfire);
                }

            }
            for (int i = 0; i < dragonfire.Count; i++)
            {
                if (dragonfire.Count >= 1)
                {
                    if (dragonfire[i].walpos == 1 && i >= 0)
                    {
                        dragonfire[i].x += 80;
                        if (dragonfire[i].x > (this.ClientSize.Width * 2))
                        {
                            dragonfire.RemoveAt(i);
                            i--;
                        }

                    }
                }
                if (dragonfire.Count >= 1 && i >= 0)
                {
                    if (dragonfire[i].walpos == 2)
                    {
                        dragonfire[i].x += dragonfire[i].hampos;
                        dragonfire[i].y += 30;
                        if (dragonfire[i].y > this.ClientSize.Height)
                        {
                            dragonfire.RemoveAt(i);
                            i--;
                        }
                    }
                }
                if (dragonfire.Count >= 1 && i >= 0)
                {
                    if (dragonfire[i].walpos == 3)
                    {

                        dragonfire[i].x -= dragonfire[i].hampos;
                        dragonfire[i].y += 30;
                        if (dragonfire[i].y > this.ClientSize.Height)
                        {
                            dragonfire.RemoveAt(i);
                            i--;
                        }

                    }
                }
                if (dragonfire.Count >= 1 && i >= 0)
                {
                    if (dragonfire[i].walpos == 4)
                    {

                        dragonfire[i].x -= 80;
                        if (dragonfire[i].x < 400)
                        {
                            dragonfire.RemoveAt(i);
                            i--;
                        }


                    }
                }

            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            flagspeed = 0;
            // flag_gun = 0;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /* if (e.KeyCode == Keys.V)
             {
                 screen_move(1, -2000, 0);
             }
             if (e.KeyCode == Keys.B)
             {
                 screen_move(2, 0, -100);
             }*/
            if (e.KeyCode == Keys.Enter && enterct == 0)
            {
                for (int i = 0; i < swit.Count; i++)
                {
                    if (i == 0)
                    {
                        if ((hero[0].x) <= swit[i].x + swit[i].s.Width && (hero[0].x) + hero[0].s.Width >= swit[i].x)
                        {
                            if ((hero[0].y) <= swit[i].y + swit[i].s.Height && (hero[0].y) + hero[0].s.Height >= swit[i].y)
                            {
                                //note must 1
                                enterct = 1;
                                swit[0].v = new Bitmap("lever2.png");
                                felevator = 1;

                            }
                        }
                    }
                }

            }
            if (e.KeyCode == Keys.Enter && enterct == 1)
            {
                for (int i = 0; i < swit.Count; i++)
                {
                    if (i == 1)
                    {
                        if ((hero[0].x) <= swit[i].x + swit[i].s.Width && (hero[0].x) + hero[0].s.Width >= swit[i].x)
                        {
                            if ((hero[0].y) <= swit[i].y + swit[i].s.Height && (hero[0].y) + hero[0].s.Height >= swit[i].y)
                            {
                                enterct = 2;
                                swit[i].v = new Bitmap("lever2.png");
                                for (int k = 0; k < 10; k++)
                                {
                                    screen_move(1, -5, 0);
                                    draw(this.CreateGraphics());
                                }
                                for (int h = 0; h < 5; h++)
                                {
                                    if (h == 0)
                                    {
                                        soldstock[16].v = new Bitmap("Door1.png");
                                    }
                                    if (h == 2)
                                    {
                                        soldstock[16].v = new Bitmap("Door2.png");
                                    }
                                    if (h == 4)
                                    {
                                        soldstock[16].v = new Bitmap("Door3.png");
                                    }
                                    draw(this.CreateGraphics());
                                }
                            }
                        }
                    }
                }
            }
            //////////////////////////////////
            if (e.KeyCode == Keys.F && Singlect == 0)
            {
                if (multbullet == 0)
                {
                    Singlect = 1;
                }
                flag_gun = 0;
                if (directionjump == 1)
                {
                    hero[0].v = new Bitmap("shoot.png");
                    m = new Bitmap("shot.png");
                    pnn = new node();
                    pnn.hampos = 1;
                    putblocks(m, (hero[0].x + hero[0].s.Width), (hero[0].y + 20), 0, 0, shot);
                }
                if (directionjump == 2)
                {
                    m = new Bitmap("shot.png");
                    pnn = new node();
                    pnn.hampos = 2;
                    putblocks(m, (hero[0].x), (hero[0].y + 20), 0, 0, shot);
                    hero[0].v = new Bitmap("shootl.png");
                }
            }
            if (e.KeyCode == Keys.Tab)
            {
                flagspeed = 1;
            }
            if (e.KeyCode == Keys.Right)
            {
                directionjump = 1;
                //screen_move(1, -5, 0);
                if (flagspeed == 0)
                {
                    if (wall_check(directionjump, 12))
                    {
                        if (walkingsteps <= 370)
                        {
                            paperwidth++;
                            //paperstart++;
                            //screen_move(1, -10, 0);
                        }
                        walkingsteps++;
                        ct_run_right++;
                        move(sender, e, 1, ref ct_run_right, 12);
                        ct_run_left = 0;
                    }
                }
                if (flagspeed == 1)
                {
                    if (wall_check(directionjump, 50))
                    {
                        if (walkingsteps <= 370)
                        {
                            // paperwidth++;
                            //paperstart++;
                            //screen_move(1, -20, 0);
                        }
                        walkingsteps += 35;
                        runctr++;
                        run(sender, e, 1, ref runctr, 50);
                        runctl = 0;
                    }
                }

            }
            if (e.KeyCode == Keys.Left)
            {
                directionjump = 2;
                if (flagspeed == 0)
                {
                    if (wall_check(directionjump, 12))
                    {
                        if (walkingsteps >= 1)
                        {
                            /* paperwidth--;
                             paperstart--;*/
                            //screen_move(1, 5, 0);
                        }
                        if (soldstock[0].x < 0)
                        {
                            //paperwidth--;
                            //paperstart--;
                            //screen_move(1, 10, 0);
                            walkingsteps--;
                        }
                        ct_run_left++;
                        move(sender, e, 2, ref ct_run_left, -12);
                        ct_run_right = 0;
                    }
                }
                if (flagspeed == 1)
                {
                    if (wall_check(directionjump, 50))
                    {
                        if (soldstock[0].x < 0)
                        {
                            //paperwidth--;
                            //paperstart -= 10;
                            //screen_move(1, 20, 0);
                            walkingsteps--;
                        }
                        walkingsteps -= 35;
                        runctl++;
                        run(sender, e, 2, ref runctl, -50);
                        runctr = 0;
                    }
                }

            }
            if (e.KeyCode == Keys.Down)
            {
                countdown++;

                for (int i = 0; i < leader.Count; i++)
                {
                    if (hero[0].x >= leader[i].x && hero[0].x + hero[0].s.Width <= leader[i].x + leader[i].s.Width)
                    {

                        if ((hero[0].y + hero[0].s.Height) > (leader[i].y + leader[i].s.Height))
                        {
                            jumpswitch = 0;
                            gg = 0;
                            hero[i].v = new Bitmap("walk-1.png");
                            countdown = 0;

                        }
                        else
                        {
                            hero[0].y++;
                            gg = 1;
                            jumpswitch = 2;
                            leadermove(ref countdown);
                            screen_move(2, 0, -5);

                        }
                    }
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                countdown++;
                for (int i = 0; i < leader.Count; i++)
                {
                    if (hero[0].x >= leader[i].x && hero[0].x + hero[0].s.Width <= leader[i].x + leader[i].s.Width)
                    {
                        if (ground[0].y + ground[0].s.Height < this.ClientSize.Height)
                        {
                            hero[0].y--;
                            jumpswitch = 2;
                            gg = 0;
                            screen_move(2, 0, 5);
                            leadermove(ref countdown);
                        }
                    }
                    else
                    {
                        countdown = 0;
                        gg = 1;
                        jumpswitch = 0;
                    }
                }


            }
            if (e.KeyCode == Keys.Space && gg == 0)
            {

                jumpswitch = 1;
                //jump(1, ctjump);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(dragon.Count>=1)
            {
                if (loss == 0)
                {
                    if (f == 1 && dragon.Count >= 1)
                    {
                        dragon[0].hampos++;
                        dragonmove(ref dragon[0].hampos);
                        dragonfiree(dragon[0].walpos);
                        stop = 1;
                    }
                    gravity();
                    hrmovect++;
                    for (int i = 0; i < lazer.Count; i++)
                    {
                        lazermove(ref lazer[i].hampos, i);
                    }
                    if (enterct == 2)
                    {
                        if ((hero[0].x) <= soldstock[16].x + soldstock[16].s.Width && (hero[0].x) + hero[0].s.Width + 10 >= soldstock[16].x)
                        {
                            if ((hero[0].y) <= soldstock[16].y + soldstock[16].s.Height && (hero[0].y) + hero[0].s.Height >= soldstock[16].y)
                            {
                                int xpos = hero[0].x;
                                for (int i = 0; i < 20; i++)
                                {
                                    xpos -= 100;

                                    if (i == 19)
                                    {
                                        screen_move(1, -50, 0);
                                        hero[0].x = xpos + 200;
                                    }
                                    else
                                    {
                                        screen_move(1, -100, 0);
                                    }
                                    gg = 0;
                                    draw(this.CreateGraphics());
                                    enterct = 3;
                                    ss = 0;
                                }

                            }
                        }
                    }
                    hrmove(ref hrmovect);
                    cthealth++;
                    if (cthealth == 1)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p1.png");
                        }
                    }
                    if (cthealth == 2)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p2.png");
                        }
                    }
                    if (cthealth == 3)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p3.png");
                        }
                    }
                    if (cthealth == 4)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p4.png");
                        }
                    }
                    if (cthealth == 5)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p5.png");
                        }
                    }
                    if (cthealth == 6)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p6.png");
                        }
                    }
                    if (cthealth == 7)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p7.png");
                        }
                    }
                    if (cthealth == 8)
                    {
                        for (int i = 0; i < poweradd.Count; i++)
                        {
                            poweradd[i].v = new Bitmap("p8.png");
                        }
                        cthealth = 0;
                    }
                    for (int i = 0; i < mon1.Count; i++)
                    {
                        if (mon1[i].y < hero[0].y + 20)
                        {
                            if (mon1[i].x < hero[0].x)
                            {
                                mon1move = 1;
                                mon1[i].hampos = 1;
                            }
                            if (mon1[i].x > hero[0].x + hero[0].s.Width)
                            {
                                mon1move = 2;
                                mon1[i].hampos = 2;
                            }

                        }
                    }
                    if (newbullet.Count == 1)
                    {
                        if ((hero[0].x) <= newbullet[0].x + newbullet[0].s.Width && (hero[0].x) + hero[0].s.Width >= newbullet[0].x)
                        {
                            if ((hero[0].y) <= newbullet[0].y + newbullet[0].s.Height && (hero[0].y) + hero[0].s.Height >= newbullet[0].y)
                            {
                                multbullet = 1;
                                newbullet.RemoveAt(0);
                            }
                        }
                    }

                    //single pullet
                    if (Singlect == 1)
                    {
                        for (int i = 0; i < soldstock.Count; i++)
                        {
                            if (shot.Count == 1)
                            {
                                if ((shot[0].x) <= soldstock[i].x + soldstock[i].s.Width && (shot[0].x) + shot[0].s.Width >= soldstock[i].x)
                                {
                                    if ((shot[0].y) <= soldstock[i].y + soldstock[i].s.Height && (shot[0].y) + shot[0].s.Height >= soldstock[i].y)
                                    {
                                        shot.RemoveAt(0);
                                        Singlect = 0;
                                        Singlecount = 0;
                                    }
                                }
                            }
                            else
                            {
                                Singlect = 0;
                            }

                        }
                        Singlecount++;
                        if (Singlecount == 10)
                        {
                            if (shot.Count == 1)
                            {
                                shot.RemoveAt(0);
                            }

                            Singlecount = 0;
                            Singlect = 0;
                        }
                    }
                    if (mon1move != 0)
                    {
                        for (int i = 0; i < mon1.Count; i++)
                        {
                            if (mon1[i].hampos == 1)
                            {
                                mon1[i].x += 5;
                                ele[i].x += 5;
                                mon1[i].v = new Bitmap("monistarmove-0.png");
                            }
                            else
                            {
                                mon1[i].x -= 5;
                                ele[i].x -= 5;
                                mon1[i].v = new Bitmap("monistarmove.png");

                            }
                        }
                    }
                    ///////////////////////////////////////////////////////////////////////////////
                    if (mon1.Count == 0)
                    {
                        if (soldstock[21].y - 12 > soldstock[17].y)
                        {
                            eledir = 1;
                        }
                        if (soldstock[21].y < soldstock[22].y)
                        {
                            eledir = 0;
                        }
                        if (eledir == 0)
                        {
                            soldstock[21].y += 10;
                        }
                        if (eledir == 1 && hero[0].x > soldstock[21].x && hero[0].x < soldstock[21].x + soldstock[21].s.Width)
                        {
                            soldstock[21].y -= 10;

                            if (hero[0].x > soldstock[21].x && hero[0].x < soldstock[21].x + soldstock[21].s.Width)
                            {
                                hero[0].y -= 10;
                            }
                        }
                        else
                        {
                            eledir = 0;
                        }
                    }
                    flag_gun++;
                    for (int i = 0; i < shot.Count; i++)
                    {
                        if (shot[i].hampos == 1)
                        {
                            shot[i].x += 60;
                        }
                        else
                        {
                            shot[i].x -= 60;
                        }
                    }
                    for (int i = 0; i < shot.Count; i++)
                    {
                        if (i >= 0)
                        {
                            if (shot[i].x < 0 && i >= 0)
                            {
                                shot.RemoveAt(i);
                                i--;
                            }
                        }
                        if (i >= 0)
                        {
                            if (shot[i].x > (this.ClientSize.Width * 2))
                            {
                                shot.RemoveAt(i);
                                i--;
                            }
                        }

                    }
                    for (int i = 0; i < kill.Count; i++)
                    {
                        if ((hero[0].x) <= kill[i].x + kill[i].s.Width && (hero[0].x) + hero[0].s.Width >= kill[i].x)
                        {
                            if ((hero[0].y) <= kill[i].y + kill[i].s.Height && (hero[0].y) + hero[0].s.Height >= kill[i].y)
                            {
                                powerct++;
                                health(powerct);
                            }
                        }
                    }
                    for (int i = 0; i < mon1.Count; i++)
                    {
                        if ((hero[0].x) <= mon1[i].x + mon1[i].s.Width && (hero[0].x) + hero[0].s.Width >= mon1[i].x)
                        {
                            if ((hero[0].y) <= mon1[i].y + mon1[i].s.Height && (hero[0].y) + hero[0].s.Height >= mon1[i].y)
                            {
                                powerct++;
                                health(powerct);
                            }
                        }
                    }
                    //kill
                    for (int i = 0; i < shot.Count; i++)
                    {
                        if ((dragon[0].x) <= shot[i].x + shot[i].s.Width && (dragon[0].x) + dragon[0].s.Width >= shot[i].x)
                        {
                            if ((dragon[0].y) <= shot[i].y + shot[i].s.Height && (dragon[0].y) + dragon[0].s.Height >= shot[i].y)
                            {
                                /////////////////////////////
                                hlt[0].s.Width -= 25;
                                if (hlt[0].s.Width <= 0)
                                {
                                    dragon.RemoveAt(0);
                                    timer.Stop();
                                    MessageBox.Show("you win");
                                }
                            }
                        }
                    }
                    for (int i = 0; i < dragon.Count; i++)
                    {
                        if ((hero[0].x) <= dragon[i].x + dragon[i].s.Width && (hero[0].x) + hero[0].s.Width >= dragon[i].x)
                        {
                            if ((hero[0].y) <= dragon[i].y + dragon[i].s.Height && (hero[0].y) + hero[0].s.Height >= dragon[i].y)
                            {
                                /////////////////////////////
                                timer.Stop();
                                MessageBox.Show("you loss");

                            }
                        }
                    }
                    for (int i = 0; i < dragonfire.Count; i++)
                    {
                        if ((hero[0].x) <= dragonfire[i].x + dragonfire[i].s.Width && (hero[0].x) + hero[0].s.Width >= dragonfire[i].x)
                        {
                            if ((hero[0].y) <= dragonfire[i].y + dragonfire[i].s.Height && (hero[0].y) + hero[0].s.Height >= dragonfire[i].y)
                            {
                                powerct++;
                                health(powerct);
                            }
                        }
                    }
                    if (mon1.Count != 0)
                    {
                        for (int i = 0; i < shot.Count; i++)
                        {
                            if ((mon1[0].x) <= shot[i].x + shot[i].s.Width && (mon1[0].x) + mon1[0].s.Width >= shot[i].x)
                            {
                                if ((mon1[0].y) <= shot[i].y + shot[i].s.Height && (mon1[0].y) + mon1[0].s.Height >= shot[i].y)
                                {
                                    mon1[0].walpos++;
                                    ele[0].s.Width -= 10;
                                    shot.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }
                    if (mon1.Count > 1)
                    {
                        for (int i = 0; i < shot.Count; i++)
                        {
                            if ((mon1[1].x) <= shot[i].x + shot[i].s.Width && (mon1[1].x) + mon1[1].s.Width >= shot[i].x)
                            {
                                if ((mon1[1].y) <= shot[i].y + shot[i].s.Height && (mon1[1].y) + mon1[1].s.Height >= shot[i].y)
                                {
                                    mon1[1].walpos++;
                                    ele[1].s.Width -= 10;
                                    shot.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }
                    for (int i = 31; i < 34; i++)
                    {
                        if ((hero[0].x) <= soldstock[i].x + soldstock[i].s.Width && (hero[0].x) + hero[0].s.Width >= soldstock[i].x)
                        {
                            if ((hero[0].y) <= soldstock[i].y + soldstock[i].s.Height && (hero[0].y) + hero[0].s.Height >= soldstock[i].y)
                            {
                                hero[0].x -= 7;
                            }
                        }
                    }
                    for (int i = 0; i < poweradd.Count; i++)
                    {
                        if ((hero[0].x) <= poweradd[i].x + poweradd[i].s.Width && (hero[0].x) + hero[0].s.Width >= poweradd[i].x)
                        {
                            if ((hero[0].y) <= poweradd[i].y + poweradd[i].s.Height && (hero[0].y) + hero[0].s.Height >= poweradd[i].y)
                            {
                                if (powerct >= 1)
                                {
                                    powerct -= 2;
                                    if (powerct == 0)
                                    {
                                        powerct++;
                                    }
                                    if (powerct < 0)
                                    {
                                        powerct = 0;
                                    }
                                    health(powerct);
                                }
                                else
                                {
                                    power[0].v = new Bitmap("D6.png");
                                }
                                poweradd.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    //////////magic door
                    for (int i = 0; i < magic.Count; i++)
                    {
                        if ((hero[0].x) <= magic[i].x + magic[i].s.Width && (hero[0].x) + hero[0].s.Width >= magic[i].x)
                        {
                            if ((hero[0].y) <= magic[i].y + magic[i].s.Height && (hero[0].y) + hero[0].s.Height >= magic[i].y)
                            {
                                ss = 1;
                                magic.RemoveAt(i);
                                i--;
                                f = 1;
                                //move up
                                int hpos = hero[0].y - 300;
                                hero[0].y -= 3000;
                                stop = 1;
                                for (int h = 0; h < 60; h++)
                                {
                                    screen_move(2, 0, 10);
                                    draw(this.CreateGraphics());
                                }
                                hero[0].y -= hpos;
                                hero[0].y = dragon[0].y - 100;
                            }
                        }
                    }
                    for (int i = 0; i < mon1.Count; i++)
                    {
                        if (mon1[i].walpos >= 6)
                        {
                            mon1.RemoveAt(i);
                            ele.RemoveAt(i);
                            i--;
                        }
                    }
                    if (stop == 0)
                    {
                        for (int i = 0; i < hammer.Count; i++)
                        {
                            if ((hero[0].x) <= hammer[i].x + hammer[i].s.Width && (hero[0].x) + hero[0].s.Width >= hammer[i].x)
                            {
                                if ((hero[0].y) <= hammer[i].y + hammer[i].s.Height && (hero[0].y) + hero[0].s.Height >= hammer[i].y)
                                {
                                    health(powerct);
                                    powerct++;
                                }
                            }
                        }
                    }
                    if (ss == 0)
                    {
                        yellowbulletct++;
                        ////////////////////////////////
                        if (yellowbulletct % 10 == 0)
                        {
                            m = new Bitmap("bulletss.png");
                            pnn = new node();
                            pnn.hampos = 0;
                            putblocks(m, this.ClientSize.Width + cc - 774, (this.ClientSize.Height * 2) - 1150, 0, 0, bulleteme1);
                        }
                        for (int i = 0; i < bulleteme1.Count; i++)
                        {
                            bulleteme1[i].x -= 50;
                            bulleteme1[i].hampos++;
                            if (bulleteme1[i].hampos > 30)
                            {
                                bulleteme1.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    for (int i = 0; i < bulleteme1.Count; i++)
                    {
                        if ((hero[0].x) <= bulleteme1[i].x + bulleteme1[i].s.Width && (hero[0].x) + hero[0].s.Width >= bulleteme1[i].x)
                        {
                            if ((hero[0].y) <= bulleteme1[i].y + bulleteme1[i].s.Height && (hero[0].y) + hero[0].s.Height >= bulleteme1[i].y)
                            {
                                powerct++;
                                health(powerct);
                                bulleteme1.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    if (felevator >= 1 && soldstock[4].y + 10 < soldstock[17].y)
                    {
                        swit[0].y += 25;
                        scrollct++;
                        soldstock[4].y += 25;
                        if (scrollct < 30)
                        {
                            screen_move(2, 0, -25);
                        }

                        felevator++;
                        // stop = 1;
                    }
                    if (stop == 0)
                    {
                        countsaw1++;
                        if (kill[0].x + kill[0].s.Width >= soldstock[5].x && kill[0].x + kill[0].s.Width <= soldstock[5].x + soldstock[0].s.Width)
                        {
                            kill[0].x += kx;
                            if (countsawmove == 0)
                            {
                                kill[0].v = new Bitmap("Saw2.png");
                            }
                            if (countsawmove == 1)
                            {
                                kill[0].v = new Bitmap("Saw3.png");
                            }
                            if (countsawmove == 2)
                            {
                                kill[0].v = new Bitmap("Saw4.png");
                            }
                            if (countsawmove == 3)
                            {
                                kill[0].v = new Bitmap("Saw1.png");
                                countsawmove = -1;
                            }
                            if (countsaw1 == 69)
                            {
                                countsaw1 = 0;
                                kx *= -1;
                            }
                            countsawmove++;
                        }
                        for (int i = 0; i < hammer.Count; i++)
                        {
                            int f = 0;
                            if (hammer[i].hampos == 1 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 2;
                                hammer[i].s.Height -= 95;
                            }
                            if (hammer[i].hampos == 2 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 3;
                                hammer[i].s.Height -= 95;
                            }
                            if (hammer[i].hampos == 3 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 4;
                                hammer[i].s.Height -= 95;
                            }
                            if (hammer[i].hampos == 4 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 5;
                                hammer[i].s.Height -= 95;
                            }
                            if (hammer[i].hampos == 5 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 6;
                                hammer[i].s.Height += 95;
                            }
                            if (hammer[i].hampos == 6 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 7;
                                hammer[i].s.Height += 95;
                            }
                            if (hammer[i].hampos == 7 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 8;
                                hammer[i].s.Height += 95;
                            }
                            if (hammer[i].hampos == 8 && f == 0)
                            {
                                f = 1;
                                hammer[i].hampos = 1;
                                hammer[i].s.Height += 95;
                            }
                        }
                    }

                    draw(this.CreateGraphics());
                }
            }
            
            if (loss == 1)
            {
                loss = 2;
                MessageBox.Show("you loss");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            draw(e.Graphics);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            of = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            m = new Bitmap("longBackground.png");
            //m = new Bitmap("1.png");
            pnn = new node();
            pnn.x = 0;
            pnn.y = 0;
            pnn.v = m;
            pnn.s.Width = this.ClientSize.Width;
            paperwidth = ((pnn.s.Width / 4) * 2);
            paperonedown = (m.Height / 2);
            pnn.s.Height = this.ClientSize.Height;
            ground.Add(pnn);
            m = new Bitmap("walk-1.png");
            pnn = new node();
            pnn.x = this.ClientSize.Width - 300;
            pnn.y = 150;
            /*            pnn.x = 1600;
                        pnn.y = 400;*/
            /*pnn.x = 10;
            pnn.y = 10;*/
            pnn.v = m;
            pnn.s.Width = m.Width + 30;
            pnn.s.Height = m.Height + 100;
            hero.Add(pnn);
            m = new Bitmap("boxx.png");
            pnn = new node();
            int box = (this.ClientSize.Height - m.Height);
            int boxsizeground = m.Width;
            putblocks(m, 0, box, 0, -18, soldstock);
            m = new Bitmap("rboxx.png");
            pnn = new node();
            putblocks(m, 0, box + m.Height, 0, -18, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, boxsizeground, (this.ClientSize.Height - m.Height + 110), -512, 0, soldstock);
            m = new Bitmap("goodwall.png");
            pnn = new node();
            //note
            putblocks(m, boxsizeground, (this.ClientSize.Height - m.Height - 118), -865, -50, soldstock);
            m = new Bitmap("elevator.png");
            pnn = new node();
            putblocks(m, boxsizeground + ((m.Width * 3) - 100), (this.ClientSize.Height - m.Height + 18), 30, -20, soldstock);
            m = new Bitmap("woodwall.png");
            pnn = new node();
            int anyor1 = (this.ClientSize.Height - 700);
            int gg = 180;
            putblocks(m, gg, anyor1, -300, 30, soldstock);
            ////////////////////////////////////////////////////////
            m = new Bitmap("Hammer1.png");
            pnn = new node();
            pnn.hampos = 1;
            putblocks(m, gg * 4, anyor1 + 100, -50, 0, hammer);
            m = new Bitmap("Hammer3.png");
            pnn = new node();
            pnn.hampos = 3;
            putblocks(m, (gg * 6) - 50, anyor1 + 100, -50, 0, hammer);
            m = new Bitmap("Hammer2.png");
            pnn = new node();
            pnn.hampos = 2;
            putblocks(m, (gg * 8), anyor1 + 100, -50, 0, hammer);
            ////////////////////////////////////////////////
            m = new Bitmap("saw1.png");
            pnn = new node();
            putblocks(m, 200, anyor1 - 40, 280, 280, kill);
            m = new Bitmap("lever1.png");
            pnn = new node();
            putblocks(m, boxsizeground + ((m.Width * 20) + 38), (this.ClientSize.Height - m.Height - 115), -92, -90, swit);
            m = new Bitmap("block.png");
            pnn = new node();
            int ledder_mid = (pnn.s.Width / 2);
            int ledder_top = (box - (m.Height * 2));
            putblocks(m, 0, ledder_top, 0, 0, soldstock);
            m = new Bitmap("stairs_full.png");
            pnn = new node();
            putblocks(m, ledder_mid + 40, ledder_top - 90, -80, -330, leader);
            m = new Bitmap("tile73.bmp");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - (m.Width + 100), 0, -100, -210, soldstock);
            m = new Bitmap("tile73.bmp");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 10, 0, -100, -210, soldstock);
            int underwall = pnn.s.Height;
            m = new Bitmap("horbox.bmp");
            pnn = new node();
            int blockstartt = this.ClientSize.Width - (m.Width);
            putblocks(m, blockstartt, underwall, 0, 0, soldstock);
            m = new Bitmap("reversehorbox.bmp");
            pnn = new node();
            putblocks(m, blockstartt + m.Width, underwall, 0, 0, soldstock);
            int sameline = pnn.s.Height - 70;
            m = new Bitmap("xbox.png");
            pnn = new node();
            int boxandxbox = (blockstartt - m.Width);
            putblocks(m, boxandxbox - 6, sameline + 315, -10, -10, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, (boxandxbox - m.Width) + 50, sameline + 215, 50, 0, soldstock);
            m = new Bitmap("row.png");
            pnn = new node();
            putblocks(m, (boxandxbox - m.Width) + 170, sameline + 300, -100, -10, dacore);
            m = new Bitmap("wall.bmp");
            pnn = new node();
            putblocks(m, this.ClientSize.Width, this.ClientSize.Height - (m.Height + 420), -20, -360, soldstock);
            m = new Bitmap("wall.bmp");
            pnn = new node();
            putblocks(m, this.ClientSize.Width, this.ClientSize.Height - 60, -20, -360, soldstock);
            m = new Bitmap("wall.bmp");
            pnn = new node();
            int doorpos = m.Width + this.ClientSize.Height + 160;
            putblocks(m, this.ClientSize.Width, this.ClientSize.Height + 160, -20, -360, soldstock);
            m = new Bitmap("Door1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width, doorpos + 450, -20, -300, soldstock);
            ////////////down
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, 0, (this.ClientSize.Height * 2) - 130, -140, 0, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, 680, (this.ClientSize.Height * 2) - 130, -140, 0, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, 850, (this.ClientSize.Height * 2) - 130, -140, 0, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, 680, (this.ClientSize.Height * 2) - 60, -290, 0, soldstock);
            m = new Bitmap("elebox.bmp");
            pnn = new node();
            putblocks(m, 485, (this.ClientSize.Height * 2) - 120, -30, 0, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, 710, (this.ClientSize.Height * 2) - 713, -250, 0, soldstock);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width, 200, 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + 500, 200, 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + 1000, 200, 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, 0, (this.ClientSize.Height / 2) - 120, 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, (this.ClientSize.Width / 2), (this.ClientSize.Height / 2) - 90, 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, (this.ClientSize.Width / 3) - 292, (this.ClientSize.Height / 2) - 90, 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, 0, (this.ClientSize.Height - 90), 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, 0, (this.ClientSize.Height + 200), 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, (this.ClientSize.Width / 2), (this.ClientSize.Height - 90), 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, (this.ClientSize.Width / 2), (this.ClientSize.Height + 200), 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width, (this.ClientSize.Height - 90), 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width, (this.ClientSize.Height + 200), 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (this.ClientSize.Width / 2), (this.ClientSize.Height - 90), 710, 300, dacore);
            m = new Bitmap("underindustry.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (this.ClientSize.Width / 2), (this.ClientSize.Height + 200), 710, 300, dacore);
            //wall.bmp
            m = new Bitmap("ww.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 385, (this.ClientSize.Height - (m.Height - 160)), 0, -390, soldstock);
            m = new Bitmap("ww.png");
            pnn = new node();
            putblocks(m, -3, (this.ClientSize.Height - (m.Height - 702)), 0, -390, soldstock);
            m = new Bitmap("monistarmove-0.png");
            pnn = new node();
            pnn.walpos = 0;
            putblocks(m, 100, (this.ClientSize.Height + (m.Height + 750)), -50, -100, mon1);
            m = new Bitmap("monistarmove-0.png");
            pnn = new node();
            pnn.walpos = 0;
            putblocks(m, 700, (this.ClientSize.Height + (m.Height + 750)), -50, -100, mon1);
            m = new Bitmap("h1.png");
            pnn = new node();
            pnn.walpos = 0;
            putblocks(m, 100, (this.ClientSize.Height + (m.Height + 720)), 0, 0, ele);
            m = new Bitmap("h1.png");
            pnn = new node();
            pnn.walpos = 0;
            putblocks(m, 700, (this.ClientSize.Height + (m.Height + 720)), 0, 0, ele);
            m = new Bitmap("lever1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 500, (this.ClientSize.Height - (m.Height - 300)), -92, -90, swit);
            m = new Bitmap("D6.png");
            pnn = new node();
            putblocks(m, 10, 10, 0, 0, power);
            m = new Bitmap("p1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 170, this.ClientSize.Height - 135, -50, -50, poweradd);
            m = new Bitmap("p1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 290, this.ClientSize.Height - 135, -50, -50, poweradd);
            m = new Bitmap("p1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width / 2 + 295, this.ClientSize.Height - 220, -50, -50, poweradd);
            m = new Bitmap("p1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 700, (this.ClientSize.Height - (m.Height - 300)), -50, -50, poweradd);
            m = new Bitmap("p1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 850, (this.ClientSize.Height - (m.Height - 300)), -50, -50, poweradd);
            m = new Bitmap("newbullet.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width - 1000, (this.ClientSize.Height - (m.Height - 300)), -50, -50, newbullet);
            m = new Bitmap("largebox.png");
            pnn = new node();
            cc = m.Width;
            putblocks(m, this.ClientSize.Width + 10, (this.ClientSize.Height * 2) - 100, 0, 0, soldstock);
            m = new Bitmap("ww.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 3) + 260, (this.ClientSize.Height * 2) - 700, 0, -390, soldstock);
            m = new Bitmap("Sawdoor.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 3) + 80, (this.ClientSize.Height * 2) - 380, 210, 210, magic);
            m = new Bitmap("ww.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 3) + 260, (this.ClientSize.Height * 2) - 1300, 0, -390, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + cc, (this.ClientSize.Height * 2) - 100, 0, 0, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 2), (this.ClientSize.Height * 2) - 100, 0, 0, soldstock);
            m = new Bitmap("largebox.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 3), (this.ClientSize.Height * 2) - 100, 0, 0, soldstock);
            //31
            m = new Bitmap("turret-1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 3) + 140, (this.ClientSize.Height * 2) - 150, -100, -30, yellowmon);
            m = new Bitmap("Transporter4.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 3) - 40, (this.ClientSize.Height * 2) - 200, -200, 60, soldstock);
            m = new Bitmap("Transporter4.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 2) + 110, (this.ClientSize.Height * 2) - 200, -200, 60, soldstock);
            m = new Bitmap("Transporter4.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc) + 240, (this.ClientSize.Height * 2) - 200, -200, 60, soldstock);
            //34
            m = new Bitmap("goodwall.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc) + 350, (this.ClientSize.Height * 2) - 700, -350, 100, soldstock);
            m = new Bitmap("goodwall.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 2) + 260, (this.ClientSize.Height * 2) - 700, -350, 100, soldstock);
            m = new Bitmap("goodwall.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc), (this.ClientSize.Height * 2) - 700, -350, 100, soldstock);
            m = new Bitmap("goodwall.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc) - 490, (this.ClientSize.Height * 2) - 700, -350, 100, soldstock);
            /////////
            m = new Bitmap("IndustrialTile_63.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc) + 670, (this.ClientSize.Height * 2) - 700, -20, 10, dacore);
            m = new Bitmap("Lightning.png");
            pnn = new node();
            pnn.hampos = 6;
            pnn.walpos = this.ClientSize.Width + (cc) + 680;
            putblocks(m, this.ClientSize.Width + (cc) + 680, (this.ClientSize.Height * 2) - 680, 15, -221, lazer);
            m = new Bitmap("IndustrialTile_63.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 2) + 360, (this.ClientSize.Height * 2) - 700, -20, 10, dacore);
            m = new Bitmap("Lightning.png");
            pnn = new node();
            pnn.hampos = 10;
            pnn.walpos = this.ClientSize.Width + (cc * 2) + 370;
            putblocks(m, this.ClientSize.Width + (cc * 2) + 370, (this.ClientSize.Height * 2) - 680, 15, -221, lazer);
            m = new Bitmap("IndustrialTile_63.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc) + 300, (this.ClientSize.Height * 2) - 700, -20, 10, dacore);
            m = new Bitmap("Lightning.png");
            pnn = new node();
            pnn.hampos = 16;
            pnn.walpos = this.ClientSize.Width + (cc) + 320;
            putblocks(m, this.ClientSize.Width + (cc) + 320, (this.ClientSize.Height * 2) - 680, 15, -221, lazer);
            m = new Bitmap("IndustrialTile_63.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + (cc * 3) - 50, (this.ClientSize.Height * 2) - 700, -20, 10, dacore);
            m = new Bitmap("Lightning.png");
            pnn = new node();
            pnn.hampos = 0;
            pnn.walpos = this.ClientSize.Width + (cc * 3) - 30;
            putblocks(m, this.ClientSize.Width + (cc * 3) - 30, (this.ClientSize.Height * 2) - 680, 15, -221, lazer);
            m = new Bitmap("g1.png");
            pnn = new node();
            pnn.hampos = 0;
            putblocks(m, this.ClientSize.Width + 50, this.ClientSize.Height, -200, -200, dragon);
            m = new Bitmap("h1.png");
            pnn = new node();
            putblocks(m, this.ClientSize.Width + 600, this.ClientSize.Height - 630, -900, -100, hlt);
            draw(this.CreateGraphics());
        }
        int cc = 0;
        public void draw(Graphics g2)
        {
            Graphics g = Graphics.FromImage(of);
            g.Clear(Color.White);

            for (int i = 0; i < ground.Count; i++)
            {
                g.DrawImage(ground[0].v, new Rectangle(ground[0].x, ground[0].y, ground[0].s.Width, ground[0].s.Height), new Rectangle(paperstart, paperoneup, paperwidth, paperonedown), GraphicsUnit.Pixel);
                //g.DrawImage(ground[0].v, new Rectangle(paperstart, paperoneup, paperwidth, paperonedown), new Rectangle(ground[0].x, ground[0].y , ground[0].s.Width , ground[0].s.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < dacore.Count; i++)
            {
                dacore[i].v.MakeTransparent(dacore[i].v.GetPixel(0, 0));
                g.DrawImage(dacore[i].v, new Rectangle(dacore[i].x, dacore[i].y, dacore[i].s.Width, dacore[i].s.Height), new Rectangle(0, 0, dacore[i].v.Width, dacore[i].v.Height), GraphicsUnit.Pixel);
            }

            for (int i = 0; i < kill.Count; i++)
            {
                kill[i].v.MakeTransparent(kill[i].v.GetPixel(0, 0));
                g.DrawImage(kill[i].v, new Rectangle(kill[i].x, kill[i].y, kill[i].s.Width, kill[i].s.Height), new Rectangle(0, 0, kill[i].v.Width, kill[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < hammer.Count; i++)
            {
                hammer[i].v.MakeTransparent(hammer[i].v.GetPixel(0, 0));
                g.DrawImage(hammer[i].v, new Rectangle(hammer[i].x, hammer[i].y, hammer[i].s.Width, hammer[i].s.Height), new Rectangle(0, 0, hammer[i].v.Width, hammer[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < soldstock.Count; i++)
            {
                if (i >= 0)
                {
                    soldstock[i].v.MakeTransparent(soldstock[i].v.GetPixel(0, 0));
                    g.DrawImage(soldstock[i].v, new Rectangle(soldstock[i].x, soldstock[i].y, soldstock[i].s.Width, soldstock[i].s.Height), new Rectangle(0, 0, soldstock[i].v.Width, soldstock[i].v.Height), GraphicsUnit.Pixel);
                }
            }
            for (int i = 0; i < leader.Count; i++)
            {
                leader[i].v.MakeTransparent(leader[i].v.GetPixel(0, 0));
                g.DrawImage(leader[i].v, new Rectangle(leader[i].x, leader[i].y, leader[i].s.Width, leader[i].s.Height), new Rectangle(0, 0, leader[i].v.Width, leader[i].v.Height), GraphicsUnit.Pixel);
            }

            for (int i = 0; i < Move.Count; i++)
            {
                Move[i].v.MakeTransparent(Move[i].v.GetPixel(0, 0));
                g.DrawImage(Move[i].v, new Rectangle(Move[i].x, Move[i].y, Move[i].s.Width, Move[i].s.Height), new Rectangle(0, 0, Move[i].v.Width, Move[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < hlt.Count; i++)
            {
                hlt[i].v.MakeTransparent(hlt[i].v.GetPixel(0, 0));
                g.DrawImage(hlt[i].v, new Rectangle(hlt[i].x, hlt[i].y, hlt[i].s.Width, hlt[i].s.Height), new Rectangle(0, 0, hlt[i].v.Width, hlt[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < mon1.Count; i++)
            {
                mon1[i].v.MakeTransparent(mon1[i].v.GetPixel(0, 0));
                g.DrawImage(mon1[i].v, new Rectangle(mon1[i].x, mon1[i].y, mon1[i].s.Width, mon1[i].s.Height), new Rectangle(0, 0, mon1[i].v.Width, mon1[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < hero.Count; i++)
            {
                hero[i].v.MakeTransparent(hero[i].v.GetPixel(0, 0));
                g.DrawImage(hero[i].v, new Rectangle(hero[i].x, hero[i].y, hero[i].s.Width, hero[i].s.Height), new Rectangle(0, 0, hero[i].v.Width, hero[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < swit.Count; i++)
            {
                swit[i].v.MakeTransparent(swit[i].v.GetPixel(0, 0));
                g.DrawImage(swit[i].v, new Rectangle(swit[i].x, swit[i].y, swit[i].s.Width, swit[i].s.Height), new Rectangle(0, 0, swit[i].v.Width, swit[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < lazer.Count; i++)
            {
                lazer[i].v.MakeTransparent(lazer[i].v.GetPixel(0, 0));
                g.DrawImage(lazer[i].v, new Rectangle(lazer[i].x, lazer[i].y, lazer[i].s.Width, lazer[i].s.Height), new Rectangle(0, 0, lazer[i].v.Width, lazer[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < shot.Count; i++)
            {
                shot[i].v.MakeTransparent(shot[i].v.GetPixel(0, 0));
                g.DrawImage(shot[i].v, new Rectangle(shot[i].x, shot[i].y, shot[i].s.Width, shot[i].s.Height), new Rectangle(0, 0, shot[i].v.Width, shot[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < power.Count; i++)
            {
                power[i].v.MakeTransparent(power[i].v.GetPixel(0, 0));
                g.DrawImage(power[i].v, new Rectangle(power[i].x, power[i].y, power[i].s.Width, power[i].s.Height), new Rectangle(0, 0, power[i].v.Width, power[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < ele.Count; i++)
            {
                ele[i].v.MakeTransparent(ele[i].v.GetPixel(0, 0));
                g.DrawImage(ele[i].v, new Rectangle(ele[i].x, ele[i].y, ele[i].s.Width, ele[i].s.Height), new Rectangle(0, 0, ele[i].v.Width, ele[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < poweradd.Count; i++)
            {
                poweradd[i].v.MakeTransparent(poweradd[i].v.GetPixel(0, 0));
                g.DrawImage(poweradd[i].v, new Rectangle(poweradd[i].x, poweradd[i].y, poweradd[i].s.Width, poweradd[i].s.Height), new Rectangle(0, 0, poweradd[i].v.Width, poweradd[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < newbullet.Count; i++)
            {
                newbullet[i].v.MakeTransparent(newbullet[i].v.GetPixel(0, 0));
                g.DrawImage(newbullet[i].v, new Rectangle(newbullet[i].x, newbullet[i].y, newbullet[i].s.Width, newbullet[i].s.Height), new Rectangle(0, 0, newbullet[i].v.Width, newbullet[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < yellowmon.Count; i++)
            {
                yellowmon[i].v.MakeTransparent(yellowmon[i].v.GetPixel(0, 0));
                g.DrawImage(yellowmon[i].v, new Rectangle(yellowmon[i].x, yellowmon[i].y, yellowmon[i].s.Width, yellowmon[i].s.Height), new Rectangle(0, 0, yellowmon[i].v.Width, yellowmon[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < dragon.Count; i++)
            {
                dragon[i].v.MakeTransparent(dragon[i].v.GetPixel(0, 0));
                g.DrawImage(dragon[i].v, new Rectangle(dragon[i].x, dragon[i].y, dragon[i].s.Width, dragon[i].s.Height), new Rectangle(0, 0, dragon[i].v.Width, dragon[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < dragonfire.Count; i++)
            {
                dragonfire[i].v.MakeTransparent(dragonfire[i].v.GetPixel(0, 0));
                g.DrawImage(dragonfire[i].v, new Rectangle(dragonfire[i].x, dragonfire[i].y, dragonfire[i].s.Width, dragonfire[i].s.Height), new Rectangle(0, 0, dragonfire[i].v.Width, dragonfire[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < magic.Count; i++)
            {
                magic[i].v.MakeTransparent(magic[i].v.GetPixel(0, 0));
                g.DrawImage(magic[i].v, new Rectangle(magic[i].x, magic[i].y, magic[i].s.Width, magic[i].s.Height), new Rectangle(0, 0, magic[i].v.Width, magic[i].v.Height), GraphicsUnit.Pixel);
            }
            for (int i = 0; i < bulleteme1.Count; i++)
            {
                bulleteme1[i].v.MakeTransparent(bulleteme1[i].v.GetPixel(0, 0));
                g.DrawImage(bulleteme1[i].v, new Rectangle(bulleteme1[i].x, bulleteme1[i].y, bulleteme1[i].s.Width, bulleteme1[i].s.Height), new Rectangle(0, 0, bulleteme1[i].v.Width, bulleteme1[i].v.Height), GraphicsUnit.Pixel);
            }
            g2.DrawImage(of, 0, 0);
        }
    }
}
