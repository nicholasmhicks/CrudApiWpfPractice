using CrudWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLog;

namespace CrudWPF.Pages
{
    /// <summary>
    /// Interaction logic for AuthorsCRUD.xaml
    /// </summary>
    public partial class AuthorsCRUD : Page
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        DataAccess _da = new DataAccess();
        public AuthorsCRUD()
        {
            InitializeComponent();
        }

        //Data grid
        void FillTable(List<Author> entities)
        {
            DataTable dt = new DataTable();

            DataColumn AuthorId = new DataColumn("Id", typeof(string));
            DataColumn FirstName = new DataColumn("First Name", typeof(string));
            DataColumn Surname = new DataColumn("Surname", typeof(string));
            DataColumn TaxFileNumber = new DataColumn("Tax File Number", typeof(string));

            dt.Columns.Add(AuthorId);
            dt.Columns.Add(FirstName);
            dt.Columns.Add(Surname);
            dt.Columns.Add(TaxFileNumber);

            foreach (var i in entities)
            {
                DataRow _row = dt.NewRow();
                _row[0] = i.AuthorId;
                _row[1] = i.FirstName;
                _row[2] = i.SurName;
                _row[3] = i.TaxFileNumber;
                dt.Rows.Add(_row);
            }
            entitiesGrid.ItemsSource = dt.DefaultView;
        }

        //Event handler
        private void entitiesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row != null)
            {
                DataGridCell RowColumnId = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                DataGridCell RowColumnFirstName = dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
                DataGridCell RowColumnSurname = dataGrid.Columns[2].GetCellContent(row).Parent as DataGridCell;
                DataGridCell RowColumnTaxFileNumber = dataGrid.Columns[3].GetCellContent(row).Parent as DataGridCell;

                AuthorIdField.Text = ((TextBlock)RowColumnId.Content).Text;
                FirstNameField.Text = ((TextBlock)RowColumnFirstName.Content).Text;
                SurnameField.Text = ((TextBlock)RowColumnSurname.Content).Text;
                TaxFileNumberField.Text = ((TextBlock)RowColumnTaxFileNumber.Content).Text;
            }

        }

        private async void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                Author a = new Author();
                a.AuthorId = AuthorIdField.Text;
                a.FirstName = FirstNameField.Text;
                a.SurName = SurnameField.Text;
                a.TaxFileNumber = TaxFileNumberField.Text;

                if (a.FirstName == "Fuck")
                {
                    MessageBox.Show("Inappropriate First name, please re-enter");
                    throw new Exception("Inappropriate name entered");
                }

                if (await _da.PutAuthorEntityAsync($"api/Authors/{AuthorIdField.Text}", a))
                {
                    this.FillTable(await _da.GetAuthorEntityAsync("api/Authors"));
                }
                else if (await _da.PostAuthorEntityAsync($"api/Authors/{AuthorIdField.Text}", a))
                {
                    this.FillTable(await _da.GetAuthorEntityAsync("api/Authors"));
                }

                _logger.Trace($"Author id:{a.AuthorId} was updated at {DateTime.Now.ToString()}");

            }catch (Exception ex)
            {
                _logger.Error($"Update author exception thrown: {ex.Message}");
            }

        }

        private async void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await _da.DeleteAuthorEntityAsync($"api/Authors/{AuthorIdField.Text}"))
                {
                    this.FillTable(await _da.GetAuthorEntityAsync("api/Authors"));
                }

                _logger.Trace($"Author id:{AuthorIdField.Text} was deleted at {DateTime.Now.ToString()}");
            } catch (Exception ex)
            {
                _logger.Error($"Delete author exception thrown: {ex.Message}");
            }
            
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.GoBack();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            this.FillTable(await _da.GetAuthorEntityAsync("api/Authors"));
        }


    }
}
