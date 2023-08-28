using System;
using System.Data.SqlClient;

namespace Database_Operation.DB
{

    class SelectStatement
    {

        public void Connect()
        {
            string constr;

            // for the connection to
            // sql server database
            SqlConnection conn;

            // Data Source is the name of the
            // server on which the database is stored.
            // The Initial Catalog is used to specify
            // the name of the database
            // The UserID and Password are the credentials
            // required to connect to the database.
            constr = @"Data Source=DESKTOP-GP8F496;Initial Catalog=Demodb;User ID=sa;Password=24518300";

            conn = new SqlConnection(constr);

            // to open the connection
            conn.Open();

            Console.WriteLine("Connection Open!");

            // to close the connection
            conn.Close();
        }
        public string Read()
        {
            string constr;

            // for the connection to
            // sql server database
            SqlConnection conn;

            // Data Source is the name of the
            // server on which the database is stored.
            // The Initial Catalog is used to specify
            // the name of the database
            // The UserID and Password are the credentials
            // required to connect to the database.
            constr = @"Data Source=DESKTOP-GP8F496;Initial Catalog=Demodb;User ID=sa;Password=24518300";

            conn = new SqlConnection(constr);

            // to open the connection
            conn.Open();

            // use to perform read and write
            // operations in the database
            SqlCommand cmd;

            // use to read a row in
            // table one by one
            SqlDataReader dreader;

            // to store SQL command and
            // the output of SQL command
            string sql, output = "";

            // use to fetch rows from demo table
            sql = "Select articleID, articleName from demo";

            // to execute the sql statement
            cmd = new SqlCommand(sql, conn);

            // fetch all the rows
            // from the demo table
            dreader = cmd.ExecuteReader();

            // for one by one reading row
            while (dreader.Read())
            {
                output = output + dreader.GetValue(0) + " - " +
                                    dreader.GetValue(1) + "\n";
            }

            // to display the output
            Console.Write(output);

            // to close all the objects
            dreader.Close();
            cmd.Dispose();
            conn.Close();
            return output;
        }
        public void Deposit(int val)
        {
            string constr;

            // for the connection to
            // sql server database
            SqlConnection conn;

            // Data Source is the name of the
            // server on which the database is stored.
            // The Initial Catalog is used to specify
            // the name of the database
            // The UserID and Password are the credentials
            // required to connect to the database.
            constr = @"Data Source=DESKTOP-GP8F496;Initial Catalog=Demodb;User ID=sa;Password=24518300";

            conn = new SqlConnection(constr);

            // to open the connection
            conn.Open();

            // use to perform read and write
            // operations in the database
            SqlCommand cmd;

            // data adapter object is use to
            // insert, update or delete commands
            SqlDataAdapter adap = new SqlDataAdapter();

            string sql = "";

            // use the defined sql statement
            // against our database
            sql = "insert into demo values("+ val + ")";

            // use to execute the sql command so we
            // are passing query and connection object
            cmd = new SqlCommand(sql, conn);

            // associate the insert SQL
            // command to adapter object
            adap.InsertCommand = new SqlCommand(sql, conn);

            // use to execute the DML statement against
            // our database
            adap.InsertCommand.ExecuteNonQuery();

            // closing all the objects
            cmd.Dispose();
            conn.Close();
        }
        public void Update()
        {
            string constr;

            // for the connection to
            // sql server database
            SqlConnection conn;

            // Data Source is the name of the
            // server on which the database is stored.
            // The Initial Catalog is used to specify
            // the name of the database
            // The UserID and Password are the credentials
            // required to connect to the database.
            constr = @"Data Source=DESKTOP-GP8F496;Initial Catalog=Demodb;User ID=sa;Password=24518300";

            conn = new SqlConnection(constr);

            // to open the connection
            conn.Open();

            // use to perform read and write
            // operations in the database
            SqlCommand cmd;

            // data adapter object is use to
            // insert, update or delete commands
            SqlDataAdapter adap = new SqlDataAdapter();

            string sql = "";

            // use the define sql
            // statement against our database
            sql = "update demo set articleName='django' where articleID=3";

            // use to execute the sql command so we
            // are passing query and connection object
            cmd = new SqlCommand(sql, conn);

            // associate the insert SQL
            // command to adapter object
            adap.InsertCommand = new SqlCommand(sql, conn);

            // use to execute the DML statement against
            // our database
            adap.InsertCommand.ExecuteNonQuery();

            // closing all the objects
            cmd.Dispose();
            conn.Close();
        }
        public void Withdraw(int val)
        {
            string constr;

            // for the connection to
            // sql server database
            SqlConnection conn;

            // Data Source is the name of the
            // server on which the database is stored.
            // The Initial Catalog is used to specify
            // the name of the database
            // The UserID and Password are the credentials
            // required to connect to the database.
            constr = @"Data Source=DESKTOP-GP8F496;Initial Catalog=Demodb;User ID=sa;Password=24518300";

            conn = new SqlConnection(constr);

            // to open the connection
            conn.Open();

            // use to perform read and write
            // operations in the database
            SqlCommand cmd;

            // data adapter object is use to
            // insert, update or delete commands
            SqlDataAdapter adap = new SqlDataAdapter();

            string sql = "";

            // use the define SQL statement
            // against our database
            sql = "delete from demo where Amount=("+val+")";

            // use to execute the sql command so we
            // are passing query and connection object
            cmd = new SqlCommand(sql, conn);

            // associate the insert SQL
            // command to adapter object
            adap.InsertCommand = new SqlCommand(sql, conn);

            // use to execute the DML statement
            // against our database
            adap.InsertCommand.ExecuteNonQuery();

            // closing all the objects
            cmd.Dispose();
            conn.Close();
        }


    }
}