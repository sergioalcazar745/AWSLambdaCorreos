using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaCorreos.Models
{
    public class ModelCorreoLamdba
    {
        public string Asunto { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }

        public List<string>? Attachments { get; set; }
    }
}
