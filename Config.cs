using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpass
{
	class PasswordConfig
	{

	}
	
	class Config
	{
		private static Config instance;
		private string configDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Simpass/";
		private string configFilename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Simpass/SimpassConf.ini";
		private string passwordFilename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Simpass/Simpass.ini";

		private Config() { }

		public static Config Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Config();
				}
				return instance;
			}
		}

		public int passwordLength = 16;
		public int numberOfSymbols = 4;
		public string disallowedSymbols = "";
		public string masterPasswordHash = "11";

		public void saveConfig()
		{
			if (!Directory.Exists(configDir))
			{
				Directory.CreateDirectory(configDir);
			}

			Dictionary<string, string> jsonCollection = new Dictionary<string, string>();
			jsonCollection.Add("passwordLength", passwordLength.ToString());
			jsonCollection.Add("numberOfSymbols", numberOfSymbols.ToString());
			jsonCollection.Add("disallowedSymbols", disallowedSymbols);
			jsonCollection.Add("masterPasswordHash", masterPasswordHash);
			//jsonCollection.Add();

			using (StreamWriter file = File.CreateText(configFilename))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, jsonCollection);
			}
		}
		public void loadConfig()
		{
			if (!File.Exists(configFilename))
			{
				saveConfig();
			}

			Dictionary<string, string> jsonCollection = new Dictionary<string, string>();

			using (StreamReader sr = File.OpenText(configFilename))
			{
				JsonSerializer serializer = new JsonSerializer();
				jsonCollection = (Dictionary<string, string>)serializer.Deserialize(sr, typeof(Dictionary<string, string>));
			}

			passwordLength = Int32.Parse(jsonCollection["passwordLength"]);
			numberOfSymbols = Int32.Parse(jsonCollection["numberOfSymbols"]);
			disallowedSymbols = jsonCollection["disallowedSymbols"];
			masterPasswordHash = jsonCollection["masterPasswordHash"];
			// = jsonCollection[""];
		}

		public List<Password> loadPasswords()
		{
			if (!File.Exists(passwordFilename))
			{
				savePasswords(new List<Password>());
			}

			List<Password> passwords = new List<Password>();

			using (StreamReader sr = File.OpenText(passwordFilename))
			{
				JsonSerializer serializer = new JsonSerializer();
				passwords = (List<Password>)serializer.Deserialize(sr, typeof(List<Password>));
			}

			return passwords;
		}

		public void savePasswords(List<Password> passwords)
		{
			if (!Directory.Exists(configDir))
			{
				Directory.CreateDirectory(configDir);
			}

			using (StreamWriter file = File.CreateText(passwordFilename))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, passwords);
			}
		}
	}
}
