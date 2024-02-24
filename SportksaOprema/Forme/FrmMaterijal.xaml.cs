
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
using System.Data.SqlClient;
namespace SportksaOprema.Forme
{
    /// <summary>
    /// Interaction logic for FrmMaterijal.xaml
    /// </summary>
    public partial class FrmMaterijal : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public FrmMaterijal()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
        }
        public FrmMaterijal(bool azuriraj, DataRowView red)
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
                cmd.Parameters.Add("@materijalID", SqlDbType.Int).Value = txtID.Text;
                cmd.Parameters.Add("@nazivMaterijala", SqlDbType.NVarChar).Value = txtMaterijal.Text;

                if (azuriraj)
                {
                    DataRowView pomocnired = this.red;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = "update tblMaterijal SET materijalID=@materijalID, nazivMaterijala=@nazivMaterijala where materijalID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblMaterijal (materijalID, nazivMaterijala) values (@materijalID, @nazivMaterijala)";
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
