using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaCorreos.Models
{
    public class ModelEmail
    {
        public string User { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string Host { get; set; }
        public bool EnableSsl { get; set; }

        public bool DefaultCredentials { get; set; }
    }
}
