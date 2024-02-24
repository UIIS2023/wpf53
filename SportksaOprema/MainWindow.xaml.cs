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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using SportksaOprema.Forme;
using System.Windows.Media.Animation;
using System.Net;

namespace SportksaOprema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private string ucitanaTabela;
        private bool azuriraj;
        private DataRowView red;

        #region Select upiti

        private static string opremeSelect = @"select opremaID as ID, velicina as Velicina, opis as Opis, kolicina as Kolicina , pol, cena, boja from tblOprema
                                                join tblModel on tblOprema.modelID = tblModel.modelID
                                                join tblBrend on tblOprema.brendID = tblBrend.brendID
                                                join tblVrsta on tblOprema.vrstaID = tblVrsta.vrstaID
                                                join tblMaterijal on tblOprema.materijalID = tblMaterijal.materijalID";

        private static string materijaliSelect = @"select materijalID as ID, nazivMaterijala as Materijal from tblMaterijal";
        private static string modeliSelect = @"select modelID as ID, imeModela as Model from tblModel";
        private static string brendoviSelect = @"select brendID as ID, imeBrenda as Brend from tblBrend";
        private static string vrsteSelect = @"select vrstaID as ID, imeVrste as Vrsta from tblVrsta";
        private static string kupovineSelect = @"select kupovinaID as ID, vrstaKupovine as 'Vrsta kupovine', brojRacuna as 'Broj racuna', popust, popustIznos as 'Iznos popusta', tblKupovina.datum from tblKupovina
                                                join tblKupac on tblKupovina.kupacID = tblKupac.kupacID
                                                join tblOprema on tblKupovina.opremaID = tblOprema.opremaID
                                                join tblZaposleni on tblKupovina.zaposleniID = tblZaposleni.zaposleniID
                                                join tblGarancija on tblKupovina.garancijaID = tblGarancija.garancijaID";

        private static string reklamacijeSelect = @"select reklamacijaID as ID, opisGarancije as 'Opis garancije', uvazena from tblReklamacija
                                                join tblZaposleni on tblReklamacija.zaposleniID = tblZaposleni.zaposleniID                                                    
                                                join tblKupac on tblReklamacija.kupacID = tblKupac.kupacID";
        #endregion


        #region Select sa uslovom
        private static string selectUslovOpreme = @"select * from tblOprema where opremaID = ";
        private static string selectUslovModeli = @"select * from tblModel where modelID= ";
        private static string selectUslovKupovine= @"select * from tblKupovina where kupovinaID = ";
        private static string selectUslovReklamacije = @"select * from tblReklamacija where reklamacijaID = ";
        private static string selectUslovMaterijali = @"select * from tblMaterijal where materijalID = ";
        private static string selectUslovVrsteOpreme = @"select * from tblVrsta where vrstaID = ";
        private static string selectUslovBrendovi = @"select * from tblBrend where brendID = ";
        #endregion

        #region Delete sa uslovom

        private static string opremeDelete = @"delete from tblOprema where opremaID = ";
        private static string modeliDelete = @"delete from tblModel where modelID= ";
        private static string kupovineDelete= @"delete from tblKupovina where kupovinaID = ";
        private static string reklamacijeDelete = @"delete from tblReklamacija where reklamacijaID = ";
        private static string materijaliDelete = @"delete from tblMaterijal where materijalID = ";
        private static string vrsteOpremeDelete = @"delete from tblVrsta where vrstaID = ";
        private static string brendoviDelete = @"delete from tblBrend where brendID = ";

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(opremeSelect);
            
        }

        private void UcitajPodatke(string selectUpit) {

            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dataAdapter.Dispose();
                dataTable.Dispose();
                {

                }
            }
            catch (SqlException)
            {

                MessageBox.Show("Neuspesno ucitani podaci!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        
        }

        private void btnKupovina_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(kupovineSelect);
        }

        private void btnReklamacije_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(reklamacijeSelect);
        }

        private void btnOprema_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(opremeSelect);
        }

        private void btnMaterijali_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(materijaliSelect);
        }

        private void btnModeli_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(modeliSelect);
        }

        private void btnVrsteOpreme_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(vrsteSelect);
        }

        private void btnBrendovi_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(brendoviSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(opremeSelect))
            {
                prozor = new FrmOprema();
                prozor.ShowDialog();
                UcitajPodatke(opremeSelect);
            }
            if (ucitanaTabela.Equals(materijaliSelect))
            {
                prozor = new FrmMaterijal();
                prozor.ShowDialog();
                UcitajPodatke(materijaliSelect);
            }
            if (ucitanaTabela.Equals(modeliSelect))
            {
                prozor = new FrmModel();
                prozor.ShowDialog();
                UcitajPodatke(modeliSelect);
            }
            if (ucitanaTabela.Equals(reklamacijeSelect))
            {
                prozor = new FrmReklamacija();
                prozor.ShowDialog();
                UcitajPodatke(reklamacijeSelect);
            }
            if (ucitanaTabela.Equals(vrsteSelect))
            {
                prozor = new FrmVrstaOpreme();
                prozor.ShowDialog();
                UcitajPodatke(vrsteSelect);
            }
            if (ucitanaTabela.Equals(brendoviSelect))
            {
                prozor = new FrmBrend();
                prozor.ShowDialog();
                UcitajPodatke(brendoviSelect);
            }
            if (ucitanaTabela.Equals(kupovineSelect))
            {
                prozor = new FrmKupovina();
                prozor.ShowDialog();
                UcitajPodatke(kupovineSelect);
            }
        }

        private void PopuniFormu(object selectUslov) {

            try
            {
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija

                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(opremeSelect))
                    {
                        FrmOprema prozorOprema = new FrmOprema(azuriraj, red);
                        prozorOprema.txtID.Text = citac["opremaID"].ToString();
                        prozorOprema.txtBoja.Text = citac["boja"].ToString();
                        prozorOprema.txtPol.Text = citac["pol"].ToString();
                        prozorOprema.txtKolicina.Text = citac["kolicina"].ToString();
                        prozorOprema.txtVelicina.Text = citac["velicina"].ToString();
                        prozorOprema.txtOpis.Text = citac["opis"].ToString();
                        prozorOprema.txtBoja.Text = citac["boja"].ToString();
                        prozorOprema.txtCena.Text = citac["cena"].ToString();
                        prozorOprema.cbBrend.SelectedValue = citac["brendID"].ToString();
                        prozorOprema.cbMaterijal.SelectedValue = citac["materijalID"].ToString();
                        prozorOprema.cbModel.SelectedValue = citac["modelID"].ToString();
                        prozorOprema.cbVrsta.SelectedValue = citac["vrstaID"].ToString();
                        prozorOprema.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(materijaliSelect))
                    {
                        FrmMaterijal prozorMaterijal = new FrmMaterijal(azuriraj, red);
                        prozorMaterijal.txtID.Text = citac["materijalID"].ToString();
                        prozorMaterijal.txtMaterijal.Text = citac["nazivMaterijala"].ToString();
                        prozorMaterijal.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(brendoviSelect))
                    {
                        FrmBrend prozorBrend = new FrmBrend(azuriraj, red);
                        prozorBrend.txtID.Text = citac["brendID"].ToString();
                        prozorBrend.txtNazivBrenda.Text = citac["imeBrenda"].ToString();
                        prozorBrend.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(modeliSelect))
                    {
                        FrmModel prozorModel = new FrmModel(azuriraj, red);
                        prozorModel.txtID.Text = citac["modelID"].ToString();
                        prozorModel.txtModel.Text = citac["imeModela"].ToString();
                        prozorModel.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(reklamacijeSelect))
                    {
                        FrmReklamacija prozorReklamacija = new FrmReklamacija(azuriraj, red);
                        prozorReklamacija.txtID.Text = citac["reklamacijaID"].ToString();
                        prozorReklamacija.txtOpis.Text = citac["opisGarancije"].ToString();
                        prozorReklamacija.cbZaposleni.SelectedValue = citac["zaposleniID"].ToString();
                        prozorReklamacija.cbKupac.SelectedValue = citac["kupacID"].ToString();
                        prozorReklamacija.checkUvazena.IsChecked = (bool)citac["uvazena"];
                        prozorReklamacija.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(vrsteSelect))
                    {
                        FrmVrstaOpreme prozorVrste = new FrmVrstaOpreme(azuriraj, red);
                        prozorVrste.txtID.Text = citac["vrstaID"].ToString();
                        prozorVrste.txtVrsta.Text = citac["imeVrste"].ToString();
                        prozorVrste.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(kupovineSelect))
                    {
                        FrmKupovina prozorKupovina = new FrmKupovina(azuriraj, red);
                        prozorKupovina.txtID.Text = citac["kupovinaID"].ToString();
                        prozorKupovina.txtBrojRacuna.Text = citac["brojRacuna"].ToString();
                        prozorKupovina.txtIznosPopusta.Text = citac["popustIznos"].ToString();
                        prozorKupovina.txtVrstaKupovine.Text = citac["vrstaKupovine"].ToString();
                        prozorKupovina.cbGarancija.SelectedValue = citac["garancijaID"].ToString();
                        prozorKupovina.cbKupac.SelectedValue = citac["kupacID"].ToString();
                        prozorKupovina.cbOprema.SelectedValue = citac["opremaID"].ToString();
                        prozorKupovina.cbZaposleni.SelectedValue = citac["zaposleniID"].ToString();
                        prozorKupovina.checkPopust.IsChecked = (bool)citac["popust"];
                        prozorKupovina.dpDatum.SelectedDate = (DateTime)citac["datum"];
                        prozorKupovina.ShowDialog();
                    }
                }

            }
            catch (SqlException)
            {

                Console.WriteLine("Greska u toku povezivanja sa bazom!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentOutOfRangeException)
            {

                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally {
                if (konekcija != null) {
                    konekcija.Close();
                }
            }
        
        }
        private void ObrisiZapis(string deleteUpit) {

            try
            {
                konekcija.Open();
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUpit + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {

                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (SqlException) {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally{
                konekcija.Close();
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(opremeSelect)){
                ObrisiZapis(opremeDelete);
                UcitajPodatke(opremeSelect);
            }
           else if (ucitanaTabela.Equals(modeliSelect))
            {
                ObrisiZapis(modeliDelete);
                UcitajPodatke(modeliSelect);
            }
           else if (ucitanaTabela.Equals(materijaliSelect))
            {
                ObrisiZapis(materijaliDelete);
                UcitajPodatke(materijaliSelect);
            }
          else if (ucitanaTabela.Equals(vrsteSelect))
            {
                ObrisiZapis(vrsteOpremeDelete);
                UcitajPodatke(vrsteSelect);
            }
           else if (ucitanaTabela.Equals(brendoviSelect))
            {
                ObrisiZapis(brendoviDelete);
                UcitajPodatke(brendoviSelect);
            }
           else if (ucitanaTabela.Equals(reklamacijeSelect))
            {
                ObrisiZapis(reklamacijeDelete);
                UcitajPodatke(reklamacijeSelect);
            }
           else if (ucitanaTabela.Equals(kupovineSelect))
            {
                ObrisiZapis(kupovineDelete);
                UcitajPodatke(kupovineSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(opremeSelect))
            {
                PopuniFormu(selectUslovOpreme);
                UcitajPodatke(opremeSelect);
            }
            else if (ucitanaTabela.Equals(modeliSelect))
            {
                PopuniFormu(selectUslovModeli);
                UcitajPodatke(modeliSelect);
            }
            else if (ucitanaTabela.Equals(materijaliSelect))
            {
                PopuniFormu(selectUslovMaterijali);
                UcitajPodatke(materijaliSelect);
            }
            else if (ucitanaTabela.Equals(vrsteSelect))
            {
                PopuniFormu(selectUslovVrsteOpreme);
                UcitajPodatke(vrsteSelect);
            }
            else if (ucitanaTabela.Equals(brendoviSelect))
            {
                PopuniFormu(selectUslovBrendovi);
                UcitajPodatke(brendoviSelect);
            }
            else if (ucitanaTabela.Equals(reklamacijeSelect))
            {
                PopuniFormu(selectUslovReklamacije);
                UcitajPodatke(reklamacijeSelect);
            }
            else if (ucitanaTabela.Equals(kupovineSelect))
            {
                PopuniFormu(selectUslovKupovine);
                UcitajPodatke(kupovineSelect);
            }
        }
    }
}
