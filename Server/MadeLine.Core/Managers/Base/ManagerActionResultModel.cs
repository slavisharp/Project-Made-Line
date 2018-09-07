namespace MadeLine.Core.Managers
{
    using System;
    using System.Collections.Generic;

    public class ManagerActionResultModel<T> : IManagerActionResultModel<T>
        where T: class
    {
        public ManagerActionResultModel()
        {
            this.Errors = new List<IErrorResultModel>();
        }

        public bool Succeeded { get; set; }

        public T Model { get; set; }

        public IList<IErrorResultModel> Errors { get; set; }
    }

    public class ErrorResultModel : IErrorResultModel
    {
        public ErrorResultModel()
        { }

        public ErrorResultModel(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public ErrorResultModel(Exception ex)
        {
            this.Code = "";
            this.Message = ex.Message;
        }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
