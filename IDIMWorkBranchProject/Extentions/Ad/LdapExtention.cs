using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Extentions.Ad
{
    public class LdapExtention
    {
        public string AdServer { get; set; }

        public string Path { get; set; }

        public DirectoryEntry Entry { get; set; }

        public LdapExtention()
        {
            AdServer = DefaultData.AdServer;
            Path = $"LDAP://{DefaultData.AdServer}";
            Entry = new DirectoryEntry(Path, DefaultData.AdUsername, DefaultData.AdPassword);
        }

        public LdapExtention(string adServer)
        {
            AdServer = adServer;
            Path = $"LDAP://{adServer}";
            Entry = new DirectoryEntry(Path, DefaultData.AdUsername, DefaultData.AdPassword);
        }

        public LdapExtention(string adServer, string adUsername, string adPassword)
        {
            AdServer = adServer;
            Path = $"LDAP://{adServer}";
            Entry = new DirectoryEntry(Path, adUsername, adPassword);
        }

        public bool IsAuthenticated()
        {
            var authenticated = false;

            try
            {
                var nativeObject = Entry.NativeObject;
                authenticated = true;
            }
            catch (Exception ex)
            {
                // ignored
                throw ex;
            }

            return authenticated;
        }
    }
}