using System.Reflection.Emit;

namespace CC01.WinForms
{
    public class ProductListPrint
    {
        private string LastName { get; set; }
        private string Firstname { get; set; }
        private string Contact { get; set; }
        private byte[] Picture { get; set; }
        private string DateOfBirth { get; set; }
        private string PlaceOfBirth { get; set; }
        private double Email { get; set; }

        public ProductListPrint(string lastname, string firstname,
            double email, byte[] picture, string dateofbirth, string placefbirth, string contact)
        {
            LastName = lastname;
            Firstname = firstname;
            Contact = contact;
            Picture = picture;
            DateOfBirth = dateofbirth;
            PlaceOfBirth = placefbirth;
            Email = email;

        }

        public ProductListPrint(string lastName, string firstName, string contact, byte[] picture, double email, string dateofbirth, string placeofbirth)
        {
            LastName = lastName;
            Firstname = firstName;
            Contact = contact;
            Picture = picture;
            Email = email;
            PlaceOfBirth = placeofbirth;
            DateOfBirth = dateofbirth;
        }
    }
}