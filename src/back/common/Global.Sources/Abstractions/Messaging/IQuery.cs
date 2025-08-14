using Global.Sources.ResultPattern;
using MediatR;

namespace Global.Sources.Abstractions.Messaging;

public interface IQuery<TResponse> 
    : IRequest<Result<TResponse>>;