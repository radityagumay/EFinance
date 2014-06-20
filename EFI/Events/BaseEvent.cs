using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFI.Events
{
    public class BaseEvent : CompositePresentationEvent<BaseArgs>
    {
        public BaseEvent()
        {
        }
    }

    public class BaseArgs : EventArgs
    {
        public string Name { get; set; }

        public object Args { get; set; }

        public BaseArgs(string name)
        {
            this.Name = name;
        }

        public BaseArgs(string name, object args)
        {
            this.Name = name;
            this.Args = args;
        }
    }
}
