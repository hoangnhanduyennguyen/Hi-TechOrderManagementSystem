using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hi_Tech_Order_Management_System.Business;
using Hi_Tech_Order_Management_System.Validation;

namespace Hi_Tech_Order_Management_System.GUI
{
    public partial class FormBook : Form
    {
        public FormBook()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                Application.Exit();
            }
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                this.Hide();
                FormLogin login = new FormLogin();
                login.ShowDialog();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Validate data input
            // Get all inputs
            string iSBN = textBoxISBN.Text.Trim();
            string title = textBoxBookTitle.Text.Trim();
            string price = textBoxUnitPrice.Text.Trim();
            decimal uPrice = 0;
            string qoh = textBoxQOH.Text.Trim();
            string publisherId = textBoxPublisherID.Text.Trim();
            string categoryId = textBoxCategoryID.Text.Trim();
            // Check empty field
            if (Validator.IsEmpty(iSBN))
            {
                MessageBox.Show("ISBN is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Focus();
                return;
            }

            if (Validator.IsEmpty(title))
            {
                MessageBox.Show("Book Title is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookTitle.Focus();
                return;
            }

            if (Validator.IsEmpty(price))
            {
                MessageBox.Show("Unit Price is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUnitPrice.Focus();
                return;
            }

            if (Validator.IsEmpty(qoh))
            {
                MessageBox.Show("Quantity On Hand is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxQOH.Focus();
                return;
            }

            if (Validator.IsEmpty(publisherId))
            {
                MessageBox.Show("Publisher ID is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Focus();
                return;
            }

            if (Validator.IsEmpty(categoryId))
            {
                MessageBox.Show("Category ID is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Focus();
                return;
            }

            if (comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Status is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxBook.Focus();
                return;
            }

            // Check Invalid ID
            if (!Validator.IsValidId(iSBN, 13))
            {
                MessageBox.Show("Please enter a 13-digit ISBN.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            if (!Validator.IsValidId(publisherId, 1))
            {
                MessageBox.Show("Please enter a 1-digit Publisher ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            if (!Validator.IsValidId(categoryId, 2))
            {
                MessageBox.Show("Please enter a 2-digit Category ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }

            try
            {
                uPrice = Convert.ToDecimal(price);
            }
            catch (Exception )
            {
                MessageBox.Show("Please enter a valid Unit Price.","Invalid Unit Price",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBoxUnitPrice.Clear();
                textBoxUnitPrice.Focus();
                return;
            }

            // Check duplicate ISBN
            Book book = new Book();
            book = book.SearchBook(iSBN);
            if (book != null)
            {
                MessageBox.Show("This ISBN already exist", "Duplicate ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            Publisher pub = new Publisher();
            pub = pub.SearchPublisher(Convert.ToInt32(publisherId));
            if (pub == null)
            {
                MessageBox.Show("This Publisher ID does not exist", "Non-exist Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            Category cat = new Category();
            cat = cat.SearchCategory(Convert.ToInt32(categoryId));
            if (cat == null)
            {
                MessageBox.Show("This Category ID does not exist", "Non-exist Category ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }
            //When data is valid
            if (MessageBox.Show("Do you want to save this Book? ", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                string stt = comboBoxStatus.Text.Trim();
                string[] sttInfo = stt.Split('.');
                int sttId = Convert.ToInt32(sttInfo[0]);
                Book bookSave = new Book();
                bookSave.ISBN = iSBN;
                bookSave.BookTitle = title;
                bookSave.UnitPrice = uPrice;
                bookSave.QOH = Convert.ToInt32(qoh);
                bookSave.PublisherId = Convert.ToInt32(publisherId);
                bookSave.CategoryId = Convert.ToInt32(categoryId);
                bookSave.Status = sttId; 
                bookSave.SaveBook(bookSave);
                MessageBox.Show("Book has been saved successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Book has NOT been saved", "Save Unsuccessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxUnitPrice.Clear();
            textBoxQOH.Clear();
            textBoxPublisherID.Clear();
            textBoxCategoryID.Clear();
            listViewBook.Items.Clear();
            textBoxISBN.Focus();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // Validate data input
            // Get all inputs
            string iSBN = textBoxISBN.Text.Trim();
            string title = textBoxBookTitle.Text.Trim();
            string price = textBoxUnitPrice.Text.Trim();
            decimal uPrice = 0;
            string qoh = textBoxQOH.Text.Trim();
            string publisherId = textBoxPublisherID.Text.Trim();
            string categoryId = textBoxCategoryID.Text.Trim();
            // Check empty field
            if (Validator.IsEmpty(iSBN))
            {
                MessageBox.Show("ISBN is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Focus();
                return;
            }

            if (Validator.IsEmpty(title))
            {
                MessageBox.Show("Book Title is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookTitle.Focus();
                return;
            }

            if (Validator.IsEmpty(price))
            {
                MessageBox.Show("Unit Price is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUnitPrice.Focus();
                return;
            }

            if (Validator.IsEmpty(qoh))
            {
                MessageBox.Show("Quantity On Hand is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxQOH.Focus();
                return;
            }

            if (Validator.IsEmpty(publisherId))
            {
                MessageBox.Show("Publisher ID is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Focus();
                return;
            }

            if (Validator.IsEmpty(categoryId))
            {
                MessageBox.Show("Category ID is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Focus();
                return;
            }

            if(comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Status is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxBook.Focus();
                return;
            }
            // Check Invalid ID
            if (!Validator.IsValidId(iSBN, 13))
            {
                MessageBox.Show("Please enter a 13-digit ISBN.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            if (!Validator.IsValidId(publisherId, 1))
            {
                MessageBox.Show("Please enter a 1-digit Publisher ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            if (!Validator.IsValidId(categoryId, 2))
            {
                MessageBox.Show("Please enter a 2-digit Category ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }

            try
            {
                uPrice = Convert.ToDecimal(price);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid Unit Price.", "Invalid Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUnitPrice.Clear();
                textBoxUnitPrice.Focus();
                return;
            }

            // Check duplicate ISBN
            Book book = new Book();
            book = book.SearchBook(iSBN);
            if (book == null)
            {
                MessageBox.Show("This ISBN does not exist", "Non-exist ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            Publisher pub = new Publisher();
            pub = pub.SearchPublisher(Convert.ToInt32(publisherId));
            if (pub == null)
            {
                MessageBox.Show("This Publisher ID does not exist", "Non-exist Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            Category cat = new Category();
            cat = cat.SearchCategory(Convert.ToInt32(categoryId));
            if (cat == null)
            {
                MessageBox.Show("This Category ID does not exist", "Non-exist Category ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }

            if (MessageBox.Show("Do you want to update this Book information? ", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                string stt = comboBoxStatus.Text.Trim();
                string[] sttInfo = stt.Split('.');
                int sttId = Convert.ToInt32(sttInfo[0]);
                Book bookUpdate = new Book();
                bookUpdate.ISBN = iSBN;
                bookUpdate.BookTitle = title;
                bookUpdate.UnitPrice = uPrice;
                bookUpdate.QOH = Convert.ToInt32(qoh);
                bookUpdate.PublisherId = Convert.ToInt32(publisherId);
                bookUpdate.CategoryId = Convert.ToInt32(categoryId);
                bookUpdate.Status = sttId;
                bookUpdate.SaveBook(bookUpdate);
                MessageBox.Show("Book has been updated successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Book has NOT been updated", "Update Unsuccessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxUnitPrice.Clear();
            textBoxQOH.Clear();
            textBoxPublisherID.Clear();
            textBoxCategoryID.Clear();
            comboBoxStatus.SelectedIndex = -1;
            listViewBook.Items.Clear();
            textBoxISBN.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Validate data input
            // Get all inputs
            string iSBN = textBoxISBN.Text.Trim();
            string title = textBoxBookTitle.Text.Trim();
            string price = textBoxUnitPrice.Text.Trim();
            decimal uPrice = 0;
            string qoh = textBoxQOH.Text.Trim();
            string publisherId = textBoxPublisherID.Text.Trim();
            string categoryId = textBoxCategoryID.Text.Trim();
            // Check empty field
            if (Validator.IsEmpty(iSBN))
            {
                MessageBox.Show("ISBN is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Focus();
                return;
            }

            if (Validator.IsEmpty(title))
            {
                MessageBox.Show("Book Title is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookTitle.Focus();
                return;
            }

            if (Validator.IsEmpty(price))
            {
                MessageBox.Show("Unit Price is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUnitPrice.Focus();
                return;
            }

            if (Validator.IsEmpty(qoh))
            {
                MessageBox.Show("Quantity On Hand is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxQOH.Focus();
                return;
            }

            if (Validator.IsEmpty(publisherId))
            {
                MessageBox.Show("Publisher ID is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Focus();
                return;
            }

            if (Validator.IsEmpty(categoryId))
            {
                MessageBox.Show("Category ID is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Focus();
                return;
            }

            if (comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Status is empty", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxBook.Focus();
                return;
            }
            // Check Invalid ID
            if (!Validator.IsValidId(iSBN, 13))
            {
                MessageBox.Show("Please enter a 13-digit ISBN.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            if (!Validator.IsValidId(publisherId, 1))
            {
                MessageBox.Show("Please enter a 1-digit Publisher ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            if (!Validator.IsValidId(categoryId, 2))
            {
                MessageBox.Show("Please enter a 2-digit Category ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }

            try
            {
                uPrice = Convert.ToDecimal(price);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid Unit Price.", "Invalid Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUnitPrice.Clear();
                textBoxUnitPrice.Focus();
                return;
            }

            // Check duplicate ISBN
            Book book = new Book();
            book = book.SearchBook(iSBN);
            if (book == null)
            {
                MessageBox.Show("This ISBN does not exist", "Non-exist ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }

            Publisher pub = new Publisher();
            pub = pub.SearchPublisher(Convert.ToInt32(publisherId));
            if (pub == null)
            {
                MessageBox.Show("This Publisher ID does not exist", "Non-exist Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPublisherID.Clear();
                textBoxPublisherID.Focus();
                return;
            }

            Category cat = new Category();
            cat = cat.SearchCategory(Convert.ToInt32(categoryId));
            if (cat == null)
            {
                MessageBox.Show("This Category ID does not exist", "Non-exist Category ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCategoryID.Clear();
                textBoxCategoryID.Focus();
                return;
            }

            if (MessageBox.Show("Do you want to delete this Book information? ", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                book.Status = 6;
                book.UpdateBook(book);
                MessageBox.Show("Book has been set to inactive successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Book has NOT been deleted.", "Delete Unsuccessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxUnitPrice.Clear();
            textBoxQOH.Clear();
            textBoxPublisherID.Clear();
            textBoxCategoryID.Clear();
            comboBoxStatus.SelectedIndex = -1;
            listViewBook.Items.Clear();
            textBoxISBN.Focus();
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxUnitPrice.Clear();
            textBoxQOH.Clear();
            textBoxPublisherID.Clear();
            textBoxCategoryID.Clear();
            listViewBook.Items.Clear();
            textBoxInput.Clear();
            comboBoxBook.SelectedIndex = -1;
            textBoxISBN.Focus();
        }

        private void buttonSearchBook_Click(object sender, EventArgs e)
        {
            textBoxISBN.Clear();
            textBoxBookTitle.Clear();
            textBoxUnitPrice.Clear();
            textBoxQOH.Clear();
            textBoxPublisherID.Clear();
            textBoxCategoryID.Clear();
            comboBoxStatus.SelectedIndex = -1;
            listViewBook.Items.Clear();
            string input = textBoxInput.Text.Trim();
            if (Validator.IsEmpty(input))
            {
                MessageBox.Show("Input is empty.", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxInput.Focus();
                return;
            }

            //When data is valid
            int select = comboBoxBook.SelectedIndex;
            switch (select)
            {
                case -1:
                    MessageBox.Show("Please choose an option.", "Empty Option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBoxBook.Focus();
                    break;
                case 0:
                    if (!Validator.IsValidId(input, 13))
                    {
                        MessageBox.Show("Please input a 13-digit ISBN.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxInput.Clear();
                        textBoxInput.Focus();
                        return;
                    }
                    Book book = new Book();
                    book = book.SearchBook(input);
                    if (book == null)
                    {
                        MessageBox.Show("This ISBN does not exist.", "Non-exist ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxInput.Clear();
                        textBoxInput.Focus();
                        return;
                    }
                    else
                    {
                        textBoxISBN.Text = book.ISBN;
                        textBoxBookTitle.Text = book.BookTitle;
                        textBoxUnitPrice.Text = book.UnitPrice.ToString();
                        textBoxQOH.Text = book.QOH.ToString();
                        textBoxPublisherID.Text = book.PublisherId.ToString();
                        textBoxCategoryID.Text = book.CategoryId.ToString();
                        int stt = book.Status;
                        if (stt == 5)
                        {
                            comboBoxStatus.SelectedIndex = 0;
                        }
                        else
                        {
                            comboBoxStatus.SelectedIndex = 1;
                        }
                    }
                    textBoxInput.Clear();
                    labelInfo.Text = "";
                    comboBoxBook.SelectedIndex = -1;
                    break;
                case 1:
                    Book book1 = new Book();
                    List<Book> listBookTitle = new List<Book>();
                    listBookTitle = book1.SearchBookByTitle(input);
                    if (listBookTitle.Count != 0)
                    {
                        foreach (var b in listBookTitle)
                        {
                            ListViewItem item = new ListViewItem(b.ISBN.ToString());
                            item.SubItems.Add(b.BookTitle.ToString());
                            item.SubItems.Add(b.UnitPrice.ToString());
                            item.SubItems.Add(b.QOH.ToString());
                            item.SubItems.Add(b.PublisherId.ToString());
                            item.SubItems.Add(b.CategoryId.ToString());
                            item.SubItems.Add(b.Status.ToString());
                            listViewBook.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Book Title does not exist.", "Non-exist Book Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBoxInput.Clear();
                    labelInfo.Text = "";
                    comboBoxBook.SelectedIndex = -1;
                    break;
                case 2:
                    if (!Validator.IsValidId(input,1))
                    {
                        MessageBox.Show("Please input a 1-digit Publisher ID.", "Invalid Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxInput.Clear();
                        textBoxInput.Focus();
                        return;
                    }
                    int pId = Convert.ToInt32(input);
                    Book book2 = new Book();
                    List<Book> listBookPub = new List<Book>();
                    listBookPub = book2.SearchBook(pId, "PublisherId");
                    if (listBookPub.Count != 0)
                    {
                        foreach (var b in listBookPub)
                        {
                            ListViewItem item = new ListViewItem(b.ISBN.ToString());
                            item.SubItems.Add(b.BookTitle.ToString());
                            item.SubItems.Add(b.UnitPrice.ToString());
                            item.SubItems.Add(b.QOH.ToString());
                            item.SubItems.Add(b.PublisherId.ToString());
                            item.SubItems.Add(b.CategoryId.ToString());
                            item.SubItems.Add(b.Status.ToString());
                            listViewBook.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found.", "Empty Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBoxInput.Clear();
                    labelInfo.Text = "";
                    comboBoxBook.SelectedIndex = -1;
                    break;
                case 3:
                    if (!Validator.IsValidId(input, 2))
                    {
                        MessageBox.Show("Please input a 2-digit Category ID.", "Invalid Category ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxInput.Clear();
                        textBoxInput.Focus();
                        return;
                    }
                    int cId = Convert.ToInt32(input);
                    Book book3 = new Book();
                    List<Book> listBookCat = new List<Book>();
                    listBookCat = book3.SearchBook(cId, "CategoryId");
                    if (listBookCat.Count != 0)
                    {
                        foreach (var b in listBookCat)
                        {
                            ListViewItem item = new ListViewItem(b.ISBN.ToString());
                            item.SubItems.Add(b.BookTitle.ToString());
                            item.SubItems.Add(b.UnitPrice.ToString());
                            item.SubItems.Add(b.QOH.ToString());
                            item.SubItems.Add(b.PublisherId.ToString());
                            item.SubItems.Add(b.CategoryId.ToString());
                            item.SubItems.Add(b.Status.ToString());
                            listViewBook.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found.", "Empty Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBoxInput.Clear();
                    labelInfo.Text = "";
                    comboBoxBook.SelectedIndex = -1;
                    break;
            }
        }

        private void buttonSearchAll_Click(object sender, EventArgs e)
        {
            List<Book> listBook = new List<Book>();
            Book book = new Book();
            listBook = book.SearchAllBook();
            listViewBook.Items.Clear();
            if (listBook.Count != 0)
            {
                foreach (var b in listBook)
                {
                    ListViewItem item = new ListViewItem(b.ISBN.ToString());
                    item.SubItems.Add(b.BookTitle.ToString());
                    item.SubItems.Add(b.UnitPrice.ToString());
                    item.SubItems.Add(b.QOH.ToString());
                    item.SubItems.Add(b.PublisherId.ToString());
                    item.SubItems.Add(b.CategoryId.ToString());
                    item.SubItems.Add(b.Status.ToString());
                    listViewBook.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("There is no book in the database.", "Empty Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBoxBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxBook.SelectedIndex;
            if (select == 0)
            {
                labelInfo.Text = "Please input ISBN.";
            }
            else if (select == 1)
            {
                labelInfo.Text = "Please input Book Title.";
            }
            else if (select == 2)
            {
                labelInfo.Text = "Please input Publisher ID.";
            }
            else if (select == 3)
            {
                labelInfo.Text = "Please input Category ID.";
            }
        }

        private void FormBook_Load(object sender, EventArgs e)
        {
            Status status = new Status();
            List<Status> listStatus = status.SearchStatus("Book");
            foreach (var item in listStatus)
            {
                comboBoxStatus.Items.Add(item.Id + ". " + item.Description);
            }
        }
    }
}
