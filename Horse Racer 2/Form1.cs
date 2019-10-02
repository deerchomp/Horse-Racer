using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace horserace2
{
    public partial class Form1 : Form
    {
        public int numberOfActiveHorses = 8;
        List<Horse> horses = new List<Horse>();
        List<Bitmap> horseIcons = new List<Bitmap>();

        public class Horse
        {
            public Image icon;
            private int speed;
            private int acceleration;
            private int heart;
            private int currentPos;
            private int distanceTraveled;
            private int horseNumber;
            private string name;

            Random seed = new Random();
       
#region PROPERTIES

            public int HorseNumber
            {
                get { return horseNumber; }
                set { horseNumber = value; }
            }

            public int Speed
            {
                get { return speed; }
                set { speed = value; }
            }

            public int Acceleration
            {
                get { return acceleration; }
                set { acceleration = value; }
            }

            public int Heart
            {
                get { return heart; }
                set { heart = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public Horse()
            {
                speed = seed.Next(50, 100);
                acceleration = seed.Next(50, 100);
                heart = seed.Next(50, 100);
            }

            public int DistanceTraveled
            {
                get { return distanceTraveled; }
                set { distanceTraveled = value; }
            }

            public int CurrentPos
            {
                get { return currentPos; }
                set { currentPos = value; }
            }
            
#endregion

            public Horse(int spawnNumber)
            {
                HorseNumber = spawnNumber;
                Speed = seed.Next(10, 70);
                Acceleration = seed.Next(50, 100);
                Heart = seed.Next(5,15);
                CurrentPos = 0;
                DistanceTraveled = 0;
                Name = GenerateHorseNames();
            }

            public string GenerateHorseNames()
            {
                //currently bugged: generates only one name

                //some ideas to fix this method:
                //use a list or dynamic array
                //shrink the list as names are taken by seed

                Random nameSeed = new Random();
                string[] nameLibrary = { "Abe", "Barney", "Carl", "Diane", "Eleanor", "Fred",
                    "Gerald", "Harrison", "Irene", "Jerome", "Kristin" };
                System.Threading.Thread.Sleep(15); //give system more time to generate seed.
                return nameLibrary[nameSeed.Next(0, nameLibrary.Length)];
            }

        }
        public Form1()
        {
            InitializeComponent();
            GenerateHorses();
        }

        public void PopulateHorseIcons()
        {
            horseIcons.Add(new Bitmap("horse.png"));
            horseIcons.Add(new Bitmap("horse2.png"));
            horseIcons.Add(new Bitmap("horse3.png"));
            horseIcons.Add(new Bitmap("horse4.png"));
            horseIcons.Add(new Bitmap("horse5.png"));
            horseIcons.Add(new Bitmap("horse6.png"));
            horseIcons.Add(new Bitmap("horse7.png"));
            horseIcons.Add(new Bitmap("horse8.png"));
        }

        public void GenerateHorses()
        {
            PopulateHorseIcons();
            for (int i = 0; i < numberOfActiveHorses; i++)
            {
                horses.Add(new Horse(i));
                horses[i].icon = horseIcons[i];
            }
            horseBox_1.Text = horses[0].Name;
            horseBox_2.Text = horses[1].Name;
            horseBox_3.Text = horses[2].Name;
            horseBox_4.Text = horses[3].Name;
            horseBox_5.Text = horses[4].Name;
            horseBox_6.Text = horses[5].Name;
            horseBox_7.Text = horses[6].Name;
            horseBox_8.Text = horses[7].Name;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            foreach (var horse in horses)
            {
                horse.Speed = 20; 
            }
        }

        /*
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] filenames = { "horse.png", "horse2.png", "horse3.png", "horse4.png", "horse5.png", "horse6.png", "horse6.png", "horse7.png", "horse8.png" };
            var images = imageList1.Images;
            foreach (string file in filenames)
            {
                images.Add(Image.FromFile(file));
            }
        }
        */

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int y = 25;

            for (int i = 0; i < horses.Count; i++)
            {
                e.Graphics.DrawImage(horseIcons[i], horses[i].Speed, y);
                y += 60; //maintain a safe distance between vertical horse draw locations.
                UpdateScoreboard();
                if (horses[i].Speed == 1400)
                {
                    MessageBox.Show(horses[i].Name + " has won the race!");
                    break;
                }
            }

            e.Graphics.DrawImage(new Bitmap("finishstrip.png"),1500, 20, 50, 500);
        }

        private void startButton_Click_1(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random seed = new Random();
            foreach (var horse in horses)
            {
                int velocity = seed.Next(0,2);
                horse.Speed += horse.Heart + velocity;
            }
            Invalidate();
        }

        private void UpdateScoreboard(int velocity = 0)
        {
            horse1Distance.Text = horses[0].Speed.ToString();
            horse2Distance.Text = horses[1].Speed.ToString();
            horse3Distance.Text = horses[2].Speed.ToString();
            horse4Distance.Text = horses[3].Speed.ToString();
            horse5Distance.Text = horses[4].Speed.ToString();
            horse6Distance.Text = horses[5].Speed.ToString();
            horse7Distance.Text = horses[6].Speed.ToString();
            horse8Distance.Text = horses[7].Speed.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
