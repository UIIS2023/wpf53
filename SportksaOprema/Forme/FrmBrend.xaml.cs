using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SportksaOprema.Forme
{
    /// <summary>
    /// Interaction logic for FrmBrend.xaml
    /// </summary>
    public partial class FrmBrend : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmBrend()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
          

        }

        public FrmBrend(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            this.azuriraj = azuriraj;
            this.red = red;
            konekcija = kon.KreirajKonekciju();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija

                };
                cmd.Parameters.Add("@brendID", SqlDbType.NVarChar).Value = txtID.Text;
                cmd.Parameters.Add("@imeBrenda", SqlDbType.NVarChar).Value = txtNazivBrenda.Text;

                if (azuriraj)
                {
                    DataRowView pomocnired = this.red;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocnired["ID"];
                    cmd.CommandText = "update tblBrend SET brendID=@brendID, imeBrenda=@imeBrenda where brendID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblBrend (brendID, imeBrenda) values (@brendID, @imeBrenda)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Unete vrednosti nisu u odredjenom formatu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
