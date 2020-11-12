using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Logic.LogicBase
{
    public class LogicBase
    {
       // protected readonly CatalogDbContext _catalogDb;
       
        protected readonly IMapper _mapper;

        protected LogicBase( IMapper mapper)
        {
           // _catalogDb = catalogDb;
          
            _mapper = mapper;
        }
    }
}
