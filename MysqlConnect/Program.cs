using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace MysqlConnect
{
	class Program
	{
		static void Main(string[] args)
		{
			string pass = "";
			string host = "";
			string username = "";
			string database = "";

			ConsoleKeyInfo key;
			Console.Write ("Host: ");
			host = Console.ReadLine ();
			Console.Write ("User: ");
			username = Console.ReadLine ();
			Console.Write ("Database: ");
			database = Console.ReadLine ();

			System.Console.Write("Podaj hasło do bazy danych: ");
			do
			{
				key = Console.ReadKey(true);
				if (key.Key != ConsoleKey.Backspace)
				{
					pass += key.KeyChar;
					Console.Write("*");
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
					{
						pass = pass.Substring(0, (pass.Length - 1));
						Console.Write("\b\b");
					}
				}
			} while (key.Key != ConsoleKey.Enter);

			// string connStr = "server=;user=;database=;password="
			string connStr = "server=" + host + ";user=" + username + ";database=" + database + ";password=" + pass;
			MySqlConnection conn = new MySqlConnection(connStr);

			try
			{
				Console.WriteLine("\nConnecting to MySQL");
				conn.Open();
			}
			catch(Exception ex) {
				Console.Write (ex.ToString ());
				conn.Close();
				Environment.Exit (0);
			}

			string command;

			Console.WriteLine ("Połączono do bazy {0}", database);
			Console.WriteLine ("Wpisz Q żeby wyjsc");

			do {
				Console.Write("> ");
				command = Console.ReadLine();

				try {
					MySqlCommand cmd = new MySqlCommand(command, conn);
					MySqlDataReader rdr = cmd.ExecuteReader();
					for(int i = 0; i < rdr.FieldCount; i++) {
						rdr.Read();
						Console.WriteLine("{0}", rdr[i]);
					}
					rdr.Close();
				} catch(Exception ex) {
					Console.WriteLine(ex.ToString());
				}
			} while(command != "Q");

			conn.Close ();
		}
	}
}