namespace Simple_Snake_Game






//Hello welcome to the snake game this game produced by balamir demirkan belül 
//At first i need to inform you before we start, in our code there are w a s d p c keys w forward a right s down d back p stop c continue commands.
//For a reason that I don't know why, the snake can sometimes freeze while eating the apple, if you find the solution and share it with me, I would appreciate it.
//HAVE FUNN !!!!
{
    public partial class Snakegamebydemir : Form
    {

        private Label snakeeyes;
        private int snakedistance = 2 ;
        private int snake_tail;
        private int snake_head = 20 ;
        private int food = 20;
        private Random _random;
        private Label snakefood;
        private moveloc  move;
        public Snakegamebydemir()
        {
            InitializeComponent();
            _random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Main panel
            snake_tail = 0;
            apple();
            changefoodloc();
            SnakeLoc();
            move = moveloc.right;
           Snake.Enabled = true;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

       
        }
        private void endgame()
        {
            
          

           
        }
       private Label snakepiece(int locationX , int locationY)
        {
            //Making the snake 
            snake_tail++;
            Label str = new Label()
            {
                Name = "snakegg " + snake_tail,
                BackColor = Color.Red,
                Width = snake_head,
                Height = snake_head,
                Location = new Point(locationX , locationY)
            };


            this.panel1.Controls.Add(str);
            return str;
        }
       private void SnakeLoc()
        {

            snakeeyes = snakepiece(0, 0);
            var panel1locationX = (panel1.Width / 2) - (snakeeyes.Width / 2);
            var panel1locationY = (panel1.Height / 2) - (snakeeyes.Height / 2);
            snakeeyes.Location = new Point(panel1locationY , panel1locationY);

        }
       private void apple()
        {
            //Making the food
            Label str = new Label()
            {
                Name = "food" ,
                BackColor = Color.Yellow,
                Width = food,
                Height = food,
                
            };
            snakefood = str;
            this.panel1.Controls.Add(str);
         



        }
        private void changefoodloc()
        {
            //Relocating the eaten bait and positioning the bait initially on the snake
            var locationXX = _random.Next(0, panel1.Width - food);
                var locationYY = _random.Next(0, panel1.Height - food);

            bool eror ;
            do
            {
                eror = false;
                var rect1 = new Rectangle(new Point(locationXX, locationYY), snakefood.Size);
                foreach (Control control in panel1.Controls)
                {
                    if (control is Label && control.Name.Contains("food"))
                    {
                        var rect2 = new Rectangle(control.Location, control.Size);
                        if (rect1.IntersectsWith(rect2))
                        {
                            eror = true;
                            break;
                        }
                    }
                }
            } while (eror);
         
            snakefood.Location = new Point(locationXX , locationYY);
        }

    private   enum moveloc
        {
            up,
            down,
            left,
            right

        }


        private void  Snakegamebydemir_KeyDown(object sender, KeyEventArgs e)

        //Moving the snake with the buttons and not returning to the direction it came from
        {
            var keycode = e.KeyCode;
            if (move == moveloc.left && keycode == Keys.D
              || move == moveloc.right && keycode == Keys.A
              || move == moveloc.up && keycode == Keys.S
              || move == moveloc.down && keycode == Keys.W) 
            {
                return;
            }
            switch (keycode)
            {
                case Keys.W:
                    move = moveloc.up;
                    break;
                case Keys.S:
                    move = moveloc.down;
                    break;
                case Keys.A:
                    move = moveloc.left;
                    break;
                case Keys.D:
                    move = moveloc.right;
                    break;
                case Keys.P:
                    timer2.Enabled = false;
                    Snake.Enabled = false;
                    break;
                case Keys.C:
                    timer2.Enabled = true;
                    Snake.Enabled = true;
                   
                    break;
            }

        }

        private void Snake_Tick(object sender, EventArgs e)
        {
            //The brain
            followthehead();
            snakewalk();
            thanosgame();
            foodeaten();
            min.Enabled = true;
            
            
            
            

        }
        private void snakewalk()
        {
            var locationx = snakeeyes.Location.X;
            var locationy = snakeeyes.Location.Y;
            switch (move)
            {
                case moveloc.up:
                    snakeeyes.Location = new Point(locationx, locationy - (snakeeyes.Width + snakedistance));
                    break;
                case moveloc.down:
                    snakeeyes.Location = new Point(locationx, locationy + (snakeeyes.Width + snakedistance));
                    break;
                case moveloc.left:
                    snakeeyes.Location = new Point(locationx - (snakeeyes.Width + snakedistance), locationy);
                    break;
                case moveloc.right:
                    snakeeyes.Location = new Point(locationx + (snakeeyes.Width + snakedistance), locationy);
                    break;
                default:
                    break;
            }

        }
        private void thanosgame()
        {
            //Ending the game 
            bool thanosgame = false; 
            var rect3 = new Rectangle(snakeeyes.Location, snakeeyes.Size);
            foreach ( Control control   in panel1.Controls)
            {
                if(control is Label &&  control.Name.Contains("snakegg") && control.Name != snakeeyes.Name)
                {
                    var rect4 = new Rectangle (control.Location, control.Size);
                    if (rect3.IntersectsWith(rect4))
                    {
                        thanosgame = true;
                        break;

                    }
                }
           
            }
            if (thanosgame)
            {
                Snake.Enabled = false;
                DialogResult finish = MessageBox.Show("Total Point :" + lblpoint.Text, "Game Over", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                
            }
        }
        

        private void foodeaten()
        {
            //adding point 
            var rect1 = new Rectangle(snakeeyes.Location, snakeeyes.Size);
            var rect2 = new Rectangle(snakefood.Location , snakefood.Size);
            if (rect1.IntersectsWith(rect2))
            {
                lblpoint.Text = (Convert.ToInt32(lblpoint.Text ) + 10 ).ToString();
                changefoodloc();
                snakepiece(-snake_head, -snake_tail);

            }
        }
            private void followthehead() 
        {
            if (snake_tail < 1) return;


            for (int i = snake_tail; i > 1 ; i--)
            {
                var nextone = (Label)panel1.Controls[i];
                var secondone = (Label)panel1.Controls[i-1];

                nextone.Location = secondone.Location;
            }
        }

        private void min_Tick(object sender, EventArgs e)
        {
            lbltime.Text = (Convert.ToInt32(lbltime.Text) + 1).ToString();
            //... . -. .. / ... . ...- .. -.-- --- .-. ..- -- / -. . .... .. .-.
        }
    }
}