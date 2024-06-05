using QuestPDF.Fluent;

namespace Test.Interfaces
{
    public interface IGeneratePdfService
    {
        Task<Document> GeneratePdfQuest();
    }
}
