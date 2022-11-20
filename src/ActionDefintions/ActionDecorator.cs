using SwDb.CLI.ActionDefintions;

namespace SwDb.CLI.Services;

public static class ActionDecorator
{
    public static IServiceCollection AddActions(this IServiceCollection collection)
    {
        var actionTypes = Assembly.GetEntryAssembly().GetTypes().Where(t => typeof(IAction).IsAssignableFrom(t) && !t.IsInterface).ToList();
        actionTypes.ForEach(t => collection.AddSingleton(typeof(IAction), t));
        return collection;
    }
}
