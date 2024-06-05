using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Test.Interfaces;

namespace Test.Services
{
    public class GeneratePdfService : IGeneratePdfService
    {
        IClientesService _clientesService;
        public GeneratePdfService(IClientesService clientesService) 
        {
            _clientesService = clientesService;
        }

        public async Task<Document> Uchalamama()
        {
            var clientes = await _clientesService.GetClientesAsync();

            var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\skimo\OneDrive\Escritorio\logo1.png");


            var report = Document.Create(container =>
            {
                foreach (var cliente in clientes)
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header().Row(row =>
                        {
                            row.ConstantItem(100).Image(imageBytes, ImageScaling.FitWidth);
                            row.RelativeItem().Column(column =>
                            {
                                column.Item().AlignCenter().Text("PROTECCIÓN CIVIL SONORA").FontSize(20).Bold();
                                column.Item().AlignCenter().Text("FICHA INFORMATIVA DEL SISTEMA DE INFORMACIÓN TELEFÓNICA").Bold().FontSize(16);
                            });
                        });

                        page.Content().PaddingVertical(10).Column(column =>
                        {
                            column.Item().Text($"FOLIO: {cliente.Nombre}").Bold();
                            column.Item().Text($"FECHA: {cliente.Email}").Bold();
                            column.Item().Text($"HORA DE INICIO: {cliente.Email}").Bold();
                            column.Item().Text($"TIPO DE EMERGENCIA: {cliente.Email}").Bold();
                            column.Item().Text($"CAUSA O POSIBLE CAUSA: {cliente.Email}").Bold();
                            column.Item().Text($"LOCALIZACIÓN: {cliente.Email}").Bold();
                            column.Item().Text($"ELEMENTOS Y UNIDADES EN EL EVENTO:").Bold();

                            foreach (var elemento in cliente.Apellido)
                            {
                                column.Item().Text($"• {elemento}");
                            }

                            column.Item().Text($"INFORMATIVO: {cliente.Nombre}").Bold();
                            column.Item().Text($"{cliente.Nombre}");

                            column.Item().Text($"SEGUIMIENTO: {cliente.Nombre}").Bold();
                            column.Item().Text($"{cliente.Nombre}");

                            column.Item().Text($"ESTATUS: {cliente.Nombre}").Bold();

                            column.Item().AlignCenter().Text($"ELABORADO POR: {cliente.Nombre}").Bold();
                        });
                    });
                }
            });

            return report;
        }

        public async Task<Document> GeneratePdfQuest()
        {
            var clientes = await _clientesService.GetClientesAsync();

            var report = Document.Create(container =>
            {
                foreach (var cliente in clientes)
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header().AlignCenter()
                            .Text($"Factura : 80025-63")
                            .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                        page.Content().Padding(25)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Nombre");
                                    header.Cell().Text("Apellido");
                                    header.Cell().Text("Email");
                                });

                                table.Cell().Text(cliente.Nombre);
                                table.Cell().Text(cliente.Apellido);
                                table.Cell().Text(cliente.Email);
                            });
                    });
                }
            });

            return report;
        }

        //AQUI OBTIENE TODA LA INFO EN UNA SOLA PAGINA
        public async Task<Document> GeneratePdfQuestV2()
        {

            var clientes = await _clientesService.GetClientesAsync();

            var report = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().AlignCenter()
                    .Text("Factura : 80025-63")
                    .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                    page.Content().Padding(25)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });



                        table.Header(header =>
                        {
                            header.Cell().Text("Nombre");
                            header.Cell().Text("Apellido");
                            header.Cell().Text("Email");

                        });

                        foreach (var cliente in clientes)
                        {
                            table.Cell().Text(cliente.Nombre);
                            table.Cell().Text(cliente.Apellido);
                            table.Cell().Text(cliente.Email);
                        }

                    });


                });


            });

            return report;
        }

    }
}
