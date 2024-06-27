using Jor.ExcelEasy;
using Jor.ExcelEasy.Celda.Valor;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Test.Interfaces;

namespace Test.Services
{
    public class GenerateExcelService : IGenerateExcelService
    {
        public IReportesDiversosService _reportes;
        public GenerateExcelService(IReportesDiversosService reportes) 
        {
        _reportes = reportes;
        }

        public byte[] CrearExcel()
        {
            Jor.ExcelEasy.ExcelLibro.CrearLibroYHoja("Reporte excel", out ISheet hojaExcel, out XSSFWorkbook libroExcel);

            var filauno = hojaExcel.CrearFila(1).ColocarValor("A", "q onda lala como estan");

            hojaExcel.CrearFila(2).ColocarValor("A", "Valor en 2-A");

            return libroExcel.ConvertirArchivoExcelABytes();
        }
    }
}
