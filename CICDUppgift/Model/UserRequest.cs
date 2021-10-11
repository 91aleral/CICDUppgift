using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDUppgift.Model
{
    public class UserRequest
    {
        public string userName { get; set; }
        public string role { get; set; }
        public string change { get; set; }
        public string currentValue { get; set; }
        public string newValue { get; set; }
    }
}
