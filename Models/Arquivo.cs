using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotaSys.Models
{
    public class Arquivo
    {
        public XDocument ArquivoXml { get; set; }
        public string Id { get; set; }

        public Arquivo(XDocument arquivoXml, String id)
        {
            ArquivoXml = arquivoXml;
            Id = id;
        }
    }
}