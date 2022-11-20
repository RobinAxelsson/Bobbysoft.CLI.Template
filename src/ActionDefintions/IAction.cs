namespace SwDb.CLI.ActionDefintions;

public interface IAction
{
    ParserResult<object> Handle(ParserResult<object> parserResult);
}
