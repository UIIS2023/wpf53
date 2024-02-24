using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for FrmOprema.xaml
    /// </summary>
    public partial class FrmOprema : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmOprema()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            NapuniPadajuceListe();
        }

        public FrmOprema(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
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
                cmd.Parameters.Add("@opremaID", SqlDbType.Int).Value = txtID.Text;
                cmd.Parameters.Add("@velicina", SqlDbType.NVarChar).Value = txtVelicina.Text;
                cmd.Parameters.Add("@opis", SqlDbType.NVarChar).Value = txtOpis.Text;
                cmd.Parameters.Add("@kolicina", SqlDbType.Int).Value = txtKolicina.Text;
                cmd.Parameters.Add("@pol", SqlDbType.NVarChar).Value = txtPol.Text;
                cmd.Parameters.Add("@cena", SqlDbType.Int).Value = txtCena.Text;
                cmd.Parameters.Add("@boja", SqlDbType.NVarChar).Value = txtBoja.Text;
                cmd.Parameters.Add("@modelID", SqlDbType.Int).Value = cbModel.SelectedValue;
                cmd.Parameters.Add("@brendID", SqlDbType.Int).Value = cbBrend.SelectedValue;
                cmd.Parameters.Add("@vrstaID", SqlDbType.Int).Value = cbVrsta.SelectedValue;
                cmd.Parameters.Add("@materijalID", SqlDbType.Int).Value = cbMaterijal.SelectedValue;

                if (azuriraj)
                {
                    DataRowView pomocnired = this.red;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocnired["ID"];
                    cmd.CommandText = "update tblOprema SET opremaID=@opremaID, velicina=@velicina, opis=@opis, kolicina=@kolicina, pol=@pol, cena=@cena, boja=@boja, modelID=@modelID, brendID=@brendID, vrstaID=@vrstaID, materijalID=@materijalID where opremaID=@id";
                    red = null;
                }

                else
                {
                    cmd.CommandText = @"insert into tblOprema (opremaID, velicina, opis, kolicina, pol, cena, boja, modelID, brendID, vrstaID, materijalID) values (@opremaID, @velicina, @opis, @kolicina, @pol, @cena, @boja, @modelID, @brendID, @vrstaID, @materijalID)";
                }
                    cmd.ExecuteNonQuery();
                cmd.Dispose();

                this.Close();
            }
        /*    catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          */  catch (FormatException)
            {
                MessageBox.Show("Unete vrednosti nisu u odredjenom formatu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally {
                konekcija.Close();
            }
        }
        private void NapuniPadajuceListe() {
            try
            {
                konekcija.Open();
                string PopuniMaterijal = @"select materijalID, nazivMaterijala from tblMaterijal";
                SqlDataAdapter daMaterijal = new SqlDataAdapter(PopuniMaterijal, konekcija);
                DataTable dtMaterijal = new DataTable();
                daMaterijal.Fill(dtMaterijal);
                cbMaterijal.ItemsSource = dtMaterijal.DefaultView;
                daMaterijal.Dispose();
                dtMaterijal.Dispose();

               
                string PopuniModel = @"select modelID, imeModela from tblModel";
                SqlDataAdapter daModel = new SqlDataAdapter(PopuniModel, konekcija);
                DataTable dtModel = new DataTable();
                daModel.Fill(dtModel);
                cbModel.ItemsSource = dtModel.DefaultView;
                daModel.Dispose();
                dtModel.Dispose();

                
                string PopuniBrend = @"select brendID, imeBrenda from tblBrend";
                SqlDataAdapter daBrend = new SqlDataAdapter(PopuniBrend, konekcija);
                DataTable dtBrend = new DataTable();
                daBrend.Fill(dtBrend);
                cbBrend.ItemsSource = dtBrend.DefaultView;
                daBrend.Dispose();
                dtBrend.Dispose();

              
                string PopuniVrstu = @"select vrstaID, imeVrste from tblVrsta";
                SqlDataAdapter daVrsta = new SqlDataAdapter(PopuniVrstu, konekcija);
                DataTable dtVrsta = new DataTable();
                daVrsta.Fill(dtVrsta);
                cbVrsta.ItemsSource = dtVrsta.DefaultView;
                daVrsta.Dispose();
                dtVrsta.Dispose();

            }
            catch
            {
                MessageBox.Show("Greska pri ucitavanju!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally {
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
