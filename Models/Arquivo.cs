using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using NotaSys.Models.Enums;

namespace NotaSys.Models
{
    public class Arquivo
    {
        public XDocument ArquivoXml { get; set; }

        public string Id { get; private set; }
        public UfEnum UnidadeFederativa { get; private set; }
        public string Cnpj { get; private set; }

        public Arquivo(XDocument arquivoXml) {
            ArquivoXml = arquivoXml;

            GravaCampos();
        }

        public void GravaCampos () {
            //Recupera a tag infNFe
            XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
            XElement infNFe = ArquivoXml.Element(ns + "nfeProc").Element(ns + "NFe").Element(ns + "infNFe");

            //Pela tag busca o Atributo Id e grava em uma variável caso haja o valor
            if (infNFe != null) {
                XAttribute idAttr = infNFe.Attribute("Id");

                if (idAttr != null) {
                    Id = idAttr.Value;
                }
            }

             UfEnum.TryParse(Id.Substring(3, 2), out UfEnum UnidadeFederativa);

            Cnpj = Id.Substring(9, 14);
        }
    }
}