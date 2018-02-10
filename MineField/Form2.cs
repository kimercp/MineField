using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// MySql Driver 6.9.9.0 is necessary to work with MySql Database (added as an extension into work solution) 
using MySql.Data.MySqlClient; 

namespace MineField
{
    public partial class frmResults : Form
    {
        int playerLevel; // variable keeps player level number
        int playerPoints; // variable keeps the player's points

        /* connection to mysql database (I had to instal MySQLDriver connector and add this to reference)
         * Create the instance of MySqlConnection class */
        MySqlConnection conn; 

        // this string holds server name, database, user and password values
        string myConnectionString = "server=sql8.freemysqlhosting.net;database=sql8141017;uid=sql8141017;pwd=uF3rYuJ1YG;";

        // constructor takes two parameters, passed by form1
        public frmResults(int levelFromForm1, int pointsFromForm1)
        // this values are passed to instance constructor by calling new object of frmResults class
        {
            this.playerLevel = levelFromForm1; // set passed value to playerLevel variable
            this.playerPoints = pointsFromForm1; // set passed value to playerPoints variable
            InitializeComponent(); // initilaize components descrived in Form2.Designer.cs
            if (playerPoints == 0) txtPlayersName.Enabled = false; // disable Player's name text field if points equal 0
        }

        // add some values to label's text
        private void Form2_Load(object sender, EventArgs e) // this mehtod will execute when form2 has been load
        {
            lblPlayerPoints.Text += playerPoints.ToString(); // convert number of points to text and add to label's text
            lblPlayerLevel.Text += playerLevel.ToString(); // convert level number to text and add to label's text
            btnShowOnly.Text += playerLevel.ToString() + " level"; // convert level number to text and add in the end of button text to display on button
            DisplayPlayersScores();
        }

        // display player's result from database on actual level
        private void DisplayPlayersScores()
        { // create connection with database used the credentials written in connection string
            using (conn = new MySqlConnection(myConnectionString)) // "using" close and dispose the object to allow the resources to be used by other processes
            {
                try // try..catch will allow to catch unexpected errors and will allow for programmer to handle them safely
                {
                    // sql query send to database is written as string
                    string sql = "SELECT nick,level,points FROM  `minefieldbestscore` WHERE level = @level ORDER BY points DESC LIMIT 0 , 15";
                    // add one parameter to sql query to display only players scores on specific level

                    // execute sql query with one parameter
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@level", playerLevel);

                    // create new DataAdapter to read data using sql query received back from database
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        // create new instance of DataTable class
                        DataTable table = new DataTable();
                        // fill table with data from data adapter
                        dataAdapter.Fill(table);
                        // display data onto the gridview
                        dataGridView1.DataSource = table;
                    }                    
                }
                // catch an error and holds all information in ex object
                catch (MySqlException ex)
                {
                    // display a message window to user
                    MessageBox.Show("Can not open connection! Check the Internet connection.");
                    // This is only for my information during the work, User will not see that.
                    // write white space in console
                    Console.WriteLine(" ");
                    // display the content of error message as text in console
                    Console.WriteLine(ex.ToString());
                    // display an error code
                    Console.WriteLine("Error Code: " + ex.Number);
                }
            }
            
        }

        private void frmResults_FormClosed(object sender, FormClosedEventArgs e)
        {
            // close this form
            this.Close();
        }

        // this method update player's result into the database 
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // if text field name is empty show message to user
            if (txtPlayersName.TextLength == 0) 
            {
                MessageBox.Show("You can't update the result if Your points equal 0 or You didn't type Your name.","Sorry");
            }
            else
            // function returns true if name contains only letters
            if (validatePlayerName(txtPlayersName.Text))
            {
                // this value set true if name is already registerd in database (the same names are not allowed in database)
                Boolean isNickExist = false;

                // "using" close and dispose the object to allow the resources to be used by other processes
                using (conn = new MySqlConnection(myConnectionString)) // set new connection with mysql database using arguments from myConnectionString
                {
                    // try..catch will allow to catch unexpected errors and will allow for programmer to handle them safely
                    try 
                    {
                        // open connection with database used the credentials written in connection string
                        conn.Open();
                        string sqlNick = "SELECT nick, level FROM  `minefieldbestscore` WHERE nick = @nick AND level = @level";
                        // create new instance cmdNick using sqlNick query and conn object as parameters
                        MySqlCommand cmdNick = new MySqlCommand(sqlNick, conn);
                        // adding parameters to sql query
                        cmdNick.Parameters.AddWithValue("@nick", txtPlayersName.Text);
                        cmdNick.Parameters.AddWithValue("@level", playerLevel);
                        // execute sql query
                        MySqlDataReader rdr = cmdNick.ExecuteReader();
                        // loop read every row from sql query result 
                        while (rdr.Read()) isNickExist = true; // set this value for true if found any rows with nick and level
                    }
                    // catch an error and holds all information in ex object
                    catch (Exception ex)
                    {
                        // display the message window to user informing him about error
                        MessageBox.Show("Can not open connection! Check the Internet connection.");
                        Console.WriteLine(" ");
                        // display the content of error message as text in console
                        Console.WriteLine(ex.ToString());
                        // This is only for my information during the work, User will not see that.
                        Console.WriteLine("Error Code: ");
                    }
                }

                // "using" close and dispose the object to allow the resources to be used by other processes
                using (conn = new MySqlConnection(myConnectionString))
                {
                    // execute this only if user is not exist in the database (not typed in before)
                    if (!isNickExist)
                    {
                        // open connection with database used the credentials written in connection string
                        conn.Open();
                        string sql = "INSERT INTO minefieldbestscore (nick,level,points) VALUES (@nick,@level,@points)";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        // adding parameters to sql query
                        cmd.Parameters.AddWithValue("@nick", txtPlayersName.Text);
                        cmd.Parameters.AddWithValue("@level", playerLevel);
                        cmd.Parameters.AddWithValue("@points", playerPoints);
                        // execute the query
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Player's score added into the database sucessfully.");
                    }
                    else
                    {
                        MessageBox.Show("This nick name has been already typed on this level, please choose another nick name.");
                    }
                }

                // display the player's score after update new result into database
                DisplayPlayersScores();
            }
            else
            {
                MessageBox.Show("Only letters allowed in name. Please correct Your name.", "No numbers, white space or special signs.");
                // set focus back on text field txtPlayersName
                txtPlayersName.Focus();
            }

        }

        // this function will check if every sign is letter and returns true if sucessfull
        private bool validatePlayerName(string playersNameStringToCheck)
        {
            // loop check every sign in given string (any length given in string)
            foreach (char c in playersNameStringToCheck)
            {
                // check if sign is not letter
                if (!Char.IsLetter(c)) return false;
            }
            return true;
        }

        // display result for all levels
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            // create connection with database used the credentials written in connection string
            using (conn = new MySqlConnection(myConnectionString)) // "using" close and dispose the object to allow the resources to be used by other processes
            {
                // try..catch will allow to catch unexpected errors and will allow for programmer to handle them safely
                try
                {
                    string sql = "SELECT nick,level,points FROM  `minefieldbestscore` ORDER BY points DESC";
                    
                    // add one parameter to sql query to display only players scores on specific level
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // create new DataAdapter
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        // use DataAdapter to fill DataTable
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table);
                        // display data onto the gridview
                        dataGridView1.DataSource = table;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Can not open connection! Check the Internet connection.");
                    Console.WriteLine(" ");
                    // display the content of error message as text in console
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Error Code: " + ex.Number);
                }
            }
        }

        // display result only for specific level
        private void btnShowOnly_Click(object sender, EventArgs e)
        {
            DisplayPlayersScores();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // close the form
            this.Close();
        }
    }
}