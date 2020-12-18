using System;
using System.Collections.Generic;
using System.Text;

namespace Kiss.Application.Parameters.Mock
{
    public class GetAllPositionsParameter : QueryStringParameters
    {
        public string OrgCode { get; set; }
    }
}
