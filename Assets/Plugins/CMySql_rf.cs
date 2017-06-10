using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using MySql.Data.MySqlClient;

public class CMySql : MonoBehaviour
{
    public static MySqlConnection dbConnection;//Just like MyConn.conn in StoryTools before    
    static string host = "127.0.0.1";
    static string id = "root";  //***不要变***  
    static string pwd = "234";  //密码  
    static string database = "unity";//数据库名    
    static string result = "";

    private string strCommand = "Select ID from unity ;";
    public static DataSet MyObj;
    public AudioSource ButtonAudio;

    public InputField UserName;
    public InputField Password;
    public Text hint;
    private string username;
    private string password;
    private float timer;
    private bool CanRegister = true;
    private bool ClickLogin = false;

    void Start()
    {
        //声明一个字符串用于存储连接数据库字符串
        string connectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3};", host, database, id, pwd);
        openSqlConnection(connectionString);
        MyObj = GetDataSet(strCommand);
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
        MySqlCommand mySqlCommand = new MySqlCommand("Select * from user;", dbConnection);
        MySqlDataReader reader = mySqlCommand.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    string name = reader.GetString(0);
                    string pw = reader.GetString(1);
                    if (username == name&&!ClickLogin)
                    {
                        hint.text = "该用户已存在！";
                        CanRegister = false;
                    }
                    else if(username == name && ClickLogin)
                    {
                        if (pw == password)
                        {
                            CanRegister = false;
                            SceneManager.LoadScene(1);
                        }
                        else
                        {
                            CanRegister = false;
                            hint.text = "密码错误！";
                        }
                    }
                    //print("UserName:" + reader.GetString(0)+ "--Password：" + reader.GetInt32(1));
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

    //增加
    public DataSet InsertInto(string tableName, string[] values)
    {
        DataSet ds = new DataSet();
        string query = "INSERT INTO " + tableName + " VALUES ("+ "'" + values[0] + "',"+ "'" + values[1] + "')";
        Debug.Log(query);
        MySqlDataAdapter da = new MySqlDataAdapter(query, dbConnection);
        da.Fill(ds);
        return ds;
    }

    public void Register()
    {
        ButtonAudio.Play();
        ClickLogin = false;
        CanRegister = true;
        username = UserName.text;
        password = Password.text;
        string[] newUser = { username, password };
        ReaderData();
        if (CanRegister)
        {
            InsertInto("user", newUser);
            hint.text = "注册成功！";
        }
    }

    public void Login()
    {
        ButtonAudio.Play();
        ClickLogin = true;
        CanRegister = true;
        username = UserName.text;
        password = Password.text;
        string[] User = { username, password };
        ReaderData();
        if (CanRegister)
        {
            hint.text = "该用户不存在！";
        }
    }

}