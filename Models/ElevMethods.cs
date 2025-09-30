using System.Data;
using Microsoft.Data.SqlClient;

namespace SkolSystem.Models
{

    public class ElevMethods
    {
        //konstruktor
        public ElevMethods() { }
        //publika metoder
        public int InsertElev(ElevDetails elevDetails, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30";

            String sqlString = "Insert into Tbl_Elev (El_Fornamn, El_Efternamn) values (@El_Fornamn, @El_Efternamn)";


            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.Add("El_Fornamn", System.Data.SqlDbType.NVarChar, 20).Value = elevDetails.El_Fornamn;
            sqlCommand.Parameters.Add("El_Efternamn", System.Data.SqlDbType.NVarChar, 20).Value = elevDetails.El_Efternamn;

            try
            {
                sqlConnection.Open();
                int i = 0;
                i = sqlCommand.ExecuteNonQuery();
                if (i == 1)
                { errormsg = ""; }
                else
                { errormsg = "Elev ej tillagd"; }
                return i;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<ElevDetails> GetElevDetailsList(out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30";
            String sqlString = "Select * from Tbl_Elev";
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            List<ElevDetails> elevDetailsList = new List<ElevDetails>();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Tbl_Elev");

                int i = 0;
                int count = 0;

                count = dataSet.Tables["Tbl_Elev"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        ElevDetails elevDetails = new ElevDetails();
                        elevDetails.El_Id = Convert.ToUInt16(dataSet.Tables["Tbl_Elev"].Rows[i]["El_Id"]);
                        elevDetails.El_Efternamn = dataSet.Tables["Tbl_Elev"].Rows[i]["El_Efternamn"].ToString();
                        elevDetails.El_Fornamn = dataSet.Tables["Tbl_Elev"].Rows[i]["El_Fornamn"].ToString();
                        i++;
                        elevDetailsList.Add(elevDetails);
                    }
                    errormsg = "";
                    return elevDetailsList;
                }
                else
                {
                    errormsg = "Inga elever funna";
                    return elevDetailsList;
                }

            }

            catch (Exception e)
            {
                errormsg = e.Message;
                return elevDetailsList;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public ElevDetails GetElevDetails(int El_Id, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30";
            String sqlString = "SELECT * FROM Tbl_Elev WHERE El_Id = @El_Id";
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.Add("El_Id", SqlDbType.Int).Value = El_Id;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Tbl_Elev");

                int count = 0;

                count = dataSet.Tables["Tbl_Elev"].Rows.Count;

                ElevDetails elevDetails = new ElevDetails();
                if (count == 1)
                {

                    elevDetails.El_Id = Convert.ToUInt16(dataSet.Tables["Tbl_Elev"].Rows[0]["El_Id"]);
                    elevDetails.El_Efternamn = dataSet.Tables["Tbl_Elev"].Rows[0]["El_Efternamn"].ToString();
                    elevDetails.El_Fornamn = dataSet.Tables["Tbl_Elev"].Rows[0]["El_Fornamn"].ToString();


                    errormsg = "";
                    return elevDetails;
                }
                else
                {
                    errormsg = "Inga elever funna";
                    return elevDetails;
                }

            }

            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public ElevDetails UpdateElevDetails(ElevDetails elevDetails, int El_Id, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30";
            String sqlString = "UPDATE Tbl_Elev SET El_Fornamn = @El_Fornamn, El_Efternamn = @El_Efternamn WHERE El_Id = @El_Id";

            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);

            sqlCommand.Parameters.AddWithValue("El_Id", El_Id);
            sqlCommand.Parameters.AddWithValue("El_Fornamn", elevDetails.El_Fornamn);
            sqlCommand.Parameters.AddWithValue("El_Efternamn", elevDetails.El_Efternamn);

            try
            {
                sqlConnection.Open();
                int i = sqlCommand.ExecuteNonQuery();

                if (1 > 0)
                {
                    errormsg = "";
                    return elevDetails;
                }
                else
                {
                    errormsg = "Inga elever funna";
                    return elevDetails;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public ElevDetails DeleteElevDetails(int El_Id, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=localhost,1433;Initial Catalog=SkolDB;User ID=sa;Password=Chandler-420;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30";
            ElevDetails elevDetails = new ElevDetails();
            String sqlString = "DELETE FROM Tbl_Elev WHERE El_Id = @El_Id";
            
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.Add("El_Id", System.Data.SqlDbType.Int).Value = El_Id;

            try
            {
                sqlConnection.Open();
                int i = sqlCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    errormsg = "";
                    return elevDetails;
                }
                else
                {
                    errormsg = "Elev ej borttagen";
                }
                return null;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }

}