using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Contracts;
using Accounting.WebAPI.Data;
using AutoMapper;

namespace Infrastructure
{
    public class BaseApiControllerWithDatabase : BaseApiController
    {
        public BaseApiControllerWithDatabase(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) : base()
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        protected IUnitOfWork UnitOfWork { get; }

        protected readonly IMapper _mapper;

        protected readonly ILoggerManager _logger;
    }
}
