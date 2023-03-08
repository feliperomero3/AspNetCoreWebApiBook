using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MyBoardGameList.Entities;
using MyBoardGameList.Models.Csv;

namespace MyBoardGameList.Data;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly IHostEnvironment _environment;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;

    public ApplicationDbContextInitializer(
        ApplicationDbContext context,
        IHostEnvironment environment,
        ILogger<ApplicationDbContextInitializer> logger)
    {
        _context = context;
        _environment = environment;
        _logger = logger;
    }

    internal void Initialize()
    {
        _context.Database.EnsureCreated();

        if (_context.BoardGames.Any())
        {
            _logger.LogInformation("Database has already been seeded.");

            return;
        }

        var path = Path.Combine(_environment.ContentRootPath, "Data/Source/bgg_dataset_test.csv");
        var config = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
        {
            Delimiter = ";",
        };

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        var boardGames = new HashSet<BoardGame>();
        var mechanics = new HashSet<Mechanic>();
        var domains = new HashSet<Domain>();
        var boardGameMechanics = new HashSet<BoardGameMechanic>();
        var boardGameDomains = new HashSet<BoardGameDomain>();

        var skippedRows = 0;

        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            var now = DateTime.Now;
            var record = csv.GetRecord<BggRecord>();

            if (record == null || string.IsNullOrWhiteSpace(record.Name))
            {
                skippedRows++;
                continue;
            }

            var boardGame = new BoardGame
            {
                Name = record.Name,
                Year = record.YearPublished ?? 0,
                MinPlayers = record.MinPlayers ?? 0,
                MaxPlayers = record.MaxPlayers ?? 0,
                PlayTime = record.PlayTime ?? 0,
                MinAge = record.MinAge ?? 0,
                UsersRated = record.UsersRated ?? 0,
                RatingAverage = record.RatingAverage ?? 0,
                BGGRank = record.BGGRank ?? 0,
                ComplexityAverage = record.ComplexityAverage ?? 0,
                OwnedUsers = record.OwnedUsers ?? 0,
                CreatedDate = now,
                LastModifiedDate = now
            };

            boardGames.Add(boardGame);

            if (!string.IsNullOrWhiteSpace(record.Mechanics))
            {
                var entries = record.Mechanics
                    .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Distinct(StringComparer.InvariantCultureIgnoreCase);

                foreach (var entry in entries)
                {
                    var mechanic = new Mechanic(entry);

                    mechanics.Add(mechanic);
                    boardGameMechanics.Add(new BoardGameMechanic { BoardGame = boardGame, Mechanic = mechanic });
                }
            }

            if (!string.IsNullOrWhiteSpace(record.Domains))
            {
                var entries = record.Domains
                    .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Distinct(StringComparer.InvariantCultureIgnoreCase);

                foreach (var entry in entries)
                {
                    var domain = new Domain(entry);

                    domains.Add(domain);
                    boardGameDomains.Add(new BoardGameDomain { BoardGame = boardGame, Domain = domain });
                }
            }
        }

        _context.AddRange(boardGames);
        _context.AddRange(mechanics);
        _context.AddRange(domains);

        _context.SaveChanges();

        _logger.LogInformation("Added {boardgames} board games.", boardGames.Count);
        _logger.LogInformation("Added {mechanics} mechanics.", mechanics.Count);
        _logger.LogInformation("Added {domains} domains.", domains.Count);
        _logger.LogInformation("Database has been seeded successfully.");
        _logger.LogInformation("Skipped {skippedRows} invalid row(s).", skippedRows);
    }
}
