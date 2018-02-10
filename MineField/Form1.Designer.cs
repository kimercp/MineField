namespace MineField
{
    partial class frmGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGame));
            this.pnlPanel = new System.Windows.Forms.Panel();
            this.pnlGameInfo = new System.Windows.Forms.Panel();
            this.lblAmmoNumber = new System.Windows.Forms.Label();
            this.lblAmmo = new System.Windows.Forms.Label();
            this.btnFire = new System.Windows.Forms.Button();
            this.lblWallOfFame = new System.Windows.Forms.Label();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnSaveGame = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.lblCollectedPeopleNumber = new System.Windows.Forms.Label();
            this.lblCollectedPeople = new System.Windows.Forms.Label();
            this.lblPlayerLivesNumber = new System.Windows.Forms.Label();
            this.lblLevelNumber = new System.Windows.Forms.Label();
            this.lblPointsNumber = new System.Windows.Forms.Label();
            this.lblMinesArroundNumber = new System.Windows.Forms.Label();
            this.lblSecondsNumber = new System.Windows.Forms.Label();
            this.lblMinesArround = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblPlayerLives = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrSeconds = new System.Windows.Forms.Timer(this.components);
            this.tmrExplosionAnimation = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tmrBulletTrajectory = new System.Windows.Forms.Timer(this.components);
            this.tmrRocketMove = new System.Windows.Forms.Timer(this.components);
            this.pnlGameInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPanel
            // 
            this.pnlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPanel.Location = new System.Drawing.Point(25, 24);
            this.pnlPanel.Name = "pnlPanel";
            this.pnlPanel.Size = new System.Drawing.Size(640, 640);
            this.pnlPanel.TabIndex = 0;
            // 
            // pnlGameInfo
            // 
            this.pnlGameInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGameInfo.Controls.Add(this.lblAmmoNumber);
            this.pnlGameInfo.Controls.Add(this.lblAmmo);
            this.pnlGameInfo.Controls.Add(this.btnFire);
            this.pnlGameInfo.Controls.Add(this.lblWallOfFame);
            this.pnlGameInfo.Controls.Add(this.btnLeft);
            this.pnlGameInfo.Controls.Add(this.btnLoadGame);
            this.pnlGameInfo.Controls.Add(this.btnDown);
            this.pnlGameInfo.Controls.Add(this.btnSaveGame);
            this.pnlGameInfo.Controls.Add(this.btnRight);
            this.pnlGameInfo.Controls.Add(this.btnUp);
            this.pnlGameInfo.Controls.Add(this.lblCollectedPeopleNumber);
            this.pnlGameInfo.Controls.Add(this.lblCollectedPeople);
            this.pnlGameInfo.Controls.Add(this.lblPlayerLivesNumber);
            this.pnlGameInfo.Controls.Add(this.lblLevelNumber);
            this.pnlGameInfo.Controls.Add(this.lblPointsNumber);
            this.pnlGameInfo.Controls.Add(this.lblMinesArroundNumber);
            this.pnlGameInfo.Controls.Add(this.lblSecondsNumber);
            this.pnlGameInfo.Controls.Add(this.lblMinesArround);
            this.pnlGameInfo.Controls.Add(this.lblLevel);
            this.pnlGameInfo.Controls.Add(this.lblPoints);
            this.pnlGameInfo.Controls.Add(this.lblSeconds);
            this.pnlGameInfo.Controls.Add(this.lblPlayerLives);
            this.pnlGameInfo.Controls.Add(this.btnStop);
            this.pnlGameInfo.Controls.Add(this.btnStart);
            this.pnlGameInfo.Location = new System.Drawing.Point(693, 24);
            this.pnlGameInfo.Name = "pnlGameInfo";
            this.pnlGameInfo.Size = new System.Drawing.Size(225, 640);
            this.pnlGameInfo.TabIndex = 1;
            // 
            // lblAmmoNumber
            // 
            this.lblAmmoNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmmoNumber.Location = new System.Drawing.Point(143, 237);
            this.lblAmmoNumber.Name = "lblAmmoNumber";
            this.lblAmmoNumber.Size = new System.Drawing.Size(32, 32);
            this.lblAmmoNumber.TabIndex = 19;
            // 
            // lblAmmo
            // 
            this.lblAmmo.AutoSize = true;
            this.lblAmmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmmo.Location = new System.Drawing.Point(81, 241);
            this.lblAmmo.Name = "lblAmmo";
            this.lblAmmo.Size = new System.Drawing.Size(47, 16);
            this.lblAmmo.TabIndex = 18;
            this.lblAmmo.Text = "Ammo";
            // 
            // btnFire
            // 
            this.btnFire.Enabled = false;
            this.btnFire.Image = global::MineField.Properties.Resources.fireButton;
            this.btnFire.Location = new System.Drawing.Point(89, 340);
            this.btnFire.Name = "btnFire";
            this.btnFire.Size = new System.Drawing.Size(50, 50);
            this.btnFire.TabIndex = 17;
            this.btnFire.UseVisualStyleBackColor = true;
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // lblWallOfFame
            // 
            this.lblWallOfFame.Font = new System.Drawing.Font("Cambria", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWallOfFame.ForeColor = System.Drawing.Color.Purple;
            this.lblWallOfFame.Image = ((System.Drawing.Image)(resources.GetObject("lblWallOfFame.Image")));
            this.lblWallOfFame.Location = new System.Drawing.Point(21, 530);
            this.lblWallOfFame.Name = "lblWallOfFame";
            this.lblWallOfFame.Size = new System.Drawing.Size(176, 100);
            this.lblWallOfFame.TabIndex = 16;
            this.lblWallOfFame.Text = "Wall of Fame";
            this.lblWallOfFame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWallOfFame.Click += new System.EventHandler(this.lblWallOfFame_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.BackgroundImage = global::MineField.Properties.Resources.left50;
            this.btnLeft.Enabled = false;
            this.btnLeft.Location = new System.Drawing.Point(33, 340);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(50, 50);
            this.btnLeft.TabIndex = 15;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.Location = new System.Drawing.Point(120, 497);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(75, 23);
            this.btnLoadGame.TabIndex = 15;
            this.btnLoadGame.Text = "Load Game";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = global::MineField.Properties.Resources.down50;
            this.btnDown.Enabled = false;
            this.btnDown.Location = new System.Drawing.Point(89, 395);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(50, 50);
            this.btnDown.TabIndex = 14;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnSaveGame
            // 
            this.btnSaveGame.Enabled = false;
            this.btnSaveGame.Location = new System.Drawing.Point(27, 497);
            this.btnSaveGame.Name = "btnSaveGame";
            this.btnSaveGame.Size = new System.Drawing.Size(75, 23);
            this.btnSaveGame.TabIndex = 14;
            this.btnSaveGame.Text = "Save Game";
            this.btnSaveGame.UseVisualStyleBackColor = true;
            this.btnSaveGame.Click += new System.EventHandler(this.btnSaveGame_Click);
            // 
            // btnRight
            // 
            this.btnRight.BackgroundImage = global::MineField.Properties.Resources.right50;
            this.btnRight.Enabled = false;
            this.btnRight.Location = new System.Drawing.Point(145, 340);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(50, 50);
            this.btnRight.TabIndex = 13;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = global::MineField.Properties.Resources.up50;
            this.btnUp.Enabled = false;
            this.btnUp.Location = new System.Drawing.Point(89, 285);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(50, 50);
            this.btnUp.TabIndex = 12;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lblCollectedPeopleNumber
            // 
            this.lblCollectedPeopleNumber.AutoSize = true;
            this.lblCollectedPeopleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCollectedPeopleNumber.Location = new System.Drawing.Point(150, 108);
            this.lblCollectedPeopleNumber.Name = "lblCollectedPeopleNumber";
            this.lblCollectedPeopleNumber.Size = new System.Drawing.Size(19, 20);
            this.lblCollectedPeopleNumber.TabIndex = 13;
            this.lblCollectedPeopleNumber.Text = "0";
            // 
            // lblCollectedPeople
            // 
            this.lblCollectedPeople.AutoSize = true;
            this.lblCollectedPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCollectedPeople.Location = new System.Drawing.Point(24, 108);
            this.lblCollectedPeople.Name = "lblCollectedPeople";
            this.lblCollectedPeople.Size = new System.Drawing.Size(112, 16);
            this.lblCollectedPeople.TabIndex = 12;
            this.lblCollectedPeople.Text = "Collected People";
            // 
            // lblPlayerLivesNumber
            // 
            this.lblPlayerLivesNumber.AutoSize = true;
            this.lblPlayerLivesNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerLivesNumber.Location = new System.Drawing.Point(150, 208);
            this.lblPlayerLivesNumber.Name = "lblPlayerLivesNumber";
            this.lblPlayerLivesNumber.Size = new System.Drawing.Size(19, 20);
            this.lblPlayerLivesNumber.TabIndex = 11;
            this.lblPlayerLivesNumber.Text = "3";
            // 
            // lblLevelNumber
            // 
            this.lblLevelNumber.AutoSize = true;
            this.lblLevelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelNumber.Location = new System.Drawing.Point(150, 176);
            this.lblLevelNumber.Name = "lblLevelNumber";
            this.lblLevelNumber.Size = new System.Drawing.Size(19, 20);
            this.lblLevelNumber.TabIndex = 10;
            this.lblLevelNumber.Text = "1";
            // 
            // lblPointsNumber
            // 
            this.lblPointsNumber.AutoSize = true;
            this.lblPointsNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPointsNumber.Location = new System.Drawing.Point(150, 143);
            this.lblPointsNumber.Name = "lblPointsNumber";
            this.lblPointsNumber.Size = new System.Drawing.Size(19, 20);
            this.lblPointsNumber.TabIndex = 9;
            this.lblPointsNumber.Text = "0";
            // 
            // lblMinesArroundNumber
            // 
            this.lblMinesArroundNumber.AutoSize = true;
            this.lblMinesArroundNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinesArroundNumber.Location = new System.Drawing.Point(148, 19);
            this.lblMinesArroundNumber.Name = "lblMinesArroundNumber";
            this.lblMinesArroundNumber.Size = new System.Drawing.Size(32, 33);
            this.lblMinesArroundNumber.TabIndex = 8;
            this.lblMinesArroundNumber.Text = "0";
            // 
            // lblSecondsNumber
            // 
            this.lblSecondsNumber.AutoSize = true;
            this.lblSecondsNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecondsNumber.Location = new System.Drawing.Point(134, 58);
            this.lblSecondsNumber.Name = "lblSecondsNumber";
            this.lblSecondsNumber.Size = new System.Drawing.Size(49, 33);
            this.lblSecondsNumber.TabIndex = 7;
            this.lblSecondsNumber.Text = "30";
            // 
            // lblMinesArround
            // 
            this.lblMinesArround.AutoSize = true;
            this.lblMinesArround.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinesArround.Location = new System.Drawing.Point(36, 33);
            this.lblMinesArround.Name = "lblMinesArround";
            this.lblMinesArround.Size = new System.Drawing.Size(94, 16);
            this.lblMinesArround.TabIndex = 6;
            this.lblMinesArround.Text = "Mines Arround";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.Location = new System.Drawing.Point(89, 176);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(41, 16);
            this.lblLevel.TabIndex = 5;
            this.lblLevel.Text = "Level";
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.Location = new System.Drawing.Point(85, 143);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(45, 16);
            this.lblPoints.TabIndex = 4;
            this.lblPoints.Text = "Points";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeconds.Location = new System.Drawing.Point(68, 72);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(62, 16);
            this.lblSeconds.TabIndex = 3;
            this.lblSeconds.Text = "Seconds";
            // 
            // lblPlayerLives
            // 
            this.lblPlayerLives.AutoSize = true;
            this.lblPlayerLives.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerLives.Location = new System.Drawing.Point(48, 208);
            this.lblPlayerLives.Name = "lblPlayerLives";
            this.lblPlayerLives.Size = new System.Drawing.Size(82, 16);
            this.lblPlayerLives.TabIndex = 2;
            this.lblPlayerLives.Text = "Player Lives";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(120, 462);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(27, 462);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrSeconds
            // 
            this.tmrSeconds.Interval = 1000;
            this.tmrSeconds.Tick += new System.EventHandler(this.tmrSeconds_Tick);
            // 
            // tmrExplosionAnimation
            // 
            this.tmrExplosionAnimation.Tick += new System.EventHandler(this.tmrExplosionAnimation_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tmrBulletTrajectory
            // 
            this.tmrBulletTrajectory.Interval = 50;
            this.tmrBulletTrajectory.Tick += new System.EventHandler(this.tmrBulletTrajectory_Tick);
            // 
            // tmrRocketMove
            // 
            this.tmrRocketMove.Interval = 1000;
            this.tmrRocketMove.Tick += new System.EventHandler(this.tmrRocketMove_Tick);
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 685);
            this.Controls.Add(this.pnlGameInfo);
            this.Controls.Add(this.pnlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MineField";
            this.Shown += new System.EventHandler(this.frmGame_Shown);
            this.pnlGameInfo.ResumeLayout(false);
            this.pnlGameInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Panel pnlPanel;
        private System.Windows.Forms.Panel pnlGameInfo;
        private System.Windows.Forms.Label lblPlayerLives;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblMinesArround;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label lblPlayerLivesNumber;
        private System.Windows.Forms.Label lblLevelNumber;
        private System.Windows.Forms.Label lblPointsNumber;
        private System.Windows.Forms.Label lblMinesArroundNumber;
        private System.Windows.Forms.Label lblSecondsNumber;
        private System.Windows.Forms.Timer tmrSeconds;
        private System.Windows.Forms.Label lblCollectedPeopleNumber;
        private System.Windows.Forms.Label lblCollectedPeople;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Button btnSaveGame;
        private System.Windows.Forms.Timer tmrExplosionAnimation;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblWallOfFame;
        private System.Windows.Forms.Button btnFire;
        private System.Windows.Forms.Label lblAmmoNumber;
        private System.Windows.Forms.Label lblAmmo;
        private System.Windows.Forms.Timer tmrBulletTrajectory;
        private System.Windows.Forms.Timer tmrRocketMove;
    }
}
