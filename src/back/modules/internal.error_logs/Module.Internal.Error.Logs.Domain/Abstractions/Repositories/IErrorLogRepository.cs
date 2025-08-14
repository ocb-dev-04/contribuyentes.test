using Global.Sources.ValueObjects.Values;
using Global.Sources.Abstractions.Repositories;
using Module.Internal.Error.Logs.Domain.Entities;

namespace Module.Internal.Error.Logs.Domain.Abstractions.Repositories;

public interface IErrorLogRepository 
    : ICreateGenericRepository<ErrorLog>,
        IBooleanGenericRepository<ErrorLog, UlidObject>;