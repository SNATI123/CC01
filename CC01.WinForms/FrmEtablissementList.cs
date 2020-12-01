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
    public partial class FrmModification : Form
    {
        private EtablissementBLO etablissementBLO;
        public FrmModification()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            etablissementBLO = new EtablissementBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }
        private void loadData()
        {
            string value = txtSearch.Text.ToLower();
            var ecoles = etablissementBLO.GetBy
            (
                x =>
                x.Adresse.ToLower().Contains(value) ||
                x.Nom.ToLower().Contains(value)
            ).OrderBy(x => x.Nom).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ecoles;
            dataGridView1.ClearSelection();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Form f = new FrmEtablissementEdit(loadData);
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
                    FrmEtudiantEdit frmEtudiant = new FrmEtudiantEdit
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Etudiant,
                        loadData
                    );
                    frmEtudiant.ShowDialog();
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
                        "Voulez vous vraiment supprimer ces etablissement?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        etablissementBLO.DeleteProduct(dataGridView1.SelectedRows[i].DataBoundItem as Etablissement);
                    }
                    loadData();
                }
            }
        }

        
    }
            
}
    

