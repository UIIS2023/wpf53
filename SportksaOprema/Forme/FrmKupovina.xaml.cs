using System;
using System.Collections.Generic;
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
using System.Data;

namespace SportksaOprema.Forme
{
    /// <summary>
    /// Interaction logic for FrmKupovina.xaml
    /// </summary>
    public partial class FrmKupovina : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmKupovina()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            NapuniPadajuceListe();
        }

        public FrmKupovina(bool azuriraj, DataRowView red)
        {
            this.azuriraj = azuriraj;
            this.red = red;
            InitializeComponent();
            NapuniPadajuceListe();
            konekcija = kon.KreirajKonekciju();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija

                };
                cmd.Parameters.Add("@kupovinaID", SqlDbType.Int).Value = txtID.Text;
                cmd.Parameters.Add("@vrstaKupovine", SqlDbType.NVarChar).Value = txtVrstaKupovine.Text;
                cmd.Parameters.Add("@brojRacuna", SqlDbType.Int).Value = txtBrojRacuna.Text;
                cmd.Parameters.Add("@popust", SqlDbType.Bit).Value = checkPopust.IsChecked;
                cmd.Parameters.Add("@popustIznos", SqlDbType.Int).Value = txtIznosPopusta.Text;
                cmd.Parameters.Add("@datum", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@kupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@opremaID", SqlDbType.Int).Value = cbOprema.SelectedValue;
                cmd.Parameters.Add("@zaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                cmd.Parameters.Add("@garancijaID", SqlDbType.Int).Value = cbGarancija.SelectedValue;

                if (azuriraj)
                {
                    DataRowView pomocnired = this.red;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocnired["ID"];
                    cmd.CommandText = "update tblKupovina SET kupovinaID=@kupovinaID, vrstaKupovine=@vrstaKupovine, brojRacuna=@brojRacuna, popust=@popust, popustIznos=@popustIznos, datum=@datum, kupacID=@kupacID, opremaID=@opremaID, zaposleniID=@zaposleniID, garancijaID=@garancijaID where kupovinaID=@id";
                    red = null;
                }
                else
                {

                    cmd.CommandText = @"insert into tblKupovina (kupovinaID,vrstaKupovine, brojRacuna, popust, popustIznos, datum, kupacID, opremaID, zaposleniID, garancijaID) values (@kupovinaID ,@vrstaKupovine, @brojRacuna, @popust, @popustIznos, @datum, @kupacID, @opremaID, @zaposleniID, @garancijaID)";
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
                string PopuniKupca = @"select kupacID, imeKupca from tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(PopuniKupca, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();


                string PopuniOpremu = @"select opremaID, opis from tblOprema";
                SqlDataAdapter daOprema = new SqlDataAdapter(PopuniOpremu, konekcija);
                DataTable dtOprema = new DataTable();
                daOprema.Fill(dtOprema);
                cbOprema.ItemsSource = dtOprema.DefaultView;
                daOprema.Dispose();
                dtOprema.Dispose();


                string PopuniZaposlenog = @"select zaposleniID, ime from tblZaposleni";
                SqlDataAdapter daZaposleni = new SqlDataAdapter(PopuniZaposlenog, konekcija);
                DataTable dtZaposleni = new DataTable();
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                daZaposleni.Dispose();
                dtZaposleni.Dispose();


                string PopuniGaranciju = @"select garancijaID, trajanje from tblGarancija";
                SqlDataAdapter daGarancija = new SqlDataAdapter(PopuniGaranciju, konekcija);
                DataTable dtGarancija = new DataTable();
                daGarancija.Fill(dtGarancija);
                cbGarancija.ItemsSource = dtGarancija.DefaultView;
                daGarancija.Dispose();
                dtGarancija.Dispose();

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
