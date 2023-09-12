using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlClient
{
    public class ArgOptions
    {
        public string DatabaseName { get; set; }
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Query { get; set; }
    }
}
