using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace SportksaOprema
{
    public class Konekcija
    {
        public SqlConnection KreirajKonekciju()
        {
            //pruza jednostavan nacin za kreiranje i upravljanje sadrzajem konekcionog stringa
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            {
                DataSource = @"SIMI\SQLEXPRESS", //naziv lokalnog servera Vašeg računara
                InitialCatalog = "SportskaOprema", //Baza na lokalnom serveru
                IntegratedSecurity = true //koristice se trenutni windows kredencijali za autentifikaciju, u slucaju da je false potrebno bi bilo u okviru konekcionog stringa navesti User ID i password
            };
            string con = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija;
        }

    }
}
