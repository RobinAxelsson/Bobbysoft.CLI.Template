namespace $projectName$.ActionDefintions;

public interface IAction
{
    ParserResult<object> Handle(ParserResult<object> parserResult);
}
