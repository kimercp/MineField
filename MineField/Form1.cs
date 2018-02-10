using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace MineField
{
    public partial class frmGame : Form
    {
        // public variables
        private int level = 1; // keeps the number of level
        private int seconds; // keeps the number value of seconds
        private int points; // keeps the number of player's points
        private int collectedPeople; // keeps the number of collected people
        private int playerLives; // keeps the number of player lives
        private int playerPositionX; // keeps the number of tile in grid, column where player stands
        private int playerPositionY; // keeps the number of tile in grid, row where player stands
        private int rocketPositionX; // keeps the number of tile in grid, column where rocket stands
        private int rocketPositionY; // keeps the number of tile in grid, row where rocket stands
        private int bulletPositionX; // keeps the number of tile in grid, column where bullet stands
        private int bulletPositionY; // keeps the number of tile in grid, row where bullet stands
        private int explosionPositionX; // keeps the number of tile in grid, column where explosion appears
        private int explosionPositionY; // keeps the number of tile in grid, row where explosion appears
        private int numberOfFrameImage = 0; // frame used to animate explosions
        private int sizeOfTile = 32; // pixels size of each tile

        private Tile[,] board = new Tile[22, 22]; // two dimension array of Tile class objects
        private Random rnd = new Random(); // instance of clas Random, will use this to generate random numbers
        private Image rocketBackgroundImage; // to keep a tile image when sprite stand
        private Image bulletBackgroundImage; // to keep an old image tile when bullet moves
        
        private Boolean isAmmo; // does tank have an ammonition (bullet)
        private Boolean isGameLoaded = false; // true if game has been loaded by player, false when user press start button
        private Boolean isTankWentOnMine = false; // this will change on true when tank goes on field with mine

        private String tankDirection = "north"; // tank's direction, use this for display a proper image related to movement's direction
        private String bulletDirection; // bullet's direction, use this for display a proper image related to movement's direction
        private Boolean isRocketDestroy = false; // set true when bullet meet with rocket on the same tile

        // some methods which will run before the form appears
        public frmGame() // constructor
        {
            InitializeComponent();
            CreateLabels(); // create grid labels
            CreateBorders(); // create borders to limit the player's movement
            WelcomeBoard(); 
            // set the openFileDialog inital directory path to save games folder
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Savegame";            
        }

        /* I have created my Tile class to keep all necessery values in each Label
           The class have constructor which sets all values as false, getters and setters methods. 
           Every tile (label):
           keeps value true if the tile has been visited
           keeps value true in this tile if there is a mine
           keeps value true in this tile if there is a people
           keeps value true if the tile is border and player is not allowed to enter this tile
           keeps value true if the tile keep an ammonition bullet
         */
        public class Tile
        {
            Boolean people;
            Boolean mine;
            Boolean border;
            Boolean visited;
            Boolean ammo;

            // constructor will create an instance with all variables sets as false 
            public Tile()
            {
                this.people = false;
                this.mine = false;
                this.border = false;
                this.visited = false;
                this.ammo = false;
            }

            public void setPeople(Boolean value)
            {
                this.people = value;
            }
            
            public void setMine(Boolean value)
            {
                this.mine = value;
            }

            public void setBorder(Boolean value)
            {
                this.border = value;
            }

            public void setVisited(Boolean value)
            {
                this.visited = value;
            }

            public void setAmmo(Boolean value)
            {
                this.ammo = value;
            }

            public Boolean getPeople()
            {
                return this.people;
            }

            public Boolean getMine()
            {
                return this.mine;
            }

            public Boolean getBorder()
            {
                return this.border;
            }

            public Boolean getVisited()
            {
                return this.visited;
            }

            public Boolean getAmmo()
            {
                return this.ammo;
            }
        }

        // this method will create 400 label objects
        void CreateLabels()
        {
            // create grid labels size 20x20
            int x = 0;
            int y = 0;

            // loop will execute 400 times
            for (int i = 1; i < 401; i++)
            {
                // will create Label object
                Label lblTile = new Label();
                // set the name of label
                lblTile.Name = "lblLabel" + i;
                // add to panel controls
                this.pnlPanel.Controls.Add(lblTile);

                // set up location for label of x and y coordinates
                lblTile.Location = new System.Drawing.Point(x * sizeOfTile, y);
                // set up size of label
                lblTile.Size = new System.Drawing.Size(sizeOfTile, sizeOfTile);
                lblTile.TabIndex = 0;
                
                x += 1; // increment variable x by 1
                if (i % 20 == 0) // if remainder is 0 add 20 to y, which will start set the labels in next row
                {
                    x = 0;
                    y += sizeOfTile; // increment variable y by sizeOfTile
                }
            }
        }

        // this function returns the Label object that is at location (X,Y)
        private Label getLabel(int x, int y)
        {
            // variable k holds the number of label which is added to string
            int k = (y - 1) * 20 + x;
            string s = "lblLabel" + k.ToString();

            // for each component controls in panel 
            foreach (Control c in pnlPanel.Controls)
            {
                // if control's type is label 
                if (c.GetType() == typeof(System.Windows.Forms.Label))
                {
                    // if the name of control is equal to string, returns Label object
                    if (c.Name == s)
                    {
                        return (Label)c;
                    }
                }
            }
            return null;
        }

        /* this method will set up beggining values of variables , initialize arrays, define bounds of board
         * The program will call this method each time and only when user press start button
         */
        void ResetVariables()
        {
            // variables initialization
            if (!isGameLoaded) // reset these values only when game is new, after load game these variables will be omitted
            {
                level = 1;
                playerLives = 3;
                points = 0;
            }
            isGameLoaded = false; // set the variable boolean as false
            // reset the others variables this method
            ResetVariablesAfterLosing();
        }

        // reset the values each time when user lose or won the actual level
        private void ResetVariablesAfterLosing()
        {            
            collectedPeople = 0;
            isAmmo = false; // reset tag for false
            seconds = 10 * level + 25; // depends from level add extra seconds, multiply level by 10 and adding 25
            isRocketDestroy = false; // reset tag for false
            isTankWentOnMine = false; // reset tag for false
            tankDirection = "north"; // reset tank direction

            // reset all moving sprites positions to default values at the beginning of the game
            playerPositionX = board.GetLength(0) / 2; // take length of array and divide by 2, which set up the player position in the middle of row
            playerPositionY = board.GetLength(1) - 2; // array has 22 rows , first and last is border
            rocketPositionY = 20;
            /* change the position of rocket from left to right and reverse, depends on the level 
             (if number of level is even the rocket starts from right corner) */
            if (level % 2 == 0) rocketPositionX = 20;            
            else rocketPositionX = 1;

            // reset store image to default value (store image is a label image before rocket step on it)
            rocketBackgroundImage = Properties.Resources.medievalTile_15;
            // set up image value as empty
            bulletBackgroundImage = null;

            // refresh labels after loosing (level, points and player lives stay the same)
            lblLevelNumber.Text = level.ToString();
            lblPlayerLivesNumber.Text = playerLives.ToString();
            lblSecondsNumber.Text = seconds.ToString();
            lblPointsNumber.Text = points.ToString();
            lblCollectedPeopleNumber.Text = collectedPeople.ToString();
            lblAmmoNumber.Image = null;
        }
        
        // this method creates border arround the board and is called only once at the beggining
        void CreateBorders()
        {
            // initialize tiles array
            for (int i = 0; i < board.GetLength(0); i++) 
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = new Tile();
                }

            // define bounds of board ( top, bottom, left and right border)
            for (int i = 0; i <= 21; i++)
            {
                board[i, 0].setBorder(true); // i rows left border
                board[i, 21].setBorder(true); // i rows right border
            }
            for (int i = 0; i <= 21; i++)
            {
                board[0, i].setBorder(true); // y columns //top border
                board[21, i].setBorder(true); // bottom border
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        // this will execute when player's click on Start button
        private void StartGame()
        {
            // reset board and variables
            ResetVariables();
            ClearBoard();
            DrawBoard();

            // enable buttons which user needed to play
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            btnUp.Enabled = true;
            btnDown.Enabled = true;
            btnLeft.Enabled = true;
            btnRight.Enabled = true;
            btnFire.Enabled = false;
            btnSaveGame.Enabled = true;
            btnLoadGame.Enabled = false;
            lblWallOfFame.Enabled = false;

            // create random mines on the board
            CreateMinefield(level);
            // create random people to collect on the board
            RandomPeopleLocation(level);
            // create random bullets position to collect on the board
            RandomBullets(level);
            // set player's position
            SetPlayerPosition(playerPositionX, playerPositionY);
            // start countdown timer
            tmrSeconds.Start();
            // start rocket moving
            tmrRocketMove.Start();
        }

        // assigns the bullets to random fields in a grid
        private void RandomBullets(int lev)
        {
            int ammonition = lev - 4;

            // the ammonition to collect will appear on fifth and higher level
            if (lev > 4)
            {
                do
                {
                    int x = rnd.Next(2, 20); // generate random number beetwen 2, 20
                    int y = rnd.Next(2, 15); // generate random number beetwen 2, 15
                    // if specific tile has not keep mine and border and people then put the ammo on this field
                    if (!board[x, y].getMine() && !board[x, y].getBorder() && !board[x, y].getPeople())
                    {
                        board[x, y].setAmmo(true); // tag the field as field with ammo to collect
                        Label lbl = getLabel(x, y); // get label object
                        lbl.Image = Properties.Resources.bullet; // change the field image for bullet image
                        ammonition--; // decrement variable ammonition by 1
                    }
                } while (ammonition > 0); // do this loop if ammonition is bigger than 0
            }            
        }

        // assigns the people to collect to random fields in a grid
        private void RandomPeopleLocation(int numberOfPeopleForCurrentLevel)
        {
            do {
                int x = rnd.Next(2,20); // cut first and last row , cause this is safe zone (road for car)
                int y = rnd.Next(2,20); // and there is no need to have located people in there to rescue
                // if specific tile has not keep mine and border and ammo then put the people tag as true on this field
                if (!board[x,y].getMine() && !board[x,y].getBorder() && !board[x,y].getAmmo()) 
                {
                    board[x, y].setPeople(true); // tag the field as field with human to collect
                    Label lbl = getLabel(x, y); // get label object
                    lbl.Image = Properties.Resources.human32; // change the field image for human image
                    numberOfPeopleForCurrentLevel--; // decrement variable by 1
                }
            } while (numberOfPeopleForCurrentLevel > 0); // do this loop if variable is bigger than 0
        }

        // clean all tiles (labels) on board
        private void ClearBoard()
        {
            // clear all array (reset mines , people, border, ammo and visited fields)
            for (int y = 1; y < board.GetLength(0) - 1; y++)
                for (int x = 1; x < board.GetLength(1) - 1; x++)
                {
                    board[x, y].setMine(false);
                    board[x, y].setPeople(false);
                    board[x, y].setVisited(false);
                    board[x, y].setBorder(false);
                    board[x, y].setAmmo(false);
                }
        }

        // this method draw the board
        private void DrawBoard()
        {
            // draw the scenario (different trees, different grass)
            for (int y = 1; y < board.GetLength(0) - 1; y++) // rows
                for (int x = 1; x < board.GetLength(1) - 1; x++) // columns
                {
                    Label lbl = getLabel(x, y);
                    lbl.Image = null;
                    lbl.BackColor = Color.LightGray; // deletes orange trail by setting tha background collor for light gray
                    if ((y == 1) || (y == 20))
                    {
                        // random tile of road (don't want to have the same pattern which makes my board visually more attractive
                        // and different on each level by random numbers with assigned graphics tile)
                        int roadTileType = rnd.Next(2); // generate random number 0 or 1
                        // choose the different image tile depends on number generate and set to roadTileType variable
                        if (roadTileType == 1) lbl.Image = Properties.Resources.medievalTile_15;
                        else lbl.Image = Properties.Resources.medievalTile_16;
                    }
                    else
                    {
                        // random tile of grass (visual effect) probability 1/3 that will be another graphic than grass
                        int grassTileType = rnd.Next(3); // generate random number
                        // choose the different image tile depends on number
                        if (grassTileType == 0) lbl.Image = Properties.Resources.medievalTile_57;
                        else if (grassTileType == 1) lbl.Image = Properties.Resources.medievalTile_58;
                        else
                        {
                            // random tile of tree
                            int treeTileType = rnd.Next(9);
                            // choose the different image tile depends on number
                            switch (treeTileType)
                            {
                                case 0:
                                    lbl.Image = Properties.Resources.medievalTile_41;
                                    break;
                                case 1:
                                    lbl.Image = Properties.Resources.medievalTile_42;
                                    break;
                                case 2:
                                    lbl.Image = Properties.Resources.medievalTile_43;
                                    break;
                                case 3:
                                    lbl.Image = Properties.Resources.medievalTile_44;
                                    break;
                                case 4:
                                    lbl.Image = Properties.Resources.medievalTile_45;
                                    break;
                                case 5:
                                    lbl.Image = Properties.Resources.medievalTile_46;
                                    break;
                                case 6:
                                    lbl.Image = Properties.Resources.medievalTile_47;
                                    break;
                                case 7:
                                    lbl.Image = Properties.Resources.medievalTile_48;
                                    break;
                                case 8:
                                    lbl.Image = Properties.Resources.medievalTile_49;
                                    break;
                            }
                        }
                    }
                }

            // add obstacles to board
            AddObstacle(level);
        }

        // this will add some obstacle on the board
        private void AddObstacle(int desiredObstacleNumber)
        {
            int houseNumber = 0;
            Label lbl; // declare instance of Label class

            // this loop create specific nubmer of obstacles depends from desiredObstacleNubmer variable passed in argument
            while (houseNumber < desiredObstacleNumber)
            {
                int temp = rnd.Next(6); // generate random number
                if (temp < 3)
                {
                    for (int i = 0; i < level * 2; i++)
                    {
                        int x = rnd.Next(2, 19); // generate random number
                        int y = rnd.Next(2, 19); // generate random number
                        // set border as true because obstacle will show on the board, which make this field non enterable for player
                        if (!board[x, y].getBorder())
                        {
                            board[x, y].setBorder(true);
                            lbl = getLabel(x, y);

                            /* modulo will make effect of random obstacles
                             * even numbers, program will display Rock32Brown
                             * if number is divide by 3 program will display Rock32Gray
                             * ex. if current level is 3, then number of obstacles is level *2 which is i=6
                             * loop for then run 6 times.
                             * So in level 3, I will expect to dipslay 3 brown rocks, 1 silver rocks 2 small house 
                             * (because loop starts from 0 and will finish when i<6 which is 5)
                             */
                            if (i % 2 == 0) lbl.Image = Properties.Resources.Rock32Brown;
                            else if (i % 3 == 0) lbl.Image = Properties.Resources.Rock32Gray;
                            else lbl.Image = Properties.Resources.SmallHouse32;
                        }
                        houseNumber++; // increment by 1
                    }
                }
                else if (temp >= 3 && temp <= 4)
                {
                    int x = rnd.Next(2, 19); // generate random number
                    int y = rnd.Next(2, 19); // generate random number
                    // check if there is no border on these tiles already
                    if ((!board[x, y].getBorder()) && (!board[x + 1, y].getBorder()) && (!board[x, y + 1].getBorder()) && (!board[x + 1, y + 1].getBorder()))
                    {
                        // set up big obstacle (4 tiles)
                        board[x, y].setBorder(true);
                        lbl = getLabel(x, y);
                        lbl.Image = Properties.Resources.TowerTopLeft;

                        board[x + 1, y].setBorder(true);
                        lbl = getLabel(x + 1, y);
                        lbl.Image = Properties.Resources.TowerTopRight;

                        board[x, y + 1].setBorder(true);
                        lbl = getLabel(x, y + 1);
                        lbl.Image = Properties.Resources.TowerBottomLeft;

                        board[x + 1, y + 1].setBorder(true);
                        lbl = getLabel(x + 1, y + 1);
                        lbl.Image = Properties.Resources.TowerBottomRight;

                        houseNumber++;
                    }
                }
                else
                {
                    int x = rnd.Next(2, 18); // 18 cause the castle is made from 3 rows of tiles and last row is road
                    int y = rnd.Next(2, 18);
                    // check if there is no border on these tiles already
                    if ((!board[x, y].getBorder()) && (!board[x + 1, y].getBorder()) && (!board[x + 2, y].getBorder()) && (!board[x, y + 1].getBorder()) && (!board[x + 1, y + 1].getBorder()) && (!board[x + 2, y + 1].getBorder()) && (!board[x, y + 2].getBorder()) && (!board[x + 1, y + 2].getBorder()) && (!board[x + 2, y + 2].getBorder()))
                    {
                        // set up very big obstacle (9 tiles build square)
                        board[x, y].setBorder(true);
                        lbl = getLabel(x, y);
                        lbl.Image = Properties.Resources.CastleTopLeft;

                        board[x + 1, y].setBorder(true);
                        lbl = getLabel(x + 1, y);
                        lbl.Image = Properties.Resources.CastleTopMiddle;

                        board[x + 2, y].setBorder(true);
                        lbl = getLabel(x + 2, y);
                        lbl.Image = Properties.Resources.CastleTopRight;

                        board[x, y + 1].setBorder(true);
                        lbl = getLabel(x, y + 1);
                        lbl.Image = Properties.Resources.CastleMiddleLeft;

                        board[x + 1, y + 1].setBorder(true);
                        lbl = getLabel(x + 1, y + 1);
                        lbl.Image = Properties.Resources.CastleMiddleMiddle;

                        board[x + 2, y + 1].setBorder(true);
                        lbl = getLabel(x + 2, y + 1);
                        lbl.Image = Properties.Resources.CastleMiddleRight;

                        board[x, y + 2].setBorder(true);
                        lbl = getLabel(x, y + 2);
                        lbl.Image = Properties.Resources.CastleBottomLeft;

                        board[x + 1, y + 2].setBorder(true);
                        lbl = getLabel(x + 1, y + 2);
                        lbl.Image = Properties.Resources.CastleBottomMiddle;

                        board[x + 2, y + 2].setBorder(true);
                        lbl = getLabel(x + 2, y + 2);
                        lbl.Image = Properties.Resources.CastleBottomRight;

                        houseNumber++;
                    }
                }
            }
        } // end of AddObstacle method

        // set and display the player's graphic on x,y position
        private void SetPlayerPosition(int playerPositionX, int playerPositionY)
        {
            if (!board[playerPositionX, playerPositionY].getMine())
            {
                Label lbl = getLabel(playerPositionX, playerPositionY); // get label object at x and y coordinates (column and row)
                // count how many mines around the player
                int minesArroundPlayer = MinesArround(playerPositionX, playerPositionY);

                // show special image with number of mines around the tank
                if ( minesArroundPlayer == 1)
                {
                    switch (tankDirection)
                    { // change the tank's image related to direction
                        case "north":
                            lbl.Image = Properties.Resources.tank32North1Mine;
                            break;
                        case "south":
                            lbl.Image = Properties.Resources.tank32South1Mine;
                            break;
                        case "east":
                            lbl.Image = Properties.Resources.tank32East1Mine;
                            break;
                        case "west":
                            lbl.Image = Properties.Resources.tank32West1Mine;
                            break;
                    }
                }
                else if (minesArroundPlayer == 2)
                {
                    switch (tankDirection)
                    { // change the tank's image related to direction
                        case "north":
                            lbl.Image = Properties.Resources.tank32North2Mine;
                            break;
                        case "south":
                            lbl.Image = Properties.Resources.tank32South2Mine;
                            break;
                        case "east":
                            lbl.Image = Properties.Resources.tank32East2Mine;
                            break;
                        case "west":
                            lbl.Image = Properties.Resources.tank32West2Mine;
                            break;
                    }
                }
                else if (minesArroundPlayer == 3)
                {
                    switch (tankDirection)
                    { // change the tank's image related to direction
                        case "north":
                            lbl.Image = Properties.Resources.tank32North3Mine;
                            break;
                        case "south":
                            lbl.Image = Properties.Resources.tank32South3Mine;
                            break;
                        case "east":
                            lbl.Image = Properties.Resources.tank32East3Mine;
                            break;
                        case "west":
                            lbl.Image = Properties.Resources.tank32West3Mine;
                            break;
                    }
                }
                else
                {
                    // no mines, tank will change the image without any number and with desired direction
                    switch (tankDirection)
                    { // change the tank's image related to direction
                        case "north":
                            lbl.Image = Properties.Resources.tank32North;
                            break;
                        case "south":
                            lbl.Image = Properties.Resources.tank32South;
                            break;
                        case "east":
                            lbl.Image = Properties.Resources.tank32East;
                            break;
                        case "west":
                            lbl.Image = Properties.Resources.tank32West;
                            break;
                    }
                }              
                
                // display number of mines in label field as text
                lblMinesArroundNumber.Text = minesArroundPlayer.ToString();
                
                // check if the tank collected the human
                if (board[playerPositionX, playerPositionY].getPeople())
                {
                    collectedPeople++; // increment collected people by 1
                    lblCollectedPeopleNumber.Text = collectedPeople.ToString(); // display number of collected people
                    // reset field to non human because tank has collected it
                    board[playerPositionX, playerPositionY].setPeople(false);
                }
                // check if the tank collected the bullet
                if (board[playerPositionX, playerPositionY].getAmmo())
                {
                    // if tank has not got ammonition then collect the bullet (if tank have the bullet already, the other bullet will be gone)
                    if (!isAmmo) {
                        isAmmo = true;
                        // enable the button to fire after collecting the bullet
                        btnFire.Enabled = true;
                        // display the bullet image in menu when bullet has been collected on board
                        lblAmmoNumber.Image = Properties.Resources.bulletNoBackground;
                    }
                        
                    // set the value false for bullet in that field, tank collect it or has already the bullet
                    board[playerPositionX, playerPositionY].setAmmo(false);
                }
            }
            else
            {
                // set new explosion coordinates
                explosionPositionX = playerPositionX;
                explosionPositionY = playerPositionY;
                // explosion when tank goes on mine
                isTankWentOnMine = true;
                // display explosion animation in x,y position
                tmrExplosionAnimation.Start();
                TryAgain();         
            }
        }

        // this method will count mines around the player
        private int MinesArround(int playerPositionX, int playerPositionY)
        {
            int x = playerPositionX;
            int y = playerPositionY;
            int minesArround = 0;
            if (board[x, y - 1].getMine()) minesArround++;
            if (board[x + 1, y].getMine()) minesArround++;
            if (board[x, y + 1].getMine()) minesArround++;
            if (board[x - 1, y].getMine()) minesArround++;           
            return minesArround;
        }

        // this will execute when player lose but have spare player's lives
        private void TryAgain()
        {
            if (playerLives > 1)
            {
                Console.Beep(); // computer's internal speaker beeps
                tmrSeconds.Stop(); // stop countdown timer
                tmrRocketMove.Stop(); // stop rocket movements
                MessageBox.Show("Try again"); // display the message window
                tmrExplosionAnimation.Stop();
                ResetVariablesAfterLosing();
                tmrSeconds.Start();
                tmrRocketMove.Start();
                ClearBoard();
                DrawBoard();
                CreateMinefield(level);
                RandomPeopleLocation(level);
                RandomBullets(level);
                Label lbl = getLabel(playerPositionX, playerPositionY);
                lbl.Image = Properties.Resources.tank32North;
                playerLives -= 1;
                lblPlayerLivesNumber.Text = playerLives.ToString();
                lblMinesArroundNumber.Text = "0";
            }
            else
            {
                GameOver();
            }
        }

        // method executes when there is no more chance to play , Player lost his all chances
        private void GameOver()
        {
            lblPlayerLivesNumber.Text = "0";
            lblPointsNumber.Text = points.ToString();
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            btnLeft.Enabled = false;
            btnRight.Enabled = false;
            btnFire.Enabled = false;
            btnSaveGame.Enabled = false;
            btnLoadGame.Enabled = true;
            lblWallOfFame.Enabled = true;
            tmrSeconds.Stop();
            tmrRocketMove.Stop();
            MessageBox.Show("This is the end! You have earned " + points.ToString() + " points.");

            // open the second form to update the result into database
            if (points > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Would You like to compare Your result with other players?", "Update the result", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (frmResults f2 = new frmResults(level, points)) // create second form
                    {
                        f2.ShowDialog(this); // this disable the first form until the second form is closed
                    }
                }
            }
        }

        // draw mines and people tiles
        private void DrawMines()
        {
            for (int y = 1; y < board.GetLength(0) - 1; y++)
                for (int x = 1; x < board.GetLength(1) - 1; x++)
                {
                    if (board[x, y].getMine())
                    {
                        Label lbl = getLabel(x, y);
                        lbl.Image = Properties.Resources.redDead32;
                    }
                    else if (board[x,y].getPeople())
                    {
                        Label lbl = getLabel(x, y);
                        lbl.Image = Properties.Resources.human32withBackground;
                    }
                }
        }

        // create mines as true for random generated tiles
        private void CreateMinefield(int level)
        {
            int i = 1;
            while( i <= (level * 5)) // number of mines increase with level
            {
                int x = rnd.Next(1, 21); // creates a number between 1 and 20,because the last column and row are border in 22 lenght array
                int y = rnd.Next(2, 21); // do not put mines on first and last row, they are safe zones
                if ((!board[x, y].getMine()) && (y != 20) && (!board[x, y].getBorder()) && (!board[x, y].getAmmo()))
                {
                    board[x, y].setMine(true);
                    i++;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            GameOver();
        }

        // when player gets to upper safe zone (road)
        private void PlayerWon()
        {
            tmrSeconds.Stop();
            tmrRocketMove.Stop();
            points += seconds + collectedPeople * 10;
            if (isRocketDestroy) points += 50; // extra points for destroing a rocket
            if (level >= 9) GameOver(); // max 9 levels
            else
            {
                level += 1;
                DialogResult dialogResult = MessageBox.Show("You have made it!!!!!! Do You want to save this level?", "Congratulations", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveGame();
                }
                ResetVariablesAfterLosing();
                tmrSeconds.Start();
                tmrRocketMove.Start();
                ClearBoard();
                DrawBoard();
                CreateMinefield(level);
                RandomPeopleLocation(level);
                RandomBullets(level);
                tankDirection = "north";
                SetPlayerPosition(playerPositionX, playerPositionY);
            }            
        }

        // handle the buttons on form (up, down , left, right, fire)
        private void btnUp_Click(object sender, EventArgs e)
        {
            tankDirection = "north";
            MovePlayer(playerPositionX, playerPositionY - 1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            tankDirection = "south";
            MovePlayer(playerPositionX, playerPositionY + 1);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            tankDirection = "west";
            MovePlayer(playerPositionX - 1, playerPositionY);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            tankDirection = "east";
            MovePlayer(playerPositionX + 1, playerPositionY);
        }

        // fire bullet if player has got one
        private void btnFire_Click(object sender, EventArgs e)
        {
            if (isAmmo && playerPositionY != 20) // if tank has ammo and is not standing on last row (safe zone)
            {
                btnFire.Enabled = false;
                // fire the bullet from specific position where tanks stands into desired direction
                FireBullet(playerPositionX, playerPositionY, tankDirection); 
            }
        }
    
        // this method will allow to player move using the keyboard
        // it will override method processCmdKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (btnStart.Enabled == false)
            {
                // check which key has been pressed by player
                switch (keyData)
                {
                    case Keys.Up:
                        tankDirection = "north";
                        MovePlayer(playerPositionX,playerPositionY - 1);
                        break;
                    case Keys.Down:
                        tankDirection = "south";
                        MovePlayer(playerPositionX, playerPositionY + 1);
                        break;
                    case Keys.Left:
                        tankDirection = "west";
                        MovePlayer(playerPositionX - 1, playerPositionY);
                        break;
                    case Keys.Right:
                        tankDirection = "east";
                        MovePlayer(playerPositionX + 1, playerPositionY);
                        break;
                    case Keys.Space:
                        if (isAmmo && playerPositionY != 20) // if tank has ammo and is not standing on last row (safe zone)
                        {
                            btnFire.Enabled = false;
                            FireBullet(playerPositionX, playerPositionY, tankDirection);
                        }
                        break;
                }
            }
            return true;
        }

        // this method will execute when player press space or clikck fire button
        private void FireBullet(int playerPositionX, int playerPositionY, string tankDirection)
        {
            // set up the bullet's position and direction
            bulletPositionX = playerPositionX;
            bulletPositionY = playerPositionY;
            bulletDirection = tankDirection;
            // start bullet animation
            tmrBulletTrajectory.Start();
            lblAmmoNumber.Image = null;
            isAmmo = false;
        }

        // set position and direction of the bullet, display it
        private void tmrBulletTrajectory_Tick(object sender, EventArgs e)
        {
            Label lbl = getLabel(bulletPositionX, bulletPositionY);
            Image temporaryImage = null;
                if (bulletBackgroundImage != null)
                {
                    lbl.Image = bulletBackgroundImage;
                }
                
                // depends from direction, different graphics of bullet display
                switch (bulletDirection)
                {
                    case "north":
                        bulletPositionY--;
                        temporaryImage = Properties.Resources.bulletNorth;
                        break;
                    case "south":
                        bulletPositionY++;
                        temporaryImage = Properties.Resources.bulletSouth;
                        break;
                    case "east":
                        bulletPositionX++;
                        temporaryImage = Properties.Resources.bulletEast;
                        break;
                    case "west":
                        bulletPositionX--;
                        temporaryImage = Properties.Resources.bulletWest;
                        break;
                }
                lbl = getLabel(bulletPositionX, bulletPositionY);
                bulletBackgroundImage = lbl.Image;
                lbl.Image = temporaryImage;
                
                // bullet collision with rocket
                if ((rocketPositionX == bulletPositionX) && (rocketPositionY == bulletPositionY))
                {
                    isRocketDestroy = true; // for destroing the rocket
                    lbl = getLabel(bulletPositionX, bulletPositionY);
                    lbl.Image = rocketBackgroundImage;
                    // set new coordinates for explosion
                    explosionPositionX = rocketPositionX;
                    explosionPositionY = rocketPositionY;
                    // because rocket has been destroyed, set coordinates as zero
                    rocketPositionX = 0;
                    rocketPositionY = 0;
                    // start explosion on explosion position x and y
                    tmrExplosionAnimation.Start();
                    tmrBulletTrajectory.Stop();
                    bulletBackgroundImage = null; // reset the image to null (this will not keep the last tile image anymore)
                    tmrRocketMove.Stop();
                    Console.Beep(); // computer's internal speaker beeps when rocket has been destroyed
                }
                // if bullet is out of grid, board then stop the bullet trajectory
                if ((bulletPositionX < 1) || (bulletPositionX > 20) || (bulletPositionY < 2) || (bulletPositionY > 19))
                {
                    tmrBulletTrajectory.Stop();
                    lbl.Image = bulletBackgroundImage;
                    bulletBackgroundImage = null;
                }
                
        }

        // this method moves sprite in desired direction only by one field
        private void MovePlayer(int newX, int newY)
        {
            if (!board[newX, newY].getBorder())
            {
                Label lbl = getLabel(playerPositionX, playerPositionY);
                // position x has been changed, this means that tank has moved horizontally
                if (newX != playerPositionX) lbl.Image = Properties.Resources.trackHorizontal;
                else lbl.Image = Properties.Resources.trackVertical;

                lbl.BackColor = Color.DarkOliveGreen;
                playerPositionX = newX;
                playerPositionY = newY;
                SetPlayerPosition(playerPositionX, playerPositionY);
            }
            if (playerPositionY == 1)
            {
                PlayerWon();
            }
        }

        // this method will run every 1000 miliseconds and it will create the count down timer (seconds)
        private void tmrSeconds_Tick(object sender, EventArgs e)
        {
            seconds -= 1;
            lblSecondsNumber.Text = seconds.ToString();
            
            if (seconds < 1)
            {
                tmrSeconds.Stop();
                tmrRocketMove.Stop();
                MessageBox.Show("Sorry, Time's up!");
                TryAgain();
            }
        }

        /* this method will move enemy sprite into player direction
         *  It will store an image in memory, draw the image sprite, when sprite left the tile, it will swap the image for old one
         *  which is kept in memory
         */
        private void MoveEnemy()
        {
            string rocketDirection = null;
            Label lbl = getLabel(rocketPositionX, rocketPositionY);
            // swap label's image for original one
            lbl.Image = rocketBackgroundImage;
            // move enemy into player direction 
            if (rocketPositionX > playerPositionX) 
            { 
                rocketPositionX -= 1;
                rocketDirection = "west";
            }
            else if (rocketPositionX < playerPositionX)
            {
                rocketPositionX += 1;
                rocketDirection = "east";
            }
            if (rocketPositionY > playerPositionY)
            {
                rocketPositionY -= 1;
                rocketDirection = "north";
            }
            else if (rocketPositionY < playerPositionY)
            {
                rocketPositionY += 1;
                rocketDirection = "south";
            } 
            // take an image from current enemy sprite position, store it, draw a sprite
            lbl = getLabel(rocketPositionX, rocketPositionY);
            rocketBackgroundImage = lbl.Image;
            // check rocekt direction and replace the label's image assigned to direction
            switch (rocketDirection)
            {
                case "north":
                    lbl.Image = Properties.Resources.rocket32North;
                    break;
                case "south":
                    lbl.Image = Properties.Resources.rocket32South;
                    break;
                case "east":
                    lbl.Image = Properties.Resources.rocket32East;
                    break;
                case "west":
                    lbl.Image = Properties.Resources.rocket32West;
                    break;
            }

            // check if enemy sprite catches player
            if ((rocketPositionX == playerPositionX) && (rocketPositionY == playerPositionY))
            {
                // set new explosion coordinates
                explosionPositionX = playerPositionX;
                explosionPositionY = playerPositionY;
                // animate explosion when rocket hits the tank
                tmrExplosionAnimation.Start();
                TryAgain();
            }
        }

        /* this method replace the image every 100 miliseconds on label when rocket hits the tank
         * This will produce the animate effect of explosion
        */
        private void tmrExplosionAnimation_Tick(object sender, EventArgs e)
        {
            Label lbl = getLabel(explosionPositionX, explosionPositionY);
            numberOfFrameImage++;
            switch (numberOfFrameImage)
            {
                case 1:
                    lbl.Image = Properties.Resources.explosion1;
                    break;
                case 2:
                    lbl.Image = Properties.Resources.explosion2;
                    break;
                case 3:
                    lbl.Image = Properties.Resources.explosion3;
                    break;
                case 4:
                    lbl.Image = Properties.Resources.explosion4;
                    break;
                case 5:
                    lbl.Image = Properties.Resources.explosion5;
                    break;
                case 6:
                    lbl.Image = Properties.Resources.explosion6;
                    break;
                case 7:
                    lbl.Image = Properties.Resources.explosion7;
                    break;
                case 8:
                    lbl.Image = Properties.Resources.explosion8;
                    break;
                case 9:
                    lbl.Image = Properties.Resources.explosion9;
                    break;
                case 10:
                    lbl.Image = Properties.Resources.explosion10;
                    break;
                case 11:
                    lbl.Image = Properties.Resources.explosion11;
                    break;
                case 12:
                    lbl.Image = Properties.Resources.explosion12;
                    break;
                case 13:
                    lbl.Image = Properties.Resources.explosion13;
                    break;
                case 14:
                    if (isTankWentOnMine) // if tank goes on fild with mine
                    {
                        lbl.Image = Properties.Resources.redDead32;
                    }
                    else if (isRocketDestroy) // if rocket has been destroyed
                    {
                        lbl.Image = rocketBackgroundImage;
                        numberOfFrameImage = 0;
                        tmrExplosionAnimation.Stop();
                    }
                    else lbl.Image = Properties.Resources.explosion14;
                    break;
            }
            if (numberOfFrameImage > 13)
            {
                // display all mines on the board
                DrawMines();
                tmrExplosionAnimation.Stop();
                numberOfFrameImage = 0;
            }
        } // end of tmrExplosionAnimation_Tick

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        // this will display first board with 'Mine Field' words
        private void WelcomeBoard()
        {
            // this array will represent the string Mine Field (it will display at the begining of game as a Title Game Screen)
            int[,] welcomeBoard = new int[21, 21] { 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, },
            { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, },
            { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            }; 

            // clear board (labels)
            for (int y = 1; y < welcomeBoard.GetLength(0); y++) // rows
                for (int x = 1; x < welcomeBoard.GetLength(1); x++) // columns
                {
                    Label lbl = getLabel(x, y);
                    if ((y == 1) || (y == 20))
                    {                         
                        lbl.Image = Properties.Resources.medievalTile_16; // road tile in 1 and last row
                    }
                    else
                    { 
                        if (welcomeBoard[x,y]==1)
                        {
                                // random tile of tree
                                int treeTileType = rnd.Next(6);
                                switch (treeTileType)
                                {
                                    case 0:
                                        lbl.Image = Properties.Resources.medievalTile_42;
                                        break;
                                    case 1:
                                        lbl.Image = Properties.Resources.medievalTile_43;
                                        break;
                                    case 2:
                                        lbl.Image = Properties.Resources.medievalTile_44;
                                        break;
                                    case 3:
                                        lbl.Image = Properties.Resources.medievalTile_46;
                                        break;
                                    case 4:
                                        lbl.Image = Properties.Resources.medievalTile_47;
                                        break;
                                    case 5:
                                        lbl.Image = Properties.Resources.medievalTile_48;
                                        break;
                                }
                        }
                        else
                        {
                            // probability 1/5 that will be another graphic than grass
                            int grassTileType = rnd.Next(6);
                            if (grassTileType == 0) lbl.Image = Properties.Resources.medievalTile_49;
                            else lbl.Image = Properties.Resources.medievalTile_57;
                        }
                    }
                }
        }// end of WelcomeBoard method

        // this method will serves save game
        private void SaveGame()
        {
            string inputSaveData = level + "," + playerLives + "," + points; // string which is going to be convert for ascii and hex
            string outputSaveData = null;
            // convert each character for ascii code            
            foreach (char c in inputSaveData)
            {
                int asciiCode = (int)c; //int asciiCode = (int)c+10;
                string hexValue = ConvertDecToHex(asciiCode);
                outputSaveData += hexValue + " ";
                // outputSaveData += String.Format("{0:X}", asciiCode); 
                /* might use this "format string" method above to convert number from dec to hex
                , but I wanted to create my method
                */
            }
            
            DateTime now = DateTime.Now; // take current date and time 
            string format = " HH.mm_d.MM.yyyy";   // Use this format.
            // create Save folder if that folder does not exist
            if (!System.IO.Directory.Exists("Savegame")) System.IO.Directory.CreateDirectory("Savegame");
            else MessageBox.Show(System.IO.Directory.Exists("Dupa").ToString());
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("Savegame\\Level"+level+"_"+now.ToString(format)+".save"))
            {
                writer.WriteLine(outputSaveData);
            }
        }

        // this method will convert any number from decimal to hexadecimal system
        private string ConvertDecToHex(int decimalNumber)
        {
            List<string> list = new List<string>();

            while (decimalNumber != 0)
            {
                int remainder = decimalNumber % 16;
                decimalNumber = (int)decimalNumber / 16;

                if (remainder > 9)
                {
                    switch (remainder)
                    {
                        case 10:
                            list.Add("A");
                            break;
                        case 11:
                            list.Add("B");
                            break;
                        case 12:
                            list.Add("C");
                            break;
                        case 13:
                            list.Add("D");
                            break;
                        case 14:
                            list.Add("E");
                            break;
                        case 15:
                            list.Add("F");
                            break;
                    }
                }
                else list.Add(remainder.ToString());
            }

            list.Reverse(); // reverse values in list collection

            string hexNumber = null;
            foreach (string s in list)
            {
                hexNumber += s;
            }
            return hexNumber;
        }

        // load main variables like live, points and level, The player has possibility to start the game on level when he finished last time
        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.StreamReader source = new System.IO.StreamReader(openFileDialog1.FileName);
                    string line = source.ReadLine(); // read text line from selected file
                    source.Close();

                    string[] hex = line.Split(' '); // split the string from file for seperate strings in array
                    int[] numbers = new int[hex.Length-1];
    
                    for (int i = 0; i < hex.Length-1; i++) // hex.length-1 because last char is white space in save file
                    {
                        numbers[i] = ConvertHexToDec(hex[i]); // the numbers conversion from hexadecimal to decimal
                    }
                
                    // change ascii code for characters and add them to one string
                    string tempString = null;
                    for (int i = 0; i < numbers.Length; i++ )
                    {
                        char tempLetter = (char)numbers[i]; // casting int number for char
                        tempString = tempString + tempLetter.ToString();
                    }
                    // cutting variables values from string
                    level = Int16.Parse(tempString.Substring(0, 1));
                    playerLives = Int16.Parse(tempString.Substring(2, 1));
                    points = Int16.Parse(tempString.Substring(4, tempString.Length - 4));
                    isGameLoaded = true; // inform the ResetVariables method that the game has been load
                    StartGame();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Sorry wrong file. Try again. ","File Error");
                Console.WriteLine(exc.Message.ToString());
            }
        }

        // this method convert any hex number ex. FF, 1A, 3C for integer number
        private int ConvertHexToDec(string p)
        {
            char[] characters = new char[p.Length];
            characters = p.ToCharArray(); // convert every letter or number from string to char array (each character seperately)
            int sum = 0;

            for (int j = 0; j < characters.Length; j++)
            {
                int number = 0;

                if (Char.IsLetter(characters[j])) // check if characters is letter
                {
                    switch (characters[j])
                    {
                        case 'A':
                            number = 10;
                            break;
                        case 'B':
                            number = 11;
                            break;
                        case 'C':
                            number = 12;
                            break;
                        case 'D':
                            number = 13;
                            break;
                        case 'E':
                            number = 14;
                            break;
                        case 'F':
                            number = 15;
                            break;
                    }
                }
                else number = (int)Char.GetNumericValue(characters[j]);
                /* sum of numbers multiply by 16 to increase power 0,1,2,3 etc. depend on position
                 * last number power to 0, before last power to 1, before before last power to 2
                 */ 
                sum += number * (int)Math.Pow(16, characters.Length - j - 1); 
            }
            return sum;
        }

        // shows form 2 with player's result from database
        private void lblWallOfFame_Click(object sender, EventArgs e)
        {
            using (frmResults f2 = new frmResults(level, points))
            {
                f2.ShowDialog(this); // this disable the first form until the second form is closed
            }
        }

        private void frmGame_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Press 'Start' button to begin the game","Welcome");
        }

        // diferent rocket speed depends on level
        private void tmrRocketMove_Tick(object sender, EventArgs e)
        {
            // add rocket from desired level and set up the rocket's speed 
            if ((level >= 3) && (level <= 4) && (seconds % 3 == 0)) MoveEnemy(); // step every 3 seconds
            else if ((level >= 5) && (level <= 6) && (seconds % 2 == 0)) MoveEnemy(); // step everey 2 seconds
            else if (level > 6) MoveEnemy(); // step every one second
        }

    } // end of public partial class Form1 : Form
} // end of namespace MineField