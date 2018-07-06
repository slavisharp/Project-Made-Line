namespace MadeLine.Core.Managers
{
    using System.Collections.Generic;

    public interface IManagerActionResultModel<T>
        where T: class
    {
        bool Succeeded { get; }

        T Model { get; }

        IEnumerable<IErrorResultModel> Errors { get; }
    }

    public interface IErrorResultModel
    {
        string Code { get; set; }

        string Message { get; set; }
    }
}
