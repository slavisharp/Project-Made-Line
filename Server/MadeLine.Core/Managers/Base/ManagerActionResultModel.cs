namespace MadeLine.Core.Managers
{
    using System;
    using System.Collections.Generic;

    public class ManagerActionResultModel<T> : IManagerActionResultModel<T>
        where T: class
    {
        public bool Succeeded { get; set; }

        public T Model { get; set; }

        public IList<IErrorResultModel> Errors { get; set; }
    }

    public class ErrorResultModel : IErrorResultModel
    {
        public ErrorResultModel()
        { }

        public ErrorResultModel(Exception ex)
        {
            this.Code = "";
            this.Message = ex.Message;
        }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
