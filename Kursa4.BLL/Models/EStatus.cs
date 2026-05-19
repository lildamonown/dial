using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Models
{
    public enum EStatus
    {
        Processing,
        PendingClient,
        InWork,
        PendingPay,
        Completed,
        Cancelled
    }

    public static class EStatusExtensions
    {
        private static readonly Dictionary<EStatus, string> StatusDescriptions = new Dictionary<EStatus, string>
        {
            { EStatus.Processing, "В обработке" },
            { EStatus.PendingClient, "В ожидании клиента" },
            { EStatus.InWork, "В работе" },
            { EStatus.PendingPay, "Ожидание оплаты" },
            { EStatus.Completed, "Выполнен" },
            { EStatus.Cancelled, "Отменен" }
        };

        public static string GetValue(this EStatus status)
        {
            return StatusDescriptions.TryGetValue(status, out var description)
                ? description
                : status.ToString();
        }
    }
}
