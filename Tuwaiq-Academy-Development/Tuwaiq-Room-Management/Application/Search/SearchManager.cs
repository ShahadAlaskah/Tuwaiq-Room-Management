using Application.Dto;
using Application.Interfaces;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Mapster;
using Microsoft.AspNetCore.Hosting;

namespace Application.Search;

public class SearchManager : ISearchManager
{
    private static FSDirectory _directory = null!;
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViewRenderService _viewRenderService;

    public SearchManager(IWebHostEnvironment env, IUnitOfWork unitOfWork, IViewRenderService viewRenderService)
    {
        _env = env;
        _unitOfWork = unitOfWork;
        _viewRenderService = viewRenderService;
    }

    private FSDirectory Directory
    {
        get
        {
            if (_directory != null)
            {
                return _directory;
            }

            var info = System.IO.Directory.CreateDirectory(LuceneDir);
            return _directory = FSDirectory.Open(info);
        }
    }

    private string LuceneDir => Path.Combine(_env.ContentRootPath, "Storage", "Search");

    public void AddToIndex(params Searchable[] searchables)
    {
        DeleteFromIndex(searchables);
        UseWriter(x =>
        {
            foreach (var searchable in searchables)
            {
                var doc = new Document();
                foreach (var field in searchable.GetFields())
                {
                    doc.Add(field);
                }

                x.AddDocument(doc);
            }
        });
    }

    private void UseWriter(Action<IndexWriter> action)
    {
        using (var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48))
        {
            using (var writer = new IndexWriter(Directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer)))
            {
                action(writer);
                writer.Commit();
            }
        }
    }


    public void DeleteFromIndex(params Searchable[] searchables)
    {
        UseWriter(x =>
        {
            foreach (var searchable in searchables)
            {
                x.DeleteDocuments(
                    new Term(Searchable.FieldStrings[Searchable.Field.Id], searchable.Id));
            }
        });
    }

    public void Clear()
    {
        UseWriter(x => x.DeleteAll());
    }

    public void InitAll()
    {
        var assetTypes = _unitOfWork.AssetTypes?.GetAll();
        if (assetTypes != null)
            foreach (var item in assetTypes.ToList())
            {
                var searchable = item.Adapt<AssetTypeDto>();
                var insert = new SearchableAssetType(searchable!, _viewRenderService);
                if (insert != null) AddToIndex(insert);
            }

        var assets = _unitOfWork.Assets?.GetAll();
        if (assets != null)
            foreach (var item in assets.ToList())
            {
                var searchable = item.Adapt<AssetDto>();
                var insert = new SearchableAsset(searchable!, _viewRenderService);
                if (insert != null) AddToIndex(insert);
            }

        var rooms = _unitOfWork.Rooms?.GetAll();
        if (rooms != null)
            foreach (var item in rooms.ToList())
            {
                var searchable = item.Adapt<RoomDto>();
                var insert = new SearchableRoom(searchable!, _viewRenderService);
                if (insert != null) AddToIndex(insert);
            }

        var buildings = _unitOfWork.Buildings?.GetAll();
        if (buildings != null)
            foreach (var item in buildings.ToList())
            {
                var searchable = item.Adapt<BuildingDto>();
                var insert = new SearchableBuilding(searchable!, _viewRenderService);
                if (insert != null) AddToIndex(insert);
            }

        var floors = _unitOfWork.Floors?.GetAll();
        if (floors != null)
            foreach (var item in floors.ToList())
            {
                var searchable = item.Adapt<FloorDto>();
                var insert = new SearchableFloor(searchable!, _viewRenderService);
                if (insert != null) AddToIndex(insert);
            }

        var roomTypes = _unitOfWork.RoomTypes?.GetAll();
        if (roomTypes != null)
            foreach (var item in roomTypes.ToList())
            {
                var searchable = item.Adapt<RoomTypeDto>();
                var insert = new SearchableRoomType(searchable!, _viewRenderService);
                if (insert != null) AddToIndex(insert);
            }

    }

    // public void Clear(AssetTypeId AssetTypeId)
    // {
    //     UseWriter(x =>
    //     {
    //         x.DeleteDocuments(
    //             new Term(Searchable.FieldStrings[Searchable.Field.AssetTypeId], AssetTypeId.Value.ToString()));
    //     });
    // }

    public SearchResultCollection Search(string searchQuery, int hitsStart, int hitsStop, string[] fields)
    {
        if (string.IsNullOrEmpty(searchQuery))
        {
            return new SearchResultCollection();
        }

        const int hitsLimit = 20;
        SearchResultCollection results;
        using (var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48))
        {
            using (var reader = DirectoryReader.Open(Directory))
            {
                var searcher = new IndexSearcher(reader);
                var parser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48, fields, analyzer);
                var query = parser.Parse(QueryParserBase.Escape(searchQuery.Trim()));

                var finalQuery = new BooleanQuery();
                finalQuery.Add(query, Occur.SHOULD);
                fields.ToList().ForEach(field =>
                {
                    QueryParserBase.Escape(searchQuery.Trim()).Split(' ').ToList().ForEach(search =>
                    {
                        finalQuery.Add(new FuzzyQuery(new Term(field,  search + "~")), Occur.SHOULD);
                    });
                });

                // var parser2 = new MultiFieldQueryParser(LuceneVersion.LUCENE_48, Searchable.AnalyzedFields.Values.ToArray(), analyzer);

                // QueryParser AssetTypeIdQueryParser = new QueryParser(LuceneVersion.LUCENE_48, Searchable.AnalyzedFields.Values.ToArray()[0], analyzer);
                // Query AssetTypeIdQuery = AssetTypeIdQueryParser.Parse(AssetTypeId.ToString());

                // BooleanQuery finalQuery = new BooleanQuery();
                // // MUST implies that the keyword must occur.
                // //finalQuery.Add(query, Occur.MUST);
                // //finalQuery.Add(query, Occur.MUST);
                // //finalQuery.Add(AssetTypeIdQuery, Occur.MUST);
                // finalQuery.Add(new TermQuery(new Term("AssetTypeId", AssetTypeId.ToString())), Occur.MUST);
                //
                // // finalQuery.Add(query, Occur.MUST);

                // fields.ToList().ForEach(field => { QueryParserBase.Escape(searchQuery.Trim()).Split(' ').ToList().ForEach(search => { finalQuery.Add(new FuzzyQuery(new Term(field, search)), Occur.MUST); }); });

                var hits = searcher.Search(finalQuery, null, hitsLimit, Sort.RELEVANCE).ScoreDocs;
                results = new SearchResultCollection
                {
                    Count = hits.Length,
                    Data = hits.Where((x, i) => i >= hitsStart && i < hitsStop)
                        .Select(x => new SearchResult(searcher.Doc(x.Doc)))
                        .ToList()
                };
            }
        }

        return results;
    }
}