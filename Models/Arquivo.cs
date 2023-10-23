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

            //int.TryParse(Id.Substring(0, 2), out int UnidadeFederativa);

            Cnpj = Id.Substring(9, 14);


            /*//Recupera a tag infNFe
            XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
            
            if (arquivoXml.Element(ns + "nfeProc").Element(ns + "NFe").Element(ns + "infNFe") == null) {

            }

            XElement? infNFe = arquivoXml.Element(ns + "nfeProc").Element(ns + "NFe").Element(ns + "infNFe");

            if (infNFe == null) { //cancelada
                infNFe = arquivoXml.Element("NFeDFe").Element(ns + "procNFe").Element(ns + "NFe").Element(ns + "infNFe");
            }

            //Pela tag busca o Atributo Id e grava em uma variavel caso haja o valor
            if (infNFe != null) {
                XAttribute idAttr = infNFe.Attribute("Id");

                if (idAttr != null) {
                    Id = idAttr.Value;
                }
            } else {
                throw new Exception("Tag infNFe não localizada. Verifique os arquivos enviados!");
            }

            DateTime.TryParse (
                arquivoXml
                .Element(ns + "nfeProc")
                .Element(ns + "NFe")
                .Element(ns + "infNFe")
                .Element(ns + "ide")
                .Element(ns + "dhEmi")
                .Value, out DateTime DEmissao);

            int.TryParse(Id.Substring(3, 2), out int UnidadeFederativa);

            Cnpj = Id.Substring(9, 14);*/
        }
    }
}