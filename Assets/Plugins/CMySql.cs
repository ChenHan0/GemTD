using UnityEngine;
using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;

public class CMySql : MonoBehaviour
{
    public static MySqlConnection dbConnection;//Just like MyConn.conn in StoryTools before    
    static string host = "54.169.71.90";
    static string id = "root";  //***不要变***  
    static string pwd = "oulihong";  //密码  
    static string database = "unity";//数据库名    
    static string result = "";

    private string strCommand = "Select ID from unity ;";
    public static DataSet MyObj;

    void OnGUI()
    {
        host = GUILayout.TextField(host, 200, GUILayout.Width(200));
        id = GUILayout.TextField(id, 200, GUILayout.Width(200));
        pwd = GUILayout.TextField(pwd, 200, GUILayout.Width(200));
        if (GUILayout.Button("Test"))
        {
            string connectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3};", host, database, id, pwd);
            openSqlConnection(connectionString);
            MyObj = GetDataSet(strCommand);

            //读取数据函数  
            ReaderData();

        }
        GUILayout.Label(result);
    }

    // On quit    
    public static void OnApplicationQuit()
    {
        closeSqlConnection();
    }

    // Connect to database    
    private static void openSqlConnection(string connectionString)
    {
        dbConnection = new MySqlConnection(connectionString);
        dbConnection.Open();
        result = dbConnection.ServerVersion;  //获得MySql的版本  
    }

    // Disconnect from database    
    private static void closeSqlConnection()
    {
        dbConnection.Close();
        dbConnection = null;
    }

    // MySQL Query    
    public static void doQuery(string sqlQuery)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Close();
        reader = null;
        dbCommand.Dispose();
        dbCommand = null;
    }
    #region Get DataSet    
    public DataSet GetDataSet(string sqlString)
    {
        DataSet ds = new DataSet();
        try
        {
            MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
            da.Fill(ds);

        }
        catch (Exception ee)
        {
            throw new Exception("SQL:" + sqlString + "\n" + ee.Message.ToString());
        }
        return ds;

    }
    #endregion

    //读取数据函数  
    void ReaderData()
    {
        MySqlCommand mySqlCommand = new MySqlCommand("Select * from unity;", dbConnection);
        MySqlDataReader reader = mySqlCommand.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    print("ID:" + reader.GetInt32(0) + "--Name：" + reader.GetString(1) + "--Sex：" + reader.GetString(2));
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("查询失败了！");
        }
        finally
        {
            reader.Close();
        }
    }
}