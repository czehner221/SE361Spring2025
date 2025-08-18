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

namespace PlatformGame
{
    public partial class frmGame : Form
    {
        // Variables
        private int time_;
        private int verticalSpeed_, verticalSpeed2_;
        private SoundPlayer gameTheme_;
        private DatabaseConnection databaseConnection_;

        // New Character types
        private Player player_;
        private Enemy enemy1_;
        private Enemy enemy2_;

        public frmGame(string username)
        {
            InitializeComponent();

            databaseConnection_ = new DatabaseConnection();

            verticalSpeed_ = 4;
            verticalSpeed2_ = 3;

            time_ = 0;

            // Initialize new characters
            player_ = new Player(username, lblPlayer.Top, lblPlayer.Bottom, lblPlayer.Left, lblPlayer.Right, 6, lblPlayer.Image);
            enemy1_ = new Enemy("Enemy1", lblEnemy1.Top, lblEnemy1.Bottom, lblEnemy1.Left, lblEnemy1.Right, 3, lblEnemy1.Image);
            enemy2_ = new Enemy("Enemy2", lblEnemy2.Top, lblEnemy2.Bottom, lblEnemy2.Left, lblEnemy2.Right, 4, lblEnemy2.Image);

            player_.left_ = lblPlayer.Left;
            player_.top_ = lblPlayer.Top;

            lblEnemy1.BringToFront();
            lblEnemy2.BringToFront();

            gameTheme_ = new SoundPlayer(PlatformGame.Properties.Resources.theme);
            gameTheme_.PlayLooping();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();

            gameTheme_.Stop();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Character moves left
            if(e.KeyCode == Keys.Left)
            {
                player_.goLeft_ = true;
                
                // Flips character
                if(player_.lastKeyRight_ == true)
                {
                    lblPlayer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    lblPlayer.Refresh();
                }

                player_.lastKeyRight_ = false;
                player_.lastKeyLeft_ = true;

            }

            // Character moves right
            if(e.KeyCode == Keys.Right)
            {
                player_.goRight_ = true;

                // Flips character
                if (player_.lastKeyLeft_ == true)
                {
                    lblPlayer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    lblPlayer.Refresh();
                }

                player_.lastKeyRight_ = true;
                player_.lastKeyLeft_ = false;
            }

            // Jump button
            if(e.KeyCode == Keys.Space && player_.jumping_ == false)
            {
                player_.jumping_ = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // Stop moving
            if (e.KeyCode == Keys.Left)
            {
                player_.goLeft_ = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                player_.goRight_ = false;
            }
            
            if(player_.jumping_)
            {
                player_.jumping_ = false;
            }

        }

        // Timer runs game
        private void tmrGame_Tick(object sender, EventArgs e)
        {
            // Update time per tick
            lblTime.Text = "Time: " + (time_ / 40);
            time_ += 1;

            lblScore.Text = "Score: " + player_.score_;

            if(!player_.grounded_ || player_.jumping_)
            {
                lblPlayer.Top += player_.jumpSpeed_;
            }
            
            // Code for character actually moving left
            if(player_.goLeft_)
            {
                lblPlayer.Left -= player_.speed_;

                
                if(lblPlayer.Left < 0)
                {
                    lblPlayer.Left = player_.left_;
                    lblPlayer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    lblPlayer.Refresh();
                    player_.lastKeyLeft_ = false;
                    player_.goLeft_ = false;
                }
            }    

            // Code for character actually moving right
            if(player_.goRight_)
            {
                lblPlayer.Left += player_.speed_;

                // Resets player if they hit side of screen
                if(lblPlayer.Left + lblPlayer.Width > ClientSize.Width)
                {
                    lblPlayer.Left = ClientSize.Width - lblPlayer.Width;
                    lblPlayer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    lblPlayer.Image = PlatformGame.Properties.Resources.player;
                    lblPlayer.Refresh();
                    player_.lastKeyRight_ = false;
                    player_.goRight_ = false;
                }
            }

            // Falling
            if(player_.jumping_ && player_.gravity_ < 0)
            {
                player_.jumping_ = false;
            }

            // Jumping
            if(player_.jumping_)
            {
                player_.jumpSpeed_ = -20;
                player_.gravity_ -= 1;
            }
            else
            {
                player_.jumpSpeed_ = 14;
            }

            player_.grounded_ = false;

            // Interactions for each object
            foreach(Control control in this.Controls)
            {
                // Player interaction with enemy
                if (control.Tag == "enemy")
                {
                    if(lblPlayer.Bounds.IntersectsWith(control.Bounds))
                    {
                        // Call Death method
                        death();

                        // Enemy death messagebox
                        if(MessageBox.Show("Gupta Got Ya! Score: " + player_.score_ + "\n\nPlay Again?", "Play Again?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            restartGame();
                        }
                        else
                        {
                            displayScores();
                        }
                    }
                }

                // Player interaction with coin
                if(control.Tag == "coin")
                {
                    // Player gets coin
                    if(lblPlayer.Bounds.IntersectsWith(control.Bounds) && control.Visible)
                    {
                        control.Visible = false;
                        player_.score_ += 1;
                    }
                }

                // Player interaction with platforms
                if(control.Tag == "platform" || control.Tag == "movingPlatform")
                {
                    // Player interacting with platforms. Allows player to stand
                    if(lblPlayer.Bounds.IntersectsWith(control.Bounds))
                    {
                        player_.grounded_ = true;
                        player_.gravity_ = 8;
                        lblPlayer.Top = control.Top - lblPlayer.Height + 1;
                    }

                    // Allows platforms to move
                    if (control.Tag == "movingPlatform")
                    {
                        if(control.Name == "lblPlatform4")
                        {
                            control.Top += verticalSpeed2_;
                            if(control.Top < 950 || control.Top > (1161))
                            {
                                verticalSpeed2_ = -verticalSpeed2_;
                            }
                        }
                        else
                        {
                            control.Top += verticalSpeed_;
                            if (control.Top < 350 || control.Top > (600))
                            {
                                verticalSpeed_ = -verticalSpeed_;
                            }
                        }

                    }
                }

                // Player interacts with door.
                if(control.Tag == "door")
                {
                    // Player wins if they interact with the door.
                    if(lblPlayer.Bounds.IntersectsWith(control.Bounds))
                    {
                        stopGame();
                        SoundPlayer winTheme = new SoundPlayer(PlatformGame.Properties.Resources.smb_gameover);
                        winTheme.Play();

                        databaseConnection_.insertQuery(player_.name_, player_.score_, time_, DateTime.Now);

                        if (MessageBox.Show("You won! Score: " + player_.score_ + "\n\nPlay Again?", "Play Again?", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            restartGame();
                        }
                        else
                        {
                            displayScores();
                        }
                    }
                }

            }

            // Player falls off the screen
            if((lblPlayer.Top + lblPlayer.Height) > (this.ClientSize.Height + 50))
            {
                // Call death method
                death();

                // Display message box with score and option to play again
                if (MessageBox.Show("You died! Score: " + player_.score_ + "\n\nPlay Again?", "Play Again?",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    restartGame();
                }
                else
                {
                    // Display Scores
                    displayScores();
                }

            }

            // Enemy movement code
            lblEnemy1.Left -= enemy1_.speed_;

            if(lblEnemy1.Left < lblPlatform8.Left || lblEnemy1.Left + lblEnemy1.Width > lblPlatform8.Width + lblPlatform8.Left)
            {
                enemy1_.speed_ = -enemy1_.speed_;
            }

            lblEnemy2.Left -= enemy2_.speed_;

            if (lblEnemy2.Left < lblPlatform8.Left || lblEnemy2.Left + lblEnemy2.Width > lblPlatform8.Width + lblPlatform5.Left)
            {
                enemy2_.speed_ = -enemy2_.speed_;
            }
        }

        private void restartGame()
        {
            // Reset all variables
            time_ = 0;
            player_.speed_ = 6;
            enemy1_.speed_ = 3;
            enemy2_.speed_ = 4;
            player_.gravity_ = 0;
            player_.jumpSpeed_ = 0;
            verticalSpeed_ = 4;
            verticalSpeed2_ = 3;
            player_.score_ = 0;
            player_.goLeft_ = false;
            player_.goRight_ = false;
            player_.jumping_ = false;
            player_.lastKeyRight_ = true;
            player_.lastKeyLeft_ = false;
            player_.grounded_ = true;

            if(player_.lastKeyLeft_ == true)
            {
                lblPlayer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                lblPlayer.Refresh();
            }

            player_.lastKeyRight_ = true;
            player_.lastKeyLeft_ = false;

            
            foreach(Control control in this.Controls)
            {
                if(control is PictureBox && control.Visible == false)
                {
                    control.Visible = true;
                }
            }

            // Resets character orientation
            lblPlayer.Image = PlatformGame.Properties.Resources.player;
            lblPlayer.Refresh();

            lblPlayer.Top = player_.top_;
            lblPlayer.Left = player_.left_;

            tmrGame.Start();

            gameTheme_.PlayLooping();
        }

        private void displayScores()
        {
            // Displays score form
            this.Hide();
            frmScores scoresForm = new frmScores();
            scoresForm.Show();
        }

        private void stopGame()
        {
            tmrGame.Stop();
            gameTheme_.Stop();
        }

        private void death()
        {
            // Everything that happens at player death
            stopGame();
            SoundPlayer dieTheme = new SoundPlayer(PlatformGame.Properties.Resources.smb_mariodie);
            dieTheme.Play();
            databaseConnection_.insertQuery(player_.name_, player_.score_, time_, DateTime.Now);
        }

        
    }
}
