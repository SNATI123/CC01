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
    public partial class FrmEtudiantEdit : Form
    {
        private Action callBack;
        private Etudiant oldEtudiant;

        public FrmEtudiantEdit()
        {
            InitializeComponent();
        }
        public FrmEtudiantEdit(Action callBack) : this()
        {
            this.callBack = callBack;
        }
        public FrmEtudiantEdit(Etudiant etudiant, Action callBack) : this(callBack)
        {
            this.oldEtudiant = etudiant;
            txtFirstName.Text = etudiant.Nom;
            txtLastName.Text = etudiant.Prenom;
            txtContact.Text = etudiant.Contact;
            txtEmail.Text = etudiant.Email.ToString();
            txtDateOfBirth.Text = etudiant.JourNaissance;
            txtPlaceOfBirth.Text = etudiant.LieuNaissance;
            txtID.Text = etudiant.ID;
            if (etudiant.Photo != null)
                pictureBox3.Image = Image.FromStream(new MemoryStream(etudiant.Photo));
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();

                Etudiant newEtudiant = new Etudiant
                (
                    txtID.Text.ToUpper(),
                    txtLastName.Text,
                    txtFirstName.Text,
                    txtPlaceOfBirth.Text,
                    txtDateOfBirth.Text,
                    txtContact.Text,
                    txtEmail.Text,
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldEtudiant?.Photo
                );

                EtudiantBLO etudiantBLo = new EtudiantBLO(ConfigurationManager.AppSettings["DbFolder"]);

                if (this.oldEtudiant == null)
                    etudiantBLo.CreateEtudiant(newEtudiant);
                else
                    etudiantBLo.EditEtudiant(oldEtudiant, newEtudiant);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (callBack != null)
                    callBack();

                if (oldEtudiant != null)
                    Close();

                txtID.Clear();
                txtLastName.Clear();
                txtFirstName.Clear();
                txtPlaceOfBirth.Clear();
                txtDateOfBirth.Clear();
                txtContact.Clear();
                txtEmail.Clear();

            }
            catch (TypingException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Typing error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (DuplicateNameException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Duplicate error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Not found error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (Exception ex)
            {
                ex.WriteToFile();
                MessageBox.Show
               (
                   "An error occurred! Please try again later.",
                   "Erreur",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }
        }
        private void checkForm()
        {
            string text = string.Empty;
            txtID.BackColor = Color.White;
            txtLastName.BackColor = Color.White;
            txtFirstName.BackColor = Color.White;
            txtPlaceOfBirth.BackColor = Color.White;
            txtDateOfBirth.BackColor = Color.White;
            txtContact.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                text += "- Please enter the LastName ! \n";
                txtLastName.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtPlaceOfBirth.Text))
            {
                text += "- Please enter the BornOn ! \n";
                txtDateOfBirth.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                text += "- Please enter the FirstName ! \n";
                txtFirstName.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtPlaceOfBirth.Text))
            {
                text += "- Please enter the Born At ! \n";
                txtPlaceOfBirth.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                text += "- Please enter the Identified ! \n";
                txtID.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                text += "- Please enter the Contact ! \n";
                txtContact.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                text += "- Please enter the Identified ! \n";
                txtEmail.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choisissez une photo";
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }




        private void FrmEtudiant_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }
    }
}
