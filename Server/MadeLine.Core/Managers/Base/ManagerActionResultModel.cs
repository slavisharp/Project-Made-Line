namespace MadeLine.Core.Managers
{
    using System.Collections.Generic;

    public class ManagerActionResultModel<T> : IManagerActionResultModel<T>
        where T: class
    {
        public bool Succeeded { get; set; }

        public T Model { get; set; }

        public IEnumerable<IErrorResultModel> Errors { get; set; }
    }
}
