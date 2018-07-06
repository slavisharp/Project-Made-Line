namespace MadeLine.Data.Models
{
    using System;

    public interface IAuditInfo
    {
        DateTime Created { get; set; }

        DateTime? Modified { get; set; }
    }
}
