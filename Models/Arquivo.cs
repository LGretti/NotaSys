using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using NotaSys.Models.Enums;

namespace NotaSys.Models
{
    public class Arquivo
    {
        public String ArquivoXml { get; set; }

        public String Id { get; private set; }

        public int serie { get; private set; }
        public int nNf { get; private set; }

        public DateTime DEmissao { get; private set; }
        public int UnidadeFederativa { get; private set; }
        public string Cnpj { get; private set; }

        public Arquivo(String arquivoXml) {
            ArquivoXml = arquivoXml;

            XmlReader xmlReader = XmlReader.Create(new StringReader(arquivoXml));

            GravaCampos(xmlReader);
        }

        public void GravaCampos (XmlReader xmlReader) {
            xmlReader.MoveToContent();
            xmlReader.ReadToDescendant("infNFe");
            Id = xmlReader.GetAttribute("Id").Substring(3, 44);

            xmlReader.MoveToContent();
            
            xmlReader.ReadToFollowing("cUF");
            UnidadeFederativa = Convert.ToInt32(xmlReader.ReadElementContentAsString());

            xmlReader.ReadToFollowing("serie");
            serie = xmlReader.ReadElementContentAsInt();

            xmlReader.ReadToFollowing("nNF");
            nNf = xmlReader.ReadElementContentAsInt();

            xmlReader.ReadToFollowing("dhEmi");
            DEmissao = xmlReader.ReadElementContentAsDateTime();

            Cnpj = Id.Substring(9, 14);
        }
    }
}