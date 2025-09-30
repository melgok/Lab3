using System.Data;
using Microsoft.Data.SqlClient;

namespace SkolSystem.Models
{
    public class RegistreringMethods
    {
        public RegistreringMethods() { }
        public List<RegistreringDetails> GetRegistreringDetailsList(out string errormsg, string valdKurs) //visar vilka elever som är registrerade på vilka kurser
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30";
            String sqlString = "SELECT DISTINCT e.El_Id, e.El_Fornamn, e.El_Efternamn, k.Ku_Namn FROM Tbl_Resultat r JOIN Tbl_Elev e ON r.El_Id = e.El_Id JOIN Tbl_Kurs k ON r.Ku_Id = k.Ku_Id";
            if (!string.IsNullOrEmpty(valdKurs))
            {
                sqlString += " WHERE k.Ku_Namn = @Ku_Namn";
            }

            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);

            if (!string.IsNullOrEmpty(valdKurs))
            {
                sqlCommand.Parameters.AddWithValue("Ku_Namn", valdKurs);
            }


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            List<RegistreringDetails> registreringDetailsList = new List<RegistreringDetails>();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Registrering");

                int i = 0;
                int count = 0;

                count = dataSet.Tables["Registrering"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        RegistreringDetails registreringDetails = new RegistreringDetails();
                        registreringDetails.El_Efternamn = dataSet.Tables["Registrering"].Rows[i]["El_Efternamn"].ToString();
                        registreringDetails.El_Fornamn = dataSet.Tables["Registrering"].Rows[i]["El_Fornamn"].ToString();
                        registreringDetails.Ku_Namn = dataSet.Tables["Registrering"].Rows[i]["Ku_Namn"].ToString();
                        i++;
                        registreringDetailsList.Add(registreringDetails);
                    }
                    errormsg = "";
                    return registreringDetailsList;
                }
                else
                {
                    errormsg = "Inga elever funna";
                    return registreringDetailsList;
                }

            }

            catch (Exception e)
            {
                errormsg = e.Message;
                return registreringDetailsList;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

       /*public int InsertElevToKurs(int El_Id, int Ku_Id, string Re_Betyg, out string errormsg)
        {
            errormsg = "";
            int rows = 0;
            try
            {
                using (var sqlConnection = new SqlConnection("Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30"))
                {
                    string sqlString = "INSERT INTO Tbl_Resultat (El_Id, Ku_Id, Re_Betyg) VALUES (@El_Id, @Ku_Id, 'G')";
                    using (var sqlCommand = new SqlCommand(sqlString, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@El_Id", El_Id);
                        sqlCommand.Parameters.AddWithValue("@Ku_Id", Ku_Id);
                        sqlCommand.Parameters.AddWithValue("@Re_Betyg", "G");
                        sqlConnection.Open();
                        rows = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }
            return rows;
        }
        public List<KursDetails> GetKursDetailsList(out string errormsg) //hämtar alla kurser från kurs-tabellen
{
    var kursList = new List<KursDetails>(); 
    errormsg = "";
    try
    {
        using (var sqlConnection = new SqlConnection("Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30"))
        {
            string sqlString = "SELECT Ku_Id, Ku_Namn FROM Tbl_Kurs";
            using (var sqlCommand = new SqlCommand(sqlString, sqlConnection))
            {
                sqlConnection.Open();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kursList.Add(new KursDetails
                        {
                            Ku_Id = Convert.ToInt32(reader["Ku_Id"]),
                            Ku_Namn = reader["Ku_Namn"].ToString()
                        });
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        errormsg = ex.Message;
    }
    return kursList;
}*/

    }
}



