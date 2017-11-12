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
		private List<char> symbolPool;
		private List<char> alphabetPool;
		private List<char> numberPool;
		private List<char> alphaNumericPool;

		public Manager()
		{
			passwords = new List<Password>();
			config = Config.Instance;
			symbolPool = new List<char> ("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
			alphabetPool = new List<char> ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
			numberPool = new List<char> ("0123456789");
			alphaNumericPool = new List<char> ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
			loadEverything();
		}

		~Manager()
		{
			saveEverything();
		}

		public string GeneratePassword()
		{
			Random rand = new Random();

			List<char> filteredSymbolPool = new List<char>();
			foreach (char c in symbolPool)
			{
				if (!Char.IsControl(c) && !config.disallowedSymbols.Contains(c))
				{
					filteredSymbolPool.Add(c);
				}
			}
			List<char> unshuffled = new List<char>();
			for (int i = 0; i < config.numberOfSymbols; i++)
			{
				int idx = rand.Next(symbolPool.Count);
				unshuffled.Add(filteredSymbolPool[idx]);
			}
			for (int i = 0; i < config.passwordLength - config.numberOfSymbols; i++)
			{
				int idx = rand.Next(alphaNumericPool.Count);
				unshuffled.Add(alphaNumericPool[idx]);
			}

			return String.Join("", unshuffled.OrderBy(x => rand.Next()));
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
