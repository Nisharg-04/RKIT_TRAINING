using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCheckInDomain
{
    /// <summary>
    /// Represents a daily summary of book returns
    /// </summary>
    public class DailySummary
    {

        public DateTime ProcessedAt { get; }
        public int TotalReturns { get; }
        public Dictionary<BookCondition, int> CountByCondition { get; }
        public List<string> TopPenaltyBooks { get; }
        public DailySummary(List<Book> books, DateTime processedAt)
        {
            ProcessedAt = processedAt;
            TotalReturns = books.Count;
            CountByCondition = books
            .GroupBy(b => b.Condition)
            .ToDictionary(g => g.Key, g => g.Count());

            TopPenaltyBooks = books
                .GroupBy(b => new { b.Id, b.Title })
                .Select(g =>
                {
                    int penalty = g.Sum(b => GetPenalty(b.Condition));
                    penalty = Math.Clamp(penalty, 0, 100);

                    return new
                    {
                        Id = g.Key.Id,
                        Title = g.Key.Title,
                        penalty = penalty
                    };
                }).OrderByDescending(b=>b.penalty)
                .Select(b =>
                {
                    return $"Id: {b.Id} Title : {b.Title} (Penalty: {b.penalty})";
                })
                .Take(5)
                .ToList();   
                ;




        }
        /// <summary>
        /// Generates formatted report text
        /// </summary>
        public string GenerateReport()
        {
            var report = new System.Text.StringBuilder();

            report.AppendLine("Daily Library Returns Summary ");
            report.AppendLine($"Date/Time Processed: {ProcessedAt:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine($"Total Returns: {TotalReturns}");
            report.AppendLine();

            report.AppendLine("Count by Condition:");
            foreach (BookCondition condition in Enum.GetValues<BookCondition>())
            {
                var count = CountByCondition.GetValueOrDefault(condition, 0);
                report.AppendLine($"  {condition}: {count}");
            }

            report.AppendLine();
            report.AppendLine("Top 5 Titles by Penalty:");
            for (int i = 0; i < TopPenaltyBooks.Count; i++)
            {
                var book = TopPenaltyBooks[i];
                report.AppendLine(TopPenaltyBooks[i]);
            }

            return report.ToString();
        }

        public static int GetPenalty(BookCondition condition)
        {
            int penalty = 0;

            if (condition == BookCondition.New)
                penalty = -1;
            else if (condition == BookCondition.Good)
                penalty = 0;
            else if (condition == BookCondition.Worn)
                penalty = 3;
            else if (condition == BookCondition.Damaged)
                penalty = 10;
            else
                penalty = 0;
            return Math.Clamp(penalty,0, 100);
        }

    }
}
