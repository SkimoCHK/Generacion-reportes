using QuestPDF.Fluent;
using QuestPDF.Helpers;
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
