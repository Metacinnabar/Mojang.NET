namespace Mojang.NET.Common
{
    public struct SaleStatistics
    {
        public int TotalSold { get; set; }
        
        public int SoldLast24h { get; set; }
        
        public float AverageSalesPerSecond { get; set; }
    }
}