using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.AcroForms;
using PdfSharpCore.Pdf.IO;
using System.IO;
using System.Threading.Tasks;
using Test.Interfaces;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClientesService _service;

        public ClienteController(IClientesService clientesService)
        {
            _service = clientesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _service.GetClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _service.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet("{id}/export-pdf")]
        public async Task<IActionResult> ExportToPdf(int id)
        {
            var cliente = await _service.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            var templatePath = @"C:\Users\skimo\OneDrive\Escritorio\EjemploV3.pdf";
            var outputPath = $"Cliente-{cliente.ClienteID}.pdf";

            using (var existingPdf = PdfReader.Open(templatePath, PdfDocumentOpenMode.Modify))
            {
                var form = existingPdf.AcroForm;
                form.Fields["Nombre"].Value = new PdfString(cliente.Nombre);
                form.Fields["Apellido"].Value = new PdfString(cliente.Apellido);
                form.Fields["Email"].Value = new PdfString(cliente.Email);

                //if (form.Elements.ContainsKey("Nombre"))
                //{
                //    form.Fields["Nombre"].Value = new PdfString(cliente.Nombre);
                //}

                //if (form.Elements.ContainsKey("Apellido"))
                //{
                //    form.Fields["Apellido"].Value = new PdfString(cliente.Nombre);
                //}

                //if (form.Elements.ContainsKey("Nombre_2"))
                //{
                //    form.Fields["Nombre_2"].Value = new PdfString(cliente.Apellido);
                //}

                //if (form.Elements.ContainsKey("Email"))
                //{
                //    form.Fields["Email"].Value = new PdfString(cliente.Email);
                //}

                existingPdf.Save(outputPath);
            }

            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(outputPath, FileMode.Open))
            {
                await fileStream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            return File(memoryStream, "application/pdf", outputPath);
        }
    }
}
