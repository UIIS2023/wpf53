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
    /// Interaction logic for FrmModel.xaml
    /// </summary>
    public partial class FrmModel : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmModel()
        {   
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
       

        }

        public FrmModel(bool azuriraj, DataRowView red)
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
                cmd.Parameters.Add("@modelID", SqlDbType.NVarChar).Value = txtID.Text;
                cmd.Parameters.Add("@imeModela", SqlDbType.NVarChar).Value = txtModel.Text;

                if (azuriraj)
                {
                    DataRowView pomocnired = this.red;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocnired["ID"];
                    cmd.CommandText = "update tblModel SET modelID=@modelID, imeModela=@imeModela where modelID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblModel (modelID, imeModela) values (@modelID, @imeModela)";
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

