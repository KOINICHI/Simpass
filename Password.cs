using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpass
{
	class Password
	{
		public string name;
		public string username;
		public string password;
		public string website;
		public string note;
		public List<string> tags;
		
		public DateTime created;
		public DateTime modified;

		public Guid id;

		public Password()
		{
			name = "Name";
			username = "Username";
			password = "Password";
			website = "https://";
			note = "Note";
			tags = new List<string>();
			
			created = DateTime.Now;
			modified = DateTime.Now;

			id = Guid.NewGuid();
		}
	}
}
