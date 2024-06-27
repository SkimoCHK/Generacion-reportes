using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using Test.Interfaces;
using Test.Services;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesDiversosController : ControllerBase
    {
        private readonly IReportesDiversosService _reportesDiversosService;
        private readonly IGeneratePdfService _generatePdfService;
        private readonly IGenerateExcelService _generateExcelService;
        public ReportesDiversosController(IReportesDiversosService service, IGeneratePdfService generatePdfService, IGenerateExcelService serviceExcel) 
        {
            _reportesDiversosService = service;
            _generatePdfService = generatePdfService;
            _generateExcelService = serviceExcel;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reports = await _reportesDiversosService.GetReportesDiversosAsync();
            return Ok(reports);
        }
        [HttpGet("reportes-pdf")]
        public async Task<IActionResult> GetReportesPdf()
        {
            var report = await _generatePdfService.Uchalamama();
            byte[] pdfBytes = report.GeneratePdf();
            return File(pdfBytes, "application/pdf", "archivoxd");
        }
        [HttpGet]
        [Route("reportes-excel")]
        public async Task<IActionResult> GetReporteExcel()
        {
            var reporte = _generateExcelService.CrearExcel();
            string nameFile = "damianxd.xlsx";
            return File(reporte, "application/pdf", nameFile);
        }
       

    }
}
