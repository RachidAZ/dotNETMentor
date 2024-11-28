using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Events;

public abstract class IntegrationEventBase
{
    public Guid EventId { get; }
    public DateTime Timestamp { get; }

    public string EventType { get; }

    protected IntegrationEventBase(string eventType)
    {
        EventId = Guid.NewGuid();
        Timestamp = DateTime.UtcNow;
        EventType = eventType;
    }

}
