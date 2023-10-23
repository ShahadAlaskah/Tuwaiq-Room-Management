using Lucene.Net.Documents;

namespace Application.Search;

public class SearchResult
{
    private readonly Document _doc;

    public SearchResult(Document doc)
    {
        _doc = doc;
        // Description = doc.Get("Description");
    }

    // public string Description { get; set; }
    public string DescriptionPath { get; set; } = null!;

    public string LinkHref { get; set; } = null!;

    public string LinkText { get; set; } = null!;
    public string Id { get; set; } = null!;
    public string Description { get; set; } = null!;

    public void Parse(Action<Document> parseAction)
    {
        parseAction(_doc);
    }
}

// public class SearchEmployeeWriter
// {
//     const LuceneVersion lv = LuceneVersion.LUCENE_48;
//     Lucene.Net.Analysis.Analyzer analyzer = new StandardAnalyzer(lv);
//     private RAMDirectory directory;
//     private IndexWriter writer;
//
//     public SearchEmployeeWriter()
//     {
//         directory = new RAMDirectory();
//         var config = new IndexWriterConfig(lv, analyzer);
//         writer = new IndexWriter(directory, config);
//     }
//
//     public void Write(Employee employee)
//     {
//         writer.AddDocument(new Document
//         {
//             employee.
//         });
//     }
// }