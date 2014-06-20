using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EFI.DataParse
{
    [DataContract]
    public class EconomyDataParse
    {
        [DataMember(Name = "sunnah")]
        public ObservableCollection<sunnah> sunnah { get; set; }
    }

    [DataContract]
    public class sunnah
    {
        [DataMember(Name = "list_id")]
        public string list_id { get; set; }

        [DataMember(Name = "en")]
        public string english { get; set; }

        [DataMember(Name = "ar")]
        public string arab { get; set; }
    }
}
