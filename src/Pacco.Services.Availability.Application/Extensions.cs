using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace Pacco.Services.Availability.Application;
public static class Extensions
{
    public static IConveyBuilder AddApplication(this IConveyBuilder builder)
    {
        builder.AddCommandHandlers();
        builder.AddEventHandlers();
        builder.AddInMemoryCommandDispatcher();
        builder.AddInMemoryEventDispatcher();

        return builder;
    }
}
