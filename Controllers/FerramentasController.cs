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
    public class FerramentasController : Controller {
        private readonly ILogger<FerramentasController> _logger;
        private readonly NotaSysContext _context;

        public FerramentasController(ILogger<FerramentasController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Upload() {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(List<IFormFile> ArquivoXml) {
            foreach (IFormFile arquivoxml in ArquivoXml) {
                //Convertendo para XML
                using (MemoryStream str = new MemoryStream()) {
                    arquivoxml.CopyToAsync(str);
                    str.Position = 0;
                    XDocument xml = XDocument.Load(str);

                    // TODO : Metodo que recebe o Id e converte para os demais dados necessarios
                    //https://www.oobj.com.br/bc/article/como-�-formada-a-chave-de-acesso-de-uma-nf-e-nfc-e-de-um-ct-e-e-um-mdf-e-281.html

                    Arquivo arquivo = new Arquivo(xml);

                    if (ModelState.IsValid) {
                        _context.Arquivos.Add(arquivo);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View("Error!");
        }
    }
}