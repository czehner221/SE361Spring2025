using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformGame
{
    public partial class frmScores : Form
    {
        private DatabaseConnection databaseConnection_;
        List<Scores> scores = new List<Scores>();
        public frmScores()
        {
            InitializeComponent();
            databaseConnection_ = new DatabaseConnection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void frmScores_Load(object sender, EventArgs e)
        {
            databaseConnection_.selectScores(scores);

            dgvScores.DataSource = scores;

            dgvScores.Columns[0].HeaderText = "Username";
            dgvScores.Columns[1].HeaderText = "Score";
            dgvScores.Columns[2].HeaderText = "Time";
            dgvScores.Columns[3].HeaderText = "Date";
        }
    }
}
