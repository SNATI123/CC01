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
    public partial class FrmEtablissementEdit : Form

    {
        private Action callBack;
        private Etablissement oldEtablissement;
       


        public FrmEtablissementEdit(object loadData)
        {
            InitializeComponent();
        }

        public FrmEtablissementEdit(Action callBack) : this()
        {
            this.callBack = callBack;
        }

        public FrmEtablissementEdit(Etablissement etablissement, Action callBack) : this(callBack)
        {
            this.oldEtablissement = etablissement;
            txtNom.Text = etablissement.Nom;
            txtAdresse.Text = etablissement.Adresse;
            txtEmail.Text = etablissement.Email;
            if (etablissement.Logo != null)
                pictureBox1.Image = Image.FromStream(new MemoryStream(etablissement.Logo));
        }

        public FrmEtablissementEdit()
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmEtablissementEdit_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();

                Etablissement newEtablissement = new Etablissement
                (
                    txtNom.Text.ToUpper(),
                    txtAdresse.Text,
                    txtEmail.Text,
                   !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldEtablissement?.Logo
                );

                EtablissementBLO etablissementBLO = new EtablissementBLO(ConfigurationManager.AppSettings["DbFolder"]);

                if (this.oldEtablissement == null)
                    etablissementBLO.CreateEcole(newEtablissement);
                else
                    etablissementBLO.EditEtablissement(oldEtablissement, newEtablissement);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                if (callBack != null)
                    callBack();

                if (oldEtablissement != null)
                    Close();

                txtNom.Clear();
                txtAdresse.Focus();
                txtEmail.Clear();

            }

            catch (TypingException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Erreur de saisie",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (Exception ex)
            {

                MessageBox.Show
               (
                   ex.Message,
                   "Duplicate error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
        }

        private void checkForm()
        {
            string text = string.Empty;
            txtAdresse.BackColor = Color.White;
            txtNom.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtAdresse.Text))
            {
                text += "- Veuillez entrer l'adresse svp! \n";
                txtAdresse.BackColor = Color.Red;
            }
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                text += "- Veuillez entrer le nom ! \n";
                txtNom.BackColor = Color.Red;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

    }
}
