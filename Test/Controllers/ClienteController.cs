using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
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
        private readonly IGeneratePdfService _generatePdfService;

        public ClienteController(IClientesService clientesService, IGeneratePdfService generatePdfService)
        {
            _service = clientesService;
            _generatePdfService = generatePdfService;
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

        [HttpGet("allClientes")]
        public async Task<IActionResult> GetPdf()
        {
            var report = await _generatePdfService.Uchalamama();
            byte[] pdfBytes = report.GeneratePdf();
            return File(pdfBytes, "application/pdf", "archivoxd");
        }




        //[HttpGet("{id}/export-pdf")]
        //public async Task<IActionResult> ExportToPdf(int id)
        //{
        //    var cliente = await _service.GetClienteById(id);
        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }

        //    var templatePath = @"C:\Users\skimo\OneDrive\Escritorio\EjemploV3.pdf";
        //    var outputPath = $"Cliente-{cliente.ClienteID}.pdf";

        //    using (var existingPdf = PdfReader.Open(templatePath, PdfDocumentOpenMode.Modify))
        //    {
        //        var form = existingPdf.AcroForm;
        //        form.Fields["Nombre"].Value = new PdfString(cliente.Nombre);
        //        form.Fields["Apellido"].Value = new PdfString(cliente.Apellido);
        //        form.Fields["Email"].Value = new PdfString(cliente.Email);

        //        //if (form.Elements.ContainsKey("Nombre"))
        //        //{
        //        //    form.Fields["Nombre"].Value = new PdfString(cliente.Nombre);
        //        //}

        //        //if (form.Elements.ContainsKey("Apellido"))
        //        //{
        //        //    form.Fields["Apellido"].Value = new PdfString(cliente.Nombre);
        //        //}

        //        //if (form.Elements.ContainsKey("Nombre_2"))
        //        //{
        //        //    form.Fields["Nombre_2"].Value = new PdfString(cliente.Apellido);
        //        //}

        //        //if (form.Elements.ContainsKey("Email"))
        //        //{
        //        //    form.Fields["Email"].Value = new PdfString(cliente.Email);
        //        //}

        //        existingPdf.Save(outputPath);
        //    }

        //    var memoryStream = new MemoryStream();
        //    using (var fileStream = new FileStream(outputPath, FileMode.Open))
        //    {
        //        await fileStream.CopyToAsync(memoryStream);
        //    }
        //    memoryStream.Position = 0;

        //    return File(memoryStream, "application/pdf", outputPath);
        //}



        //[HttpGet("export-all-pdf")]
        //public async Task<IActionResult> ExportAllToPdf()
        //{
        //    var clientes = await _service.GetClientesAsync();
        //    if (clientes == null || !clientes.Any())
        //    {
        //        return NotFound();
        //    }

        //    var templatePath = @"C:\Users\skimo\OneDrive\Escritorio\EjemploV3.pdf";
        //    var memoryStream = new MemoryStream();

        //    using (var outputPdf = new PdfDocument())
        //    {
        //        foreach (var cliente in clientes)
        //        {
        //            using (var templatePdf = PdfReader.Open(templatePath, PdfDocumentOpenMode.Import))
        //            {
        //                foreach (var templatePage in templatePdf.Pages)
        //                {
        //                    var page = outputPdf.AddPage(templatePage);

        //                    // Rellenar los campos del formulario con los datos del cliente
        //                    var form = templatePdf.AcroForm;
        //                    form.Fields["Nombre"].Value = new PdfString(cliente.Nombre);
        //                    form.Fields["Apellido"].Value = new PdfString(cliente.Apellido);
        //                    form.Fields["Email"].Value = new PdfString(cliente.Email);
        //                }
        //            }
        //        }

        //        outputPdf.Save(memoryStream);
        //    }

        //    memoryStream.Position = 0;
        //    return File(memoryStream, "application/pdf", "Todos_los_reportes.pdf");
        //}





    }
}
