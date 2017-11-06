using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpass
{
	class Manager
	{
		public List<Password> passwords;

		private Config config;

		public Manager()
		{
			passwords = new List<Password>();
			config = Config.Instance;
			loadEverything();
		}

		~Manager()
		{
			saveEverything();
		}

		public string GeneratePassword(Config config)
		{
			StringBuilder sb = new StringBuilder();

			List<char> charPool = new List<char>();
			for (int i = Char.MinValue; i <= Char.MaxValue; i++)
			{
				char c = Convert.ToChar(i);
				if (!Char.IsControl(c) && !config.disallowedSymbols.Contains(c))
				{
					charPool.Add(c);
				}
			}

			Random rand = new Random();
			for (int i = 0; i < config.numberOfSymbols; i++)
			{
				int idx = rand.Next(charPool.Count);
				sb.Append(charPool[idx]);
			}

			return sb.ToString();
		}
		
		public void addPassword(Password password)
		{
			passwords.Add(password);
		}

		public void deletePassword(Password password)
		{
			passwords.Remove(password);
		}

		private void loadEverything()
		{
			config.loadConfig();
			passwords = config.loadPasswords();
		}

		private void saveEverything()
		{
			config.saveConfig();
			config.savePasswords(passwords);
		}

	}
}
