using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModel
{
    public class MessagesViewModel
    {
        public MessagesViewModel()
        {
            Messages = new List<string>();
        }

        public IList<string> Messages { get; set; }
    }
}
