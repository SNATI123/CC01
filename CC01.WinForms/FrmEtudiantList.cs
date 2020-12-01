using CC01.BLL;
using CC01.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FrmEtudiantList : Form
    {
        private EtudiantBLO etudiantBLO;
        public FrmEtudiantList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            etudiantBLO = new EtudiantBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }
        private void loadData()
        {
            string value = txtSearch.Text.ToLower();
            var etudiants = etudiantBLO.GetBy
            (
                x =>
                x.ID.ToLower().Contains(value) ||
                x.Nom.ToLower().Contains(value) ||
                x.Prenom.ToLower().Contains(value) ||
                x.JourNaissance.ToLower().Contains(value) ||
                x.LieuNaissance.ToLower().Contains(value) ||
                x.Contact.ToString().ToLower().Contains(value) ||
                x.Email.ToLower().Contains(value)
            ).OrderBy(x => x.ID).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = etudiants;
            dataGridView1.ClearSelection();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
           
            Form f = new FrmEtudiantEdit(loadData);
            f.Show();
        }

        private void FrmModification_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                loadData();
            else
                txtSearch.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new FrmEtudiantEdit
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Etudiant,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(sender, e);
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this students(s)?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        etudiantBLO.DeleteEtudiant(dataGridView1.SelectedRows[i].DataBoundItem as Etudiant);
                    }
                    loadData();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<EtudiantListPrint> items = new List<EtudiantListPrint>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Etudiant E = dataGridView1.Rows[i].DataBoundItem as Etudiant;
                byte[] Logo = null;
                items.Add
                (
                   new EtudiantListPrint
                   (
                       E.JourNaissance,
                       E.ID,
                       E.Nom,
                       E.Prenom,
                       E.Photo,
                       E.Contact,
                       E.Email
                    )
                );
            }
            Form f = new FrmPreview("EtudiantListRpt.rdlc", items);
            f.Show();
        }
    }

    
    
}
