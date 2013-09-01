using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.Synchronization.BoardCalendarBondSynchronization
{
    public interface IBoardCalendarBondSynchronizationService
    {
        void UpdateBond(Calendar calendar);

        void UpdateBondSettings(Board board);
    }
}
