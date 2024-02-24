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
    /// Interaction logic for FrmVrstaOpreme.xaml
    /// </summary>
    public partial class FrmVrstaOpreme : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmVrstaOpreme()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
         
        }

        public FrmVrstaOpreme(bool azuriraj, DataRowView red)
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
                cmd.Parameters.Add("@vrstaID", SqlDbType.Int).Value = txtID.Text;
                cmd.Parameters.Add("@imeVrste", SqlDbType.NVarChar).Value = txtVrsta.Text;

                if (azuriraj)
                {
                    DataRowView pomocnired = this.red;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocnired["ID"];
                    cmd.CommandText = "update tblVrsta SET vrstaID=@vrstaID, imeVrste=@imeVrste WHERE vrstaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblVrsta (vrstaID, imeVrste) values (@vrstaID, @imeVrste)";
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
