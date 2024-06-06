using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.IO;
using QuestPDF.Infrastructure;
using Test.Interfaces;
using System.Xml.Linq;

namespace Test.Services
{
    public class GeneratePdfService : IGeneratePdfService
    {
        private IReportesDiversosService _reportesDiversService;
        public GeneratePdfService(IReportesDiversosService service)
        {
            _reportesDiversService = service;
        }

        public async Task<Document> Uchalamama()
        {
            var reportes = await _reportesDiversService.GetReportesDiversosAsync();

            var imageBytes = File.ReadAllBytes(@"C:\Users\skimo\OneDrive\Escritorio\logo1.png");
            var imageBytes2 = File.ReadAllBytes(@"C:\Users\skimo\OneDrive\Escritorio\logoPC.png");


            var report = Document.Create(container =>
            {
                foreach (var reporte in reportes)
                {
                    var fecha = reporte.FechaCreacion.ToString().Substring(0, 10);
                    var hora = reporte.FechaCreacion.ToString().Substring(11);

                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header().Row(row =>
                        {
                            row.ConstantItem(120).Image(imageBytes).FitArea();

                            row.ConstantItem(155).TranslateX(230).Image(imageBytes2).FitArea();
                        });

                        page.Content().PaddingVertical(22).Column(column =>
                        {
                            column.Spacing(1);
                            column.Item().AlignCenter().Height(50).Text("FICHA INFORMATIVA DEL SISTEMA DE INFORMACIÓN TELEFÓNICA").Bold().FontSize(13);


                            //Aqui se separa Folio de cliente.nombre, cada string tiene su propio estilo..
                            column.Item().Text(text =>
                            {
                                text.Span("FOLIO: ").Bold();
                                text.Span($"{reporte.IdReporteDiverso}".ToUpper());
                            });
                            column.Item().Text(text =>
                            {
                                text.Span("FECHA: ").Bold();
                                text.Span(fecha.ToUpper());
                            });
                            column.Item().Text(text =>
                            {
                                text.Span("HORA DE INICIO: ").Bold();
                                text.Span(hora.ToUpper());
                            });
                            column.Item().Text(text =>
                            {
                                text.Span("TIPO DE EMERGENCIA: ").Bold();
                                text.Span(reporte.CategoriaReporte.ToUpper());
                            });
                            column.Item().Text(text =>
                            {
                                text.Span("CAUSA O POSIBLE CAUSA: ").Bold();
                                text.Span($"Sin proporcionar".ToUpper());
                            });

                            column.Item().Text(text =>
                            {
                                text.Span("LOCALIZACIÓN: ").Bold();
                                text.Span($"{reporte.Domicilio.ToUpper()}, {reporte.Colonia.ToUpper()}");
                            });

                            column.Item().Text($"ELEMENTOS Y UNIDADES EN EL EVENTO: \n").Bold();


                            column.Item().Text($"           • CRUZ ROJA");
                            column.Item().Text($"           • PM");

                            //foreach (var elemento in cliente.Apellido)
                            //{
                            //    column.Item().Text($"           • {elemento}");
                            //}

                            column.Item().Text(text =>
                            {
                                text.Span("\nINFORMATIVO: ").Bold();
                                text.Span(reporte.Asunto.ToUpper());
                            });

                            column.Item().Text(text =>
                            {
                                text.Span("\nSEGUIMIENTO: ").Bold();
                                text.Span(reporte.Seguimiento.ToUpper());
                            });

                            column.Item().Text(text =>
                            {
                                text.Span("\nESTATUS: ").Bold();
                                text.Span($"{(reporte.Estatus == true ? "ABIERTO" : "CERRADO").ToUpper()}");
                            });

                            page.Footer().AlignCenter().TranslateY(-30).Text(t =>
                            {
                                t.Span("ELABORADO POR:").Bold();
                                t.Span($"\n {reporte.Reporta.ToUpper()}");
                            });
                        });
                    });
                }
            });

            return report;
        }

        Task<Document> IGeneratePdfService.GeneratePdfQuest()
        {
            throw new NotImplementedException();
        }

        //public async Task<Document> GeneratePdfQuest()
        //{
        //    var clientes = await _clientesService.GetClientesAsync();

        //    var report = Document.Create(container =>
        //    {
        //        foreach (var cliente in clientes)
        //        {
        //            container.Page(page =>
        //            {
        //                page.Margin(50);
        //                page.Size(PageSizes.A4);
        //                page.PageColor(Colors.White);
        //                page.DefaultTextStyle(x => x.FontSize(12));

        //                page.Header().AlignCenter()
        //                    .Text($"Factura : 80025-63")
        //                    .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

        //                page.Content().Padding(25)
        //                    .Table(table =>
        //                    {
        //                        table.ColumnsDefinition(columns =>
        //                        {
        //                            columns.RelativeColumn();
        //                            columns.RelativeColumn();
        //                            columns.RelativeColumn();
        //                        });

        //                        table.Header(header =>
        //                        {
        //                            header.Cell().Text("Nombre");
        //                            header.Cell().Text("Apellido");
        //                            header.Cell().Text("Email");
        //                        });

        //                        table.Cell().Text(cliente.Nombre);
        //                        table.Cell().Text(cliente.Apellido);
        //                        table.Cell().Text(cliente.Email);
        //                    });
        //            });
        //        }
        //    });

        //    return report;
        //}

        //AQUI OBTIENE TODA LA INFO EN UNA SOLA PAGINA
        //    public async Task<Document> GeneratePdfQuestV2()
        //    {

        //        var clientes = await _clientesService.GetClientesAsync();

        //        var report = Document.Create(container =>
        //        {
        //            container.Page(page =>
        //            {
        //                page.Margin(50);
        //                page.Size(PageSizes.A4);
        //                page.PageColor(Colors.White);
        //                page.DefaultTextStyle(x => x.FontSize(12));

        //                page.Header().AlignCenter()
        //                .Text("Factura : 80025-63")
        //                .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

        //                page.Content().Padding(25)
        //                .Table(table =>
        //                {
        //                    table.ColumnsDefinition(columns =>
        //                    {
        //                        columns.RelativeColumn();
        //                        columns.RelativeColumn();
        //                        columns.RelativeColumn();
        //                    });



        //                    table.Header(header =>
        //                    {
        //                        header.Cell().Text("Nombre");
        //                        header.Cell().Text("Apellido");
        //                        header.Cell().Text("Email");

        //                    });

        //                    foreach (var cliente in clientes)
        //                    {
        //                        table.Cell().Text(cliente.Nombre);
        //                        table.Cell().Text(cliente.Apellido);
        //                        table.Cell().Text(cliente.Email);
        //                    }

        //                });


        //            });


        //        });

        //        return report;
        //    }

    }   //}
}
