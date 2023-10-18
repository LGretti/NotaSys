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
        public DateTime DEmissao { get; private set; }

        /*//construtor com todas as propriedades
        public Arquivo(XDocument arquivoXml, String id, UfEnum unidadeFederatuva, string cnpj, DateTime dEmissao) {
            ArquivoXml = arquivoXml;
            Id = id;
            UnidadeFederativa = unidadeFederatuva;
            Cnpj = cnpj;
            DEmissao = dEmissao;
        }*/

        public Arquivo(XDocument arquivoXml) {
            ArquivoXml = arquivoXml;

            GravaCampos();
        }

        public void GravaCampos () {
            //Recupera a tag infNFe
            XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
            XElement infNFe = ArquivoXml.Element(ns + "nfeProc").Element(ns + "NFe").Element(ns + "infNFe");

            //Pela tag busca o Atributo Id e grava em uma vari�vel caso haja o valor
            if (infNFe != null) {
                XAttribute idAttr = infNFe.Attribute("Id");

                if (idAttr != null) {
                    Id = idAttr.Value;
                }
            }

            DateTime.TryParse (
                ArquivoXml
                .Element(ns + "nfeProc")
                .Element(ns + "NFe")
                .Element(ns + "infNFe")
                .Element(ns + "ide")
                .Element(ns + "dhEmi")
                .Value, out DateTime DEmissao);

            UfEnum.TryParse(Id.Substring(3, 2), out UfEnum UnidadeFederativa);

            Cnpj = Id.Substring(9, 14);
        }
    }
}