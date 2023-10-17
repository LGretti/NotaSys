using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotaSys.Models;
using NotaSys.Context;
using System.Reflection.Metadata;
using System.Xml.XPath;

namespace NotaSys.Controllers {
    public class FerramentasController : Controller
    {
        private readonly ILogger<FerramentasController> _logger;
        private readonly NotaSysContext _context;

        public FerramentasController(ILogger<FerramentasController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(List<IFormFile> ArquivoXml) {
            foreach (IFormFile arquivoxml in ArquivoXml)
            {
                //Convertendo para XML
                using (MemoryStream str = new MemoryStream()) {
                    arquivoxml.CopyToAsync(str);
                    str.Position = 0;
                    XDocument xml = XDocument.Load(str);

                    //Recupera a tag infNFe
                    XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
                    XElement infNFe = xml.Element(ns + "nfeProc").Element(ns + "NFe").Element(ns + "infNFe");

                    //Pela tag busca o Atributo Id e grava em uma variável caso haja o valor
                    if (infNFe != null) {
                        XAttribute idAttr = infNFe.Attribute("Id");
                        if (idAttr != null) {
                            string id = idAttr.Value;
  
                            Arquivo arquivo = new Arquivo(xml, id); 
                            
                            if (ModelState.IsValid) {
                                _context.Arquivos.Add(arquivo);
                                _context.SaveChanges();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }

                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}