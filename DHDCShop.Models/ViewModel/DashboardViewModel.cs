using DHDCShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.ViewModel
{
    public class DashboardViewModel
    {
        public List<decimal> AverageEachMonth { get; set; }
        public Statistic StatisticIsNow { get; set; }
        public List<NationStatisticViewModel> ListNationStatistic { get; set; }
        public List<TopSaleViewModel> ListTopSales { get; set; }

        public DashboardViewModel()
        {
            AverageEachMonth = new List<decimal>();
            ListNationStatistic = new List<NationStatisticViewModel>();
            ListTopSales = new List<TopSaleViewModel>();
        }

    }
}
