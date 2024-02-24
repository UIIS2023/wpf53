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
    /// Interaction logic for FrmReklamacija.xaml
    /// </summary>
    public partial class FrmReklamacija : Window
    {   
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmReklamacija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            NapuniPadajuceListe();

        }

        public FrmReklamacija(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            this.azuriraj = azuriraj;
            this.red = red;
            konekcija = kon.KreirajKonekciju();
            NapuniPadajuceListe();
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
                cmd.Parameters.Add("@reklamacijaID", SqlDbType.Int).Value = txtID.Text;
                cmd.Parameters.Add("@opisGarancije", SqlDbType.NVarChar).Value = txtOpis.Text;
                cmd.Parameters.Add("@zaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                cmd.Parameters.Add("@kupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@uvazena", SqlDbType.Bit).Value = checkUvazena.IsChecked;

                if (azuriraj)
                {
                    DataRowView pomocnired = this.red;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocnired["ID"];
                    cmd.CommandText = "update tblReklamacija SET reklamacijaID=@reklamacijaID, opisGarancije=@opisGarancije, zaposleniID=@zaposleniID, kupacID=@kupacID, uvazena=@uvazena where reklamacijaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblReklamacija (reklamacijaID, opisGarancije, zaposleniID, kupacID, uvazena) values (@reklamacijaID, @opisGarancije, @zaposleniID, @kupacID, @uvazena)";
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
        private void NapuniPadajuceListe()
        {
            try
            {        

                konekcija.Open();
                string PopuniZaposlenog = @"select zaposleniID, ime from tblZaposleni";
                SqlDataAdapter daZaposleni = new SqlDataAdapter(PopuniZaposlenog, konekcija);
                DataTable dtZaposleni = new DataTable();
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                daZaposleni.Dispose();
                dtZaposleni.Dispose();

               
                string PopuniKupca = @"select kupacID, imeKupca from tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(PopuniKupca, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                daKupac.Dispose();

            }
            catch
            {
                MessageBox.Show("Greska pri ucitavanju!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
